namespace MetalHive.Data.DataModel.Tables
{
    public class ProductionFacility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public float NormativeEquipmentArea { get; set; }

        public List<Contract> Contracts { get; set; }
    }
}
