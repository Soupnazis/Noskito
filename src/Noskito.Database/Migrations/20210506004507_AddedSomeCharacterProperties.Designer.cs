﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Noskito.Database;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Noskito.Database.Migrations
{
    [DbContext(typeof(NoskitoContext))]
    [Migration("20210506004507_AddedSomeCharacterProperties")]
    partial class AddedSomeCharacterProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Noskito.Database.Entity.DbAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Noskito.Database.Entity.DbCharacter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<int>("Dignity")
                        .HasColumnType("integer");

                    b.Property<long>("Experience")
                        .HasColumnType("bigint");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<int>("HairColor")
                        .HasColumnType("integer");

                    b.Property<int>("HairStyle")
                        .HasColumnType("integer");

                    b.Property<long>("HeroExperience")
                        .HasColumnType("bigint");

                    b.Property<int>("HeroLevel")
                        .HasColumnType("integer");

                    b.Property<int>("Hp")
                        .HasColumnType("integer");

                    b.Property<int>("Job")
                        .HasColumnType("integer");

                    b.Property<long>("JobExperience")
                        .HasColumnType("bigint");

                    b.Property<int>("JobLevel")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("MapId")
                        .HasColumnType("integer");

                    b.Property<int>("Mp")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Reputation")
                        .HasColumnType("bigint");

                    b.Property<byte>("Slot")
                        .HasColumnType("smallint");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Noskito.Database.Entity.DbMap", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Grid")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("Noskito.Database.Entity.DbCharacter", b =>
                {
                    b.HasOne("Noskito.Database.Entity.DbAccount", "Account")
                        .WithMany("Characters")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Noskito.Database.Entity.DbAccount", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
