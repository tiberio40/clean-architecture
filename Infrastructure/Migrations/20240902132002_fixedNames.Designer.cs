﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(SqlDbContext))]
    [Migration("20240902132002_fixedNames")]
    partial class fixedNames
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Campaign.MarketingCampaignEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDateRecurring")
                        .HasColumnType("datetime2");

                    b.Property<string>("HourForSending")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("IndefiniteEndDate")
                        .HasColumnType("bit");

                    b.Property<int>("MarketingId")
                        .HasColumnType("int");

                    b.Property<string>("OriginId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("RecurringTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StarDateRecurring")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SyncedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("MarketingId");

                    b.HasIndex("RecurringTypeId");

                    b.ToTable("MarketingCampaigns", "Campaign");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MarketingEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cover")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MarketingStatusId")
                        .HasColumnType("int");

                    b.Property<int?>("MetaConfigurationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("OAuthId")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("MarketingStatusId");

                    b.HasIndex("MetaConfigurationId");

                    b.ToTable("Marketings", "Campaign");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MarketingStatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("MarketingStatus", "Campaign");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MarketingTemplateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FormValues")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MarketingCampaignId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TemplateMetaId")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("TitleTemplateMeta")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("MarketingCampaignId");

                    b.ToTable("MarketingTemplates", "Campaign");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MarketingUserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContactId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("MarketingCampaignId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MarketingCampaignId");

                    b.ToTable("MarketingUsers", "Campaign");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MetaConfigurationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MetaTypeServiceId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("OAuthId")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("MetaTypeServiceId");

                    b.ToTable("MetaConfigurations", "Campaign");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MetaTypeServiceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("MetaTypeServices", "Campaign");
                });

            modelBuilder.Entity("Core.Entities.Campaign.RecurringTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("RecurringTypes", "Campaign");
                });

            modelBuilder.Entity("Core.Entities.Scaffolding.ConfigThemeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ColorBackgroundMenu")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ColorButtonCancel")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ColorButtonCreate")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ColorButtonText")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ColorButtonsAction")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ColorHeaderTable")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ColorSubtitle")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ColorText")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ColorTextColumn")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ColorTextMenu")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ColorTitle")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("IdFile")
                        .HasColumnType("int");

                    b.Property<string>("StyleLetter")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TypeParagraph")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TypeSubtitle")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TypeTitle")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("IdFile");

                    b.ToTable("ConfigTheme", "Scaffolding");
                });

            modelBuilder.Entity("Core.Entities.Scaffolding.FileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TypeFile")
                        .HasColumnType("int");

                    b.Property<string>("UrlFile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Files", "Scaffolding");
                });

            modelBuilder.Entity("Core.Entities.Scaffolding.NormalizeProjectNamesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NameNetSuite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameTMetric")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NormalizeProjectNames", "Scaffolding");
                });

            modelBuilder.Entity("Core.Entities.Securitty.MenuEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("IdMenuPrimary")
                        .HasColumnType("int");

                    b.Property<bool>("IsPrincipal")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Menu", "Security");
                });

            modelBuilder.Entity("Core.Entities.Securitty.PermissionEntity", b =>
                {
                    b.Property<int>("IdPermission")
                        .HasColumnType("int");

                    b.Property<string>("Ambit")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Permission")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdPermission");

                    b.ToTable("Permission", "Security");
                });

            modelBuilder.Entity("Core.Entities.Securitty.RolEntity", b =>
                {
                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdRol");

                    b.ToTable("Rol", "Security");
                });

            modelBuilder.Entity("Core.Entities.Securitty.RolMenusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdMenu")
                        .HasColumnType("int");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdMenu");

                    b.HasIndex("IdRol");

                    b.ToTable("RolMenusEntity");
                });

            modelBuilder.Entity("Core.Entities.Securitty.RolesPermissionsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdPermission")
                        .HasColumnType("int");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdPermission");

                    b.HasIndex("IdRol");

                    b.ToTable("RolesPermissions", "Security");
                });

            modelBuilder.Entity("Core.Entities.Securitty.UserEntity", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PasswordReset")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("VerefiedEmail")
                        .HasColumnType("bit");

                    b.HasKey("IdUser");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("IdRol");

                    b.ToTable("User", "Security");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MarketingCampaignEntity", b =>
                {
                    b.HasOne("Core.Entities.Campaign.MarketingEntity", "MarketingEntity")
                        .WithMany()
                        .HasForeignKey("MarketingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Campaign.RecurringTypeEntity", "RecurringTypeEntity")
                        .WithMany()
                        .HasForeignKey("RecurringTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketingEntity");

                    b.Navigation("RecurringTypeEntity");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MarketingEntity", b =>
                {
                    b.HasOne("Core.Entities.Campaign.MarketingStatusEntity", "MarketingStatusEntity")
                        .WithMany()
                        .HasForeignKey("MarketingStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Campaign.MetaConfigurationEntity", "MetaConfigurationEntity")
                        .WithMany("MarketingEntities")
                        .HasForeignKey("MetaConfigurationId");

                    b.Navigation("MarketingStatusEntity");

                    b.Navigation("MetaConfigurationEntity");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MarketingTemplateEntity", b =>
                {
                    b.HasOne("Core.Entities.Campaign.MarketingCampaignEntity", "MarketingCampaignEntity")
                        .WithMany("MarketingTemplateEntities")
                        .HasForeignKey("MarketingCampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketingCampaignEntity");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MarketingUserEntity", b =>
                {
                    b.HasOne("Core.Entities.Campaign.MarketingCampaignEntity", "MarketingCampaignEntity")
                        .WithMany("MarketingUserEntities")
                        .HasForeignKey("MarketingCampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketingCampaignEntity");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MetaConfigurationEntity", b =>
                {
                    b.HasOne("Core.Entities.Campaign.MetaTypeServiceEntity", "MetaTypeServiceEntity")
                        .WithMany("MetaConfigurationEntities")
                        .HasForeignKey("MetaTypeServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MetaTypeServiceEntity");
                });

            modelBuilder.Entity("Core.Entities.Scaffolding.ConfigThemeEntity", b =>
                {
                    b.HasOne("Core.Entities.Scaffolding.FileEntity", "FileEntity")
                        .WithMany("ConfigThemeEntities")
                        .HasForeignKey("IdFile");

                    b.Navigation("FileEntity");
                });

            modelBuilder.Entity("Core.Entities.Securitty.RolMenusEntity", b =>
                {
                    b.HasOne("Core.Entities.Securitty.MenuEntity", "MenuEntity")
                        .WithMany("RolMenusEntities")
                        .HasForeignKey("IdMenu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Securitty.RolEntity", "RolEntity")
                        .WithMany("RolMenusEntities")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuEntity");

                    b.Navigation("RolEntity");
                });

            modelBuilder.Entity("Core.Entities.Securitty.RolesPermissionsEntity", b =>
                {
                    b.HasOne("Core.Entities.Securitty.PermissionEntity", "PermissionEntity")
                        .WithMany("RolesPermissionsEntities")
                        .HasForeignKey("IdPermission")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Securitty.RolEntity", "RolEntity")
                        .WithMany("RolesPermissionsEntities")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PermissionEntity");

                    b.Navigation("RolEntity");
                });

            modelBuilder.Entity("Core.Entities.Securitty.UserEntity", b =>
                {
                    b.HasOne("Core.Entities.Securitty.RolEntity", "RolEntity")
                        .WithMany("UserEntities")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RolEntity");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MarketingCampaignEntity", b =>
                {
                    b.Navigation("MarketingTemplateEntities");

                    b.Navigation("MarketingUserEntities");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MetaConfigurationEntity", b =>
                {
                    b.Navigation("MarketingEntities");
                });

            modelBuilder.Entity("Core.Entities.Campaign.MetaTypeServiceEntity", b =>
                {
                    b.Navigation("MetaConfigurationEntities");
                });

            modelBuilder.Entity("Core.Entities.Scaffolding.FileEntity", b =>
                {
                    b.Navigation("ConfigThemeEntities");
                });

            modelBuilder.Entity("Core.Entities.Securitty.MenuEntity", b =>
                {
                    b.Navigation("RolMenusEntities");
                });

            modelBuilder.Entity("Core.Entities.Securitty.PermissionEntity", b =>
                {
                    b.Navigation("RolesPermissionsEntities");
                });

            modelBuilder.Entity("Core.Entities.Securitty.RolEntity", b =>
                {
                    b.Navigation("RolMenusEntities");

                    b.Navigation("RolesPermissionsEntities");

                    b.Navigation("UserEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
