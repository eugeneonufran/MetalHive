namespace MetalHive.Utilities.Enums
{
    public enum ContractValidationError
    {
        None = 0,
        EquipmentTypeDoesntExist = 1,
        ProductionFacilityDoesntExist = 2,
        ProductionFacilityIsAlreadyTaken = 3,
        InsufficientProductionSpace = 4
    }
}
