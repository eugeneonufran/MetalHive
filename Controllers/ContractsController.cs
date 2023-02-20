using MetalHive.Data.DataModel.DTO;
using MetalHive.Data.DataServices.Interfaces;
using MetalHive.Data.DataServices;
using Microsoft.AspNetCore.Mvc;
using MetalHive.Data.DataModel;
using MetalHive.Data.DataModel.Tables;
using Microsoft.EntityFrameworkCore;

namespace MetalHive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly MetalHiveDbContext _context;
        private readonly IContractDataService _contractDataService;

        public ContractsController(MetalHiveDbContext context, IContractDataService contractDataService)
        {
            _context = context;
            _contractDataService = contractDataService;
        }

        [HttpPost]
        public async Task<ActionResult<Contract>> CreateContract(ContractRequestDto contractRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validationError = await _contractDataService.ValidateContract(contractRequestDto);
            switch (validationError)
            {
                case ContractValidationError.None:
                    // Validation succeeded, so insert the contract
                    var query = await _contractDataService.InsertContract(contractRequestDto);
                    return CreatedAtAction("GetContracts", new { id = query.Id }, query);

                case ContractValidationError.EquipmentTypeDoesntExist:
                    return BadRequest(new { ErrorCode = 6001, ErrorDescription = "The specified equipment type does not exist." });
                case ContractValidationError.ProductionFacilityDoesntExist:
                    return BadRequest(new { ErrorCode = 6002, ErrorDescription = "The specified production facility does not exist." });
                case ContractValidationError.ProductionFacilityIsAlreadyTaken:
                    return BadRequest(new { ErrorCode = 6003, ErrorDescription = "The specified production facility is already assigned to another contract." });
                case ContractValidationError.InsufficientProductionSpace:
                    return BadRequest(new { ErrorCode = 6003, ErrorDescription = "Not enough available production space for the requested equipment type and count." });
                default:
                    return StatusCode(500);

            }
        }



        [HttpGet]
        public async Task<ActionResult<IQueryable<Contract>>> GetContracts()
        {
            try
            {
                var contracts = await _context.Contracts
                .Include(c => c.ProductionFacility)
                .Include(c => c.Equipment)
                .Select(c => new
                {
                    c.ClientName,
                    ProductionFacilityName = c.ProductionFacility.Name,
                    EquipmentTypeName = c.Equipment.Name,
                    c.EquipmentCount
                })
                .ToListAsync();

                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        protected void Dispose()
        {
            _context.Dispose();
            _contractDataService.Dispose();
        }
    }
}
