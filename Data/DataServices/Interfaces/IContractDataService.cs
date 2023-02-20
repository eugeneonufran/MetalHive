using MetalHive.Data.DataModel.DTO;
using MetalHive.Data.DataModel.Tables;
using Microsoft.AspNetCore.Mvc;

namespace MetalHive.Data.DataServices.Interfaces
{
    public interface IContractDataService : IDisposable
    {
        Task<ContractResponseDto> InsertContract(ContractRequestDto contractRequestDto);
        Task<ContractValidationError> ValidateContract(ContractRequestDto contractRequestDto);

        Task<List<ContractResponseDto>> GetContracts();
    }
}
