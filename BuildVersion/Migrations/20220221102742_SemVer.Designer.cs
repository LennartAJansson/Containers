﻿// <auto-generated />
using BuildVersion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BuildVersion.Migrations
{
    [DbContext(typeof(BuildVersionsDb))]
    [Migration("20220221102742_SemVer")]
    partial class SemVer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BuildVersion.Binary", b =>
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

                    b.HasIndex("BuildVersionId");

                    b.HasIndex("ProjectFile");

                    b.ToTable("Binaries");
                });

            modelBuilder.Entity("BuildVersion.BuildVersion", b =>
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

                    b.Property<string>("SemanticVersionRevision")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("BuildVersions");
                });

            modelBuilder.Entity("BuildVersion.Binary", b =>
                {
                    b.HasOne("BuildVersion.BuildVersion", "BuildVersion")
                        .WithMany()
                        .HasForeignKey("BuildVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuildVersion");
                });
#pragma warning restore 612, 618
        }
    }
}