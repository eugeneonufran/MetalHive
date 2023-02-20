﻿// <auto-generated />
using MetalHive.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MetalHive.Migrations
{
    [DbContext(typeof(MetalHiveDbContext))]
    [Migration("20230220085032_INIT")]
    partial class INIT
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MetalHive.Data.DataModel.Tables.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("EquipmentCount")
                        .HasColumnType("int");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<int>("ProductionFacilityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("ProductionFacilityId");

                    b.ToTable("Contracts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClientName = "Lucy",
                            EquipmentCount = 4,
                            EquipmentId = 1,
                            ProductionFacilityId = 1
                        },
                        new
                        {
                            Id = 2,
                            ClientName = "Maksym",
                            EquipmentCount = 4,
                            EquipmentId = 2,
                            ProductionFacilityId = 2
                        });
                });

            modelBuilder.Entity("MetalHive.Data.DataModel.Tables.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Footprint")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Equipments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Footprint = 10f,
                            Name = "GTS-X 5000"
                        },
                        new
                        {
                            Id = 2,
                            Footprint = 60f,
                            Name = "Samson 200"
                        },
                        new
                        {
                            Id = 3,
                            Footprint = 19f,
                            Name = "Herc - X"
                        },
                        new
                        {
                            Id = 4,
                            Footprint = 250f,
                            Name = "Foxxer 8000"
                        });
                });

            modelBuilder.Entity("MetalHive.Data.DataModel.Tables.ProductionFacility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("NormativeEquipmentArea")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("ProductionFacilities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Kyiv Factory 1",
                            NormativeEquipmentArea = 500.5f
                        },
                        new
                        {
                            Id = 2,
                            Name = "Zhytomyr Factory 1",
                            NormativeEquipmentArea = 1000f
                        },
                        new
                        {
                            Id = 3,
                            Name = "Volyn",
                            NormativeEquipmentArea = 5000f
                        });
                });

            modelBuilder.Entity("MetalHive.Data.DataModel.Tables.Contract", b =>
                {
                    b.HasOne("MetalHive.Data.DataModel.Tables.Equipment", "Equipment")
                        .WithMany("Contracts")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MetalHive.Data.DataModel.Tables.ProductionFacility", "ProductionFacility")
                        .WithMany("Contracts")
                        .HasForeignKey("ProductionFacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("ProductionFacility");
                });

            modelBuilder.Entity("MetalHive.Data.DataModel.Tables.Equipment", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("MetalHive.Data.DataModel.Tables.ProductionFacility", b =>
                {
                    b.Navigation("Contracts");
                });
#pragma warning restore 612, 618
        }
    }
}
