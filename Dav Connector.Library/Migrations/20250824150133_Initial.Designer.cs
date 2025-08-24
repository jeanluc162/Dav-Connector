using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Dav_Connector.Library.Model;

namespace DavConnector.Library.Migrations
{
    [DbContext(typeof(DavConnectorDbContext))]
    [Migration("20250824150133_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.6");

            modelBuilder.Entity("Dav_Connector.Library.Model.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountTypeId");

                    b.Property<string>("EncryptedPassword");

                    b.Property<string>("Name");

                    b.Property<Guid>("SyncTypeId");

                    b.Property<string>("Url");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.HasIndex("SyncTypeId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Dav_Connector.Library.Model.AccountType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");
                });

            modelBuilder.Entity("Dav_Connector.Library.Model.SyncType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SyncTypes");
                });

            modelBuilder.Entity("Dav_Connector.Library.Model.Account", b =>
                {
                    b.HasOne("Dav_Connector.Library.Model.AccountType", "AccountType")
                        .WithMany()
                        .HasForeignKey("AccountTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dav_Connector.Library.Model.SyncType", "SyncType")
                        .WithMany()
                        .HasForeignKey("SyncTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
