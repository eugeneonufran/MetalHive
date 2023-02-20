namespace MetalHive.Data.DataServices.Interfaces
{
    public interface IContractDataService : IDisposable
    {
        Task<ContractResponseDto> InsertContract(ContractRequestDto contractRequestDto);
        Task<ContractValidationError> ValidateContract(ContractRequestDto contractRequestDto);

        Task<List<ContractResponseDto>> GetContracts();
    }
}
