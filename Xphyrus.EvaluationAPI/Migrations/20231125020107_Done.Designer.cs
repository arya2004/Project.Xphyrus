﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xphyrus.EvaluationAPI.Data;

#nullable disable

namespace Xphyrus.EvaluationAPI.Migrations
{
    [DbContext(typeof(ApplicatioDbContext))]
    [Migration("20231125020107_Done")]
    partial class Done
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Xphyrus.EvaluationAPI.Models.SubmissionRequest", b =>
                {
                    b.Property<string>("SubmissionRequestId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AssesmentId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("expected_output")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("language_id")
                        .HasColumnType("int");

                    b.Property<string>("source_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("stdin")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubmissionRequestId");

                    b.ToTable("submissionRequests");
                });

            modelBuilder.Entity("Xphyrus.EvaluationAPI.Models.UserSubmissionandSulition", b =>
                {
                    b.Property<int>("UserSubmissionandSulitionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserSubmissionandSulitionId"));

                    b.Property<int>("AssesmentId")
                        .HasColumnType("int");

                    b.Property<int>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedON")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<int>("LanguageCode")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserSubmissionandSulitionId");

                    b.ToTable("userSubmissionandSulitions");
                });
#pragma warning restore 612, 618
        }
    }
}