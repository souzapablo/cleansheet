﻿// <auto-generated />
using System;
using CleanSheet.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanSheet.Infrastructure.Migrations
{
    [DbContext(typeof(CleanSheetDbContext))]
    [Migration("20240826204136_InitialTeamsTable")]
    partial class InitialTeamsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CleanSheet.Domain.Entities.Career", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Careers");
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.InitialTeam", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Stadium")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.HasKey("Id");

                    b.ToTable("InitialTeams");
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<short>("KitNumber")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<short>("Overall")
                        .HasColumnType("smallint");

                    b.Property<short>("Position")
                        .HasColumnType("smallint");

                    b.Property<long?>("TeamId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.Team", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CareerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Stadium")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("CareerId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.Career", b =>
                {
                    b.HasOne("CleanSheet.Domain.Entities.User", "User")
                        .WithMany("Careers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CleanSheet.Domain.Entities.Manager", "Manager", b1 =>
                        {
                            b1.Property<long>("CareerId")
                                .HasColumnType("bigint");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("varchar(12)")
                                .HasColumnName("ManagerFirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("varchar(12)")
                                .HasColumnName("ManagerLastName");

                            b1.HasKey("CareerId");

                            b1.ToTable("Careers");

                            b1.WithOwner()
                                .HasForeignKey("CareerId");
                        });

                    b.Navigation("Manager")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.InitialTeam", b =>
                {
                    b.OwnsMany("CleanSheet.Domain.Entities.InitialPlayer", "InitialSquad", b1 =>
                        {
                            b1.Property<long>("InitialTeamId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<DateOnly>("Birthday")
                                .HasColumnType("date");

                            b1.Property<int>("KitNumber")
                                .HasColumnType("integer");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("varchar(50)");

                            b1.Property<int>("Overall")
                                .HasColumnType("integer");

                            b1.Property<string>("Position")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("InitialTeamId", "Id");

                            b1.ToTable("InitialTeams");

                            b1.ToJson("InitialSquad");

                            b1.WithOwner()
                                .HasForeignKey("InitialTeamId");
                        });

                    b.Navigation("InitialSquad");
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.Player", b =>
                {
                    b.HasOne("CleanSheet.Domain.Entities.Team", null)
                        .WithMany("Squad")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.Team", b =>
                {
                    b.HasOne("CleanSheet.Domain.Entities.Career", "Career")
                        .WithMany("Teams")
                        .HasForeignKey("CareerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Career");
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.Career", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.Team", b =>
                {
                    b.Navigation("Squad");
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.User", b =>
                {
                    b.Navigation("Careers");
                });
#pragma warning restore 612, 618
        }
    }
}
