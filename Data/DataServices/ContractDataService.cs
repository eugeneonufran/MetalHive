using MetalHive.Data.DataModel;
using MetalHive.Data.DataModel.DTO;
using MetalHive.Data.DataModel.Tables;
using MetalHive.Data.DataServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetalHive.Data.DataServices
{
    public enum ContractValidationError
    {
        None = 0,
        EquipmentTypeDoesntExist = 1,
        ProductionFacilityDoesntExist = 2,
        ProductionFacilityIsAlreadyTaken = 3,
        InsufficientProductionSpace = 4
    }

    public class ContractDataService : IContractDataService
    {
        private readonly MetalHiveDbContext _metalhiveDbContext;

        public ContractDataService(MetalHiveDbContext metalhiveDbContext)
        {
            _metalhiveDbContext = metalhiveDbContext;
        }

        public async Task<ContractResponseDto> InsertContract(ContractRequestDto contractRequestDto)
        {
            var contract = new Contract
            {
                ClientName = contractRequestDto.ClientName,
                ProductionFacilityId = contractRequestDto.ProductionFacilityId,
                EquipmentCount = contractRequestDto.EquipmentCount,
                EquipmentId = contractRequestDto.EquipmentId,
            };
            await _metalhiveDbContext.Contracts.AddAsync(contract);

            await _metalhiveDbContext.SaveChangesAsync();

            return new ContractResponseDto
            {
                Id = contract.Id,
                ClientName = contract.ClientName,
                ProductionFacilityId = contract.ProductionFacilityId,
                EquipmentCount = contract.EquipmentCount,
                EquipmentId = contract.EquipmentId,
            };

        }


        public async Task<ContractValidationError> ValidateContract(ContractRequestDto contractRequestDto)
        {
            var equipmentTypeExists = await _metalhiveDbContext.Equipments.AnyAsync(et => et.Id == contractRequestDto.EquipmentId);
            if (!equipmentTypeExists)
            {
                return ContractValidationError.EquipmentTypeDoesntExist;
            }

            var productionFacilityExists = await _metalhiveDbContext.ProductionFacilities.AnyAsync(pf => pf.Id == contractRequestDto.ProductionFacilityId);
            if (!productionFacilityExists)
            {
                return ContractValidationError.ProductionFacilityDoesntExist;
            }

            var productionFacilityHasContracts = await _metalhiveDbContext.Contracts.AnyAsync(c => c.ProductionFacilityId == contractRequestDto.ProductionFacilityId);
            if (productionFacilityHasContracts)
            {
                return ContractValidationError.ProductionFacilityIsAlreadyTaken;
            }

            var facility = await _metalhiveDbContext.ProductionFacilities.FirstAsync(pf => pf.Id == contractRequestDto.ProductionFacilityId);
            var equipment = await _metalhiveDbContext.Equipments.FirstAsync(et => et.Id == contractRequestDto.EquipmentId);
            var requiredArea = equipment.Footprint * contractRequestDto.EquipmentCount;

            if (facility.NormativeEquipmentArea < requiredArea)
            {
                return ContractValidationError.InsufficientProductionSpace;
            }

            return ContractValidationError.None;
        }

        public async Task<List<ContractResponseDto>> GetContracts()
        {
            var contracts = await _metalhiveDbContext.Contracts
                .Include(c => c.ProductionFacility)
                .Include(c => c.Equipment)
                .Select(c => new ContractResponseDto
                {
                    Id= c.Id,
                    ClientName = c.ClientName,
                    ProductionFacilityId = c.ProductionFacility.Id,
                    EquipmentId = c.Equipment.Id,
                    EquipmentCount = c.EquipmentCount
                })
                .ToListAsync();

            return contracts;
        }

        public void Dispose()
        {
            _metalhiveDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        
    }
}
