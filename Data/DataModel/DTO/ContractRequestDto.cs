using System.ComponentModel.DataAnnotations;

namespace MetalHive.Data.DataModel.DTO
{
    public class ContractRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string ClientName { get; set; }

        [Required]
        [Range(1, 10000)]
        public int EquipmentCount { get; set; }

        public int ProductionFacilityId { get; set; }

        public int EquipmentId { get; set; }
    }
}
