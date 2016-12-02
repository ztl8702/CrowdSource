﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CrowdSource.Data;

namespace CrowdSource.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161202141730_FirstModelMigration")]
    partial class FirstModelMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CrowdSource.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.ApplicationUserEndorsesGroupVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("AUEGVs");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.Collection", b =>
                {
                    b.Property<int>("CollectionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CollectionId");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.Field", b =>
                {
                    b.Property<int>("FieldId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FieldMetadata");

                    b.Property<int?>("FieldTypeForeignKey");

                    b.Property<int?>("GroupForeignKey");

                    b.HasKey("FieldId");

                    b.HasIndex("FieldTypeForeignKey");

                    b.HasIndex("GroupForeignKey");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.FieldType", b =>
                {
                    b.Property<int>("FieldTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("FieldTypeId");

                    b.ToTable("FieldTypes");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CollectionForeignKey");

                    b.HasKey("GroupId");

                    b.HasIndex("CollectionForeignKey");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.GroupVersion", b =>
                {
                    b.Property<int>("GroupVersionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GroupForeignKey");

                    b.Property<int?>("NextVersionGroupVersionId");

                    b.HasKey("GroupVersionId");

                    b.HasIndex("GroupForeignKey");

                    b.HasIndex("NextVersionGroupVersionId");

                    b.ToTable("GroupVersions");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.GroupVersionRefersSuggestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("GVSuggestions");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.Suggestion", b =>
                {
                    b.Property<int>("SuggestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserForeignKey");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<int?>("FieldForeignKey");

                    b.HasKey("SuggestionId");

                    b.HasIndex("ApplicationUserForeignKey");

                    b.HasIndex("FieldForeignKey");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.Field", b =>
                {
                    b.HasOne("CrowdSource.Models.CoreModels.FieldType", "FieldType")
                        .WithMany()
                        .HasForeignKey("FieldTypeForeignKey");

                    b.HasOne("CrowdSource.Models.CoreModels.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupForeignKey");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.Group", b =>
                {
                    b.HasOne("CrowdSource.Models.CoreModels.Collection", "Collection")
                        .WithMany()
                        .HasForeignKey("CollectionForeignKey");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.GroupVersion", b =>
                {
                    b.HasOne("CrowdSource.Models.CoreModels.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupForeignKey");

                    b.HasOne("CrowdSource.Models.CoreModels.GroupVersion", "NextVersion")
                        .WithMany()
                        .HasForeignKey("NextVersionGroupVersionId");
                });

            modelBuilder.Entity("CrowdSource.Models.CoreModels.Suggestion", b =>
                {
                    b.HasOne("CrowdSource.Models.ApplicationUser", "Author")
                        .WithMany()
                        .HasForeignKey("ApplicationUserForeignKey");

                    b.HasOne("CrowdSource.Models.CoreModels.Field", "Field")
                        .WithMany()
                        .HasForeignKey("FieldForeignKey");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CrowdSource.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CrowdSource.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CrowdSource.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
