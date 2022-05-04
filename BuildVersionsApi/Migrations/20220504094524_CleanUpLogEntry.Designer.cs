﻿// <auto-generated />
using BuildVersionsApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BuildVersion.Migrations
{
    [DbContext(typeof(BuildVersionsDb))]
    [Migration("20220504094524_CleanUpLogEntry")]
    partial class CleanUpLogEntry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BuildVersionsApi.Data.Binary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BuildVersionId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectFile")
                        .IsRequired()
                        .HasColumnType("varchar(95)");

                    b.HasKey("Id");

                    b.HasIndex("BuildVersionId")
                        .IsUnique();

                    b.HasIndex("ProjectFile");

                    b.ToTable("Binaries");
                });

            modelBuilder.Entity("BuildVersionsApi.Data.BuildVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Build")
                        .HasColumnType("int");

                    b.Property<int>("Major")
                        .HasColumnType("int");

                    b.Property<int>("Minor")
                        .HasColumnType("int");

                    b.Property<int>("Revision")
                        .HasColumnType("int");

                    b.Property<string>("SemanticVersionPre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("BuildVersions");
                });

            modelBuilder.Entity("BuildVersionsApi.Data.Binary", b =>
                {
                    b.HasOne("BuildVersionsApi.Data.BuildVersion", "BuildVersion")
                        .WithOne("Binary")
                        .HasForeignKey("BuildVersionsApi.Data.Binary", "BuildVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuildVersion");
                });

            modelBuilder.Entity("BuildVersionsApi.Data.BuildVersion", b =>
                {
                    b.Navigation("Binary")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
