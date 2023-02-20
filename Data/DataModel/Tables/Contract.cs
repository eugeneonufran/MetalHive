using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MetalHive.Data.DataModel.Tables
{
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClientName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int EquipmentCount { get; set; }

        [ForeignKey("ProductionFacility")]
        public int ProductionFacilityId { get; set; }
        public ProductionFacility ProductionFacility { get; set; }

        [ForeignKey("EquipmentType")]
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

    }
}

