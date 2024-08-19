﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(AdminDbContext))]
    partial class AdminDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Server.DataModels.EnglishWords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EWord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EnglishWords");
                });

            modelBuilder.Entity("Server.DataModels.EnglishWordsUkrainianWords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EnglishWordsId")
                        .HasColumnType("int");

                    b.Property<int>("UkrainianWordsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnglishWordsId");

                    b.HasIndex("UkrainianWordsId");

                    b.ToTable("EnglishWordsUkrainianWords");
                });

            modelBuilder.Entity("Server.DataModels.Progress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EnglishWordsId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnglishWordsId");

                    b.HasIndex("UserId");

                    b.ToTable("Progresses");
                });

            modelBuilder.Entity("Server.DataModels.UkrainianWords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UWord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UkrainianWords");
                });

            modelBuilder.Entity("Server.DataModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Server.DataModels.EnglishWordsUkrainianWords", b =>
                {
                    b.HasOne("Server.DataModels.EnglishWords", "EnglishWords")
                        .WithMany("EnglishWordsUkrainianWords")
                        .HasForeignKey("EnglishWordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.DataModels.UkrainianWords", "UkrainianWords")
                        .WithMany("EnglishWordsUkrainianWords")
                        .HasForeignKey("UkrainianWordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnglishWords");

                    b.Navigation("UkrainianWords");
                });

            modelBuilder.Entity("Server.DataModels.Progress", b =>
                {
                    b.HasOne("Server.DataModels.EnglishWords", "EnglishWords")
                        .WithMany("Progress")
                        .HasForeignKey("EnglishWordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.DataModels.User", "User")
                        .WithMany("Progresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnglishWords");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.DataModels.EnglishWords", b =>
                {
                    b.Navigation("EnglishWordsUkrainianWords");

                    b.Navigation("Progress");
                });

            modelBuilder.Entity("Server.DataModels.UkrainianWords", b =>
                {
                    b.Navigation("EnglishWordsUkrainianWords");
                });

            modelBuilder.Entity("Server.DataModels.User", b =>
                {
                    b.Navigation("Progresses");
                });
#pragma warning restore 612, 618
        }
    }
}