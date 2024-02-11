﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NRIAwards.DAL.Context;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NRIAwards.DAL.Context.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    [Migration("20240211150107_awards")]
    partial class awards
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Award", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("VisualContentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VisualContentId");

                    b.ToTable("Awards");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.AwardEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AwardId")
                        .HasColumnType("integer");

                    b.Property<int>("AwardsId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("VisualContentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AwardId");

                    b.ToTable("AwardEvents");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.AwardSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AwardId")
                        .HasColumnType("integer");

                    b.Property<string>("ConnectionCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NominationPassed")
                        .HasColumnType("integer");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AwardId");

                    b.HasIndex("UserId");

                    b.ToTable("AwardSessions");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Nomination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AwardId")
                        .HasColumnType("integer");

                    b.Property<int>("AwardsId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReaderId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("VisualContentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AwardId");

                    b.HasIndex("ReaderId");

                    b.HasIndex("VisualContentId");

                    b.ToTable("Nominations");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Nominee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NominationId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("VisualContentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NominationId");

                    b.HasIndex("VisualContentId");

                    b.ToTable("Nominee");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Reader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Reader");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("VisualContentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VisualContentId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsBlocked = false,
                            Login = "admin",
                            Password = "6fe7785e8523e09070fa676fa94c272b09c11699149a2a7589e67bf8ce81fd97ffb944005390f83e5eb1299383fc2b6c42bfc902e0daf106d64c3b574f68112f",
                            RoleId = 1,
                            UpdatedAt = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            VisualContentId = 1
                        });
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.VisualContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("VisualContents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Link = "",
                            Title = "Placeholder",
                            Type = 1,
                            UpdatedAt = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsCanseld")
                        .HasColumnType("boolean");

                    b.Property<int>("NomineeId")
                        .HasColumnType("integer");

                    b.Property<string>("TelegramAvatar")
                        .HasColumnType("text");

                    b.Property<string>("TelegramUserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Tier")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("NomineeId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Award", b =>
                {
                    b.HasOne("NRIAwards.DAL.Context.Model.VisualContent", null)
                        .WithMany("Awards")
                        .HasForeignKey("VisualContentId");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.AwardEvent", b =>
                {
                    b.HasOne("NRIAwards.DAL.Context.Model.Award", "Award")
                        .WithMany("AwardEvents")
                        .HasForeignKey("AwardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Award");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.AwardSession", b =>
                {
                    b.HasOne("NRIAwards.DAL.Context.Model.Award", "Award")
                        .WithMany("AwardSessions")
                        .HasForeignKey("AwardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NRIAwards.DAL.Context.Model.User", "User")
                        .WithMany("AwardSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Award");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Nomination", b =>
                {
                    b.HasOne("NRIAwards.DAL.Context.Model.Award", "Award")
                        .WithMany("Nominations")
                        .HasForeignKey("AwardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NRIAwards.DAL.Context.Model.Reader", "Reader")
                        .WithMany("Nominations")
                        .HasForeignKey("ReaderId");

                    b.HasOne("NRIAwards.DAL.Context.Model.VisualContent", null)
                        .WithMany("Nominations")
                        .HasForeignKey("VisualContentId");

                    b.Navigation("Award");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Nominee", b =>
                {
                    b.HasOne("NRIAwards.DAL.Context.Model.Nomination", "Nomination")
                        .WithMany("Nominees")
                        .HasForeignKey("NominationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NRIAwards.DAL.Context.Model.VisualContent", null)
                        .WithMany("Nominees")
                        .HasForeignKey("VisualContentId");

                    b.Navigation("Nomination");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.User", b =>
                {
                    b.HasOne("NRIAwards.DAL.Context.Model.VisualContent", null)
                        .WithMany("Users")
                        .HasForeignKey("VisualContentId");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Vote", b =>
                {
                    b.HasOne("NRIAwards.DAL.Context.Model.Nominee", "Nominee")
                        .WithMany("Votes")
                        .HasForeignKey("NomineeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nominee");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Award", b =>
                {
                    b.Navigation("AwardEvents");

                    b.Navigation("AwardSessions");

                    b.Navigation("Nominations");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Nomination", b =>
                {
                    b.Navigation("Nominees");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Nominee", b =>
                {
                    b.Navigation("Votes");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.Reader", b =>
                {
                    b.Navigation("Nominations");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.User", b =>
                {
                    b.Navigation("AwardSessions");
                });

            modelBuilder.Entity("NRIAwards.DAL.Context.Model.VisualContent", b =>
                {
                    b.Navigation("Awards");

                    b.Navigation("Nominations");

                    b.Navigation("Nominees");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
