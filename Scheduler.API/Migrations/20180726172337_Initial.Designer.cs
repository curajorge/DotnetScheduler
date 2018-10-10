using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Scheduler.Data;
using Scheduler.Model;

namespace Scheduler.API.Migrations
{
    [DbContext(typeof(SchedulerContext))]
    [Migration("20180726172337_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Scheduler.Model.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<int>("AddressType");

                    b.Property<string>("City");

                    b.Property<int>("CustomerId");

                    b.Property<string>("State");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Scheduler.Model.Attendee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ScheduleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("UserId");

                    b.ToTable("Attendee");
                });

            modelBuilder.Entity("Scheduler.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2018, 7, 26, 13, 23, 37, 777, DateTimeKind.Local));

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("Lead");

                    b.Property<int?>("LeadTypeId");

                    b.HasKey("Id");

                    b.HasIndex("LeadTypeId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Scheduler.Model.EndUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<DateTime>("LastLogging");

                    b.Property<string>("LastName");

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("EndUser");
                });

            modelBuilder.Entity("Scheduler.Model.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AddressId");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("JobType");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("Scheduler.Model.JobAssignee", b =>
                {
                    b.Property<int>("EndUserId");

                    b.Property<int>("JobId");

                    b.Property<int>("Id");

                    b.HasKey("EndUserId", "JobId");

                    b.HasIndex("JobId");

                    b.ToTable("JobAssignee");
                });

            modelBuilder.Entity("Scheduler.Model.Lead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("WeeklyCost");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Lead");
                });

            modelBuilder.Entity("Scheduler.Model.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<string>("Number");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("PhoneNumber");
                });

            modelBuilder.Entity("Scheduler.Model.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatorId");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2018, 7, 26, 13, 23, 37, 795, DateTimeKind.Local));

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2018, 7, 26, 13, 23, 37, 795, DateTimeKind.Local));

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<DateTime>("TimeEnd");

                    b.Property<DateTime>("TimeStart");

                    b.Property<string>("Title");

                    b.Property<int>("Type")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("Scheduler.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Profession");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Scheduler.Model.WorkDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JobId");

                    b.Property<decimal>("Revenue");

                    b.Property<int>("WorkType");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("WorkDescription");
                });

            modelBuilder.Entity("Scheduler.Model.Address", b =>
                {
                    b.HasOne("Scheduler.Model.Customer", "Customer")
                        .WithMany("Addresses")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Scheduler.Model.Attendee", b =>
                {
                    b.HasOne("Scheduler.Model.Schedule", "Schedule")
                        .WithMany("Attendees")
                        .HasForeignKey("ScheduleId");

                    b.HasOne("Scheduler.Model.User", "User")
                        .WithMany("SchedulesAttended")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Scheduler.Model.Customer", b =>
                {
                    b.HasOne("Scheduler.Model.Lead", "LeadType")
                        .WithMany()
                        .HasForeignKey("LeadTypeId");
                });

            modelBuilder.Entity("Scheduler.Model.Job", b =>
                {
                    b.HasOne("Scheduler.Model.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Scheduler.Model.Customer", "Customer")
                        .WithMany("Jobs")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Scheduler.Model.JobAssignee", b =>
                {
                    b.HasOne("Scheduler.Model.EndUser", "EndUser")
                        .WithMany("JobAssignees")
                        .HasForeignKey("EndUserId");

                    b.HasOne("Scheduler.Model.Job", "Job")
                        .WithMany("JobAssignees")
                        .HasForeignKey("JobId");
                });

            modelBuilder.Entity("Scheduler.Model.PhoneNumber", b =>
                {
                    b.HasOne("Scheduler.Model.Customer", "Customer")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Scheduler.Model.Schedule", b =>
                {
                    b.HasOne("Scheduler.Model.User", "Creator")
                        .WithMany("SchedulesCreated")
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("Scheduler.Model.WorkDescription", b =>
                {
                    b.HasOne("Scheduler.Model.Job", "Job")
                        .WithMany("WorkDescriptions")
                        .HasForeignKey("JobId");
                });
        }
    }
}
