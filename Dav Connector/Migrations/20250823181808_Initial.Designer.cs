using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Dav_Connector.Model;

namespace Dav_Connector.Migrations
{
    [DbContext(typeof(DavConnectorDbContext))]
    [Migration("20250823181808_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.6");

            modelBuilder.Entity("Dav_Connector.Model.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AccountTypeId");

                    b.Property<string>("EncryptedPassword");

                    b.Property<string>("Name");

                    b.Property<Guid?>("SyncTypeId");

                    b.Property<string>("Url");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.HasIndex("SyncTypeId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Dav_Connector.Model.AccountType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");
                });

            modelBuilder.Entity("Dav_Connector.Model.SyncType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SyncTypes");
                });

            modelBuilder.Entity("Dav_Connector.Model.Account", b =>
                {
                    b.HasOne("Dav_Connector.Model.AccountType", "AccountType")
                        .WithMany()
                        .HasForeignKey("AccountTypeId");

                    b.HasOne("Dav_Connector.Model.SyncType", "SyncType")
                        .WithMany()
                        .HasForeignKey("SyncTypeId");
                });
        }
    }
}
