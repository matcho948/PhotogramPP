﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Photogram;

#nullable disable

namespace Photogram.Migrations
{
    [DbContext(typeof(PhotogramDbContext))]
    [Migration("20220404181149_modifymodels")]
    partial class modifymodels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Photogram.Models.Comments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhotosId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PhotosId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Photogram.Models.Photos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserAccountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Photogram.Models.Reactions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("PhotosId")
                        .HasColumnType("int");

                    b.Property<int>("TypeOfReaction")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PhotosId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("Photogram.Models.Reports", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("PhotoId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Photogram.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Users");
                });

            modelBuilder.Entity("Photogram.Models.AdminAccount", b =>
                {
                    b.HasBaseType("Photogram.Models.Users");

                    b.HasDiscriminator().HasValue("AdminAccount");
                });

            modelBuilder.Entity("Photogram.Models.UserAccount", b =>
                {
                    b.HasBaseType("Photogram.Models.Users");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("UserAccount");
                });

            modelBuilder.Entity("Photogram.Models.Comments", b =>
                {
                    b.HasOne("Photogram.Models.Photos", null)
                        .WithMany("Comments")
                        .HasForeignKey("PhotosId");
                });

            modelBuilder.Entity("Photogram.Models.Photos", b =>
                {
                    b.HasOne("Photogram.Models.UserAccount", null)
                        .WithMany("Photos")
                        .HasForeignKey("UserAccountId");
                });

            modelBuilder.Entity("Photogram.Models.Reactions", b =>
                {
                    b.HasOne("Photogram.Models.Photos", null)
                        .WithMany("Reactions")
                        .HasForeignKey("PhotosId");
                });

            modelBuilder.Entity("Photogram.Models.Reports", b =>
                {
                    b.HasOne("Photogram.Models.Comments", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId");

                    b.HasOne("Photogram.Models.Photos", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId");

                    b.Navigation("Comment");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("Photogram.Models.Photos", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("Photogram.Models.UserAccount", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
