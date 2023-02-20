﻿using MetalHive.Data.DataModel.Tables;
using Microsoft.EntityFrameworkCore;

namespace MetalHive.Data.DataModel
{
    public class MetalHiveDbContext : DbContext
    {
        public MetalHiveDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ProductionFacility> ProductionFacilities { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // EquipmentTypes seed data
            modelBuilder.Entity<Equipment>().HasData(

                new Equipment
                {
                    Id = 1,
                    Name = "GTS-X 5000",
                    Footprint = 10.0f
                },

                new Equipment
                {
                    Id = 2,
                    Name = "Samson 200",
                    Footprint = 60.0f
                }
            );

            // ProductionFacilities seed data
            modelBuilder.Entity<ProductionFacility>().HasData(
                new ProductionFacility
                {
                    Id = 1,
                    Name = "Kyiv Factory 1",
                    NormativeEquipmentArea = 500.5f
                },

                new ProductionFacility
                {
                    Id = 2,
                    Name = "Zhytomyr Factory 1",
                    NormativeEquipmentArea = 1000.0f
                }
            );

            // Contracts seed data
            modelBuilder.Entity<Contract>().HasData(

                new Contract
                {
                    Id = 1,
                    EquipmentCount = 1,
                    EquipmentId = 1,
                    ProductionFacilityId = 1,
                    ClientName = "Lucy",
                }
            );


        }


    }
}