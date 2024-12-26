﻿// <auto-generated />
using System;
using JobPlusWPF.DBLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobPlusWPF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241226132147_AddSalaryForSalaryEntity")]
    partial class AddSalaryForSalaryEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JobPlusWPF.Model.Classes.Benefit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("JobSeekerId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("JobSeekerId");

                    b.HasIndex("UserId");

                    b.ToTable("Benefits");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.CityDirectory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.EducationLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EducationLevels");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.Employer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StreetId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("StreetId");

                    b.HasIndex("UserId");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.StreetDirectory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Streets");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployerId")
                        .HasColumnType("integer");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Salary")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EmployerId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("JobSeeker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("EducationDocumentScan")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EducationLevelId")
                        .HasColumnType("integer");

                    b.Property<string>("Institution")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PassportIssueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PassportIssuedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StreetId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkExperience")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("EducationLevelId");

                    b.HasIndex("StreetId");

                    b.HasIndex("UserId");

                    b.ToTable("JobSeekers");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.Benefit", b =>
                {
                    b.HasOne("JobSeeker", "JobSeeker")
                        .WithMany()
                        .HasForeignKey("JobSeekerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobPlusWPF.Model.Classes.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobSeeker");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.Employer", b =>
                {
                    b.HasOne("JobPlusWPF.Model.Classes.CityDirectory", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JobPlusWPF.Model.Classes.StreetDirectory", "Street")
                        .WithMany()
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JobPlusWPF.Model.Classes.User", "User")
                        .WithMany("Employers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Street");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.Vacancy", b =>
                {
                    b.HasOne("JobPlusWPF.Model.Classes.Employer", "Employer")
                        .WithMany("Vacancies")
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employer");
                });

            modelBuilder.Entity("JobSeeker", b =>
                {
                    b.HasOne("JobPlusWPF.Model.Classes.CityDirectory", "City")
                        .WithMany("JobSeekers")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JobPlusWPF.Model.Classes.EducationLevel", "EducationLevel")
                        .WithMany("JobSeekers")
                        .HasForeignKey("EducationLevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JobPlusWPF.Model.Classes.StreetDirectory", "Street")
                        .WithMany("JobSeekers")
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JobPlusWPF.Model.Classes.User", "User")
                        .WithMany("JobSeekers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("EducationLevel");

                    b.Navigation("Street");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.CityDirectory", b =>
                {
                    b.Navigation("JobSeekers");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.EducationLevel", b =>
                {
                    b.Navigation("JobSeekers");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.Employer", b =>
                {
                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.StreetDirectory", b =>
                {
                    b.Navigation("JobSeekers");
                });

            modelBuilder.Entity("JobPlusWPF.Model.Classes.User", b =>
                {
                    b.Navigation("Employers");

                    b.Navigation("JobSeekers");
                });
#pragma warning restore 612, 618
        }
    }
}
