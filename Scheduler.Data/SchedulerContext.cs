using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scheduler.Model;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Scheduler.Data
{
    public class SchedulerContext : DbContext
    {
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        //Freezer
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobAssignee> JobAssignees { get; set; }
        public DbSet<EndUser> EndUsers { get; set; }
        public DbSet<WorkDescription> WorkDescriptions { get; set; }
        public DbSet<Lead> Leads { get; set; }


        public SchedulerContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            modelBuilder.Entity<PhoneNumber>()
                .ToTable("PhoneNumber");
            modelBuilder.Entity<PhoneNumber>()
                .Property(p => p.CustomerId)
                .IsRequired();
            modelBuilder.Entity<PhoneNumber>()
                .HasOne(p => p.Customer);

            modelBuilder.Entity<Address>()
                .ToTable("Address");
            modelBuilder.Entity<Address>()
                .Property(a => a.CustomerId)
                .IsRequired();
            modelBuilder.Entity<Address>()
                .HasOne(p => p.Customer);

            modelBuilder.Entity<Customer>()
                .ToTable("Customer");
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Addresses).WithOne(a => a.Customer);
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.PhoneNumbers).WithOne(p => p.Customer);
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Jobs).WithOne(j => j.Customer);
            modelBuilder.Entity<Customer>()
            .Property(s => s.DateCreated)
                .HasDefaultValue(DateTime.Now);
            
            


            //

            modelBuilder.Entity<Job>()
                .ToTable("Job");
            modelBuilder.Entity<Job>()
                .HasMany(j => j.WorkDescriptions).WithOne(w => w.Job);
            //modelBuilder.Entity<Job>()
              //  .HasMany(c => c.JobAssignees);
                
            
            //modelBuilder.Entity<Job>();                
               /*  
              modelBuilder.Entity<JobAssignee>()
                  .ToTable("JobAssignee");
              modelBuilder.Entity<JobAssignee>()
                  .HasOne(ja => ja.Job).WithMany(j => j.JobAssignees)
                  .HasForeignKey(ja => ja.JobId);
              modelBuilder.Entity<JobAssignee>()
                  .HasOne(ja => ja.EndUser).WithMany(e => e.JobAssignees)
                  .HasForeignKey(ja => ja.EndUserId);
                  */

            modelBuilder.Entity<JobAssignee>()
                .HasKey(c => new { c.EndUserId, c.JobId });
            modelBuilder.Entity<JobAssignee>()
                .HasOne(c => c.Job).WithMany(c => c.JobAssignees).HasForeignKey(c => c.JobId);
            modelBuilder.Entity<JobAssignee>()
                .HasOne(c => c.EndUser).WithMany(c => c.JobAssignees).HasForeignKey(c => c.EndUserId);
            modelBuilder.Entity<JobAssignee>()
                .ToTable("JobAssignee");
            //modelBuilder.Entity<JobAssignee>()
               // .Property(s => s.EndUserId).ValueGeneratedOnAdd();
            //modelBuilder.Entity<JobAssignee>()
                //.Property(s => s.JobId).ValueGeneratedOnAdd();



            modelBuilder.Entity<EndUser>()
                .ToTable("EndUser");
            //modelBuilder.Entity<EndUser>()
            //  .HasMany(c => c.JobAssignees).WithOne(c);


            modelBuilder.Entity<Lead>()
                .ToTable("Lead");
            modelBuilder.Entity<Lead>()
                .Property(l => l.Name).IsRequired();
            modelBuilder.Entity<Lead>()
                .HasIndex(l => l.Name).IsUnique();
            //modelBuilder.Entity<Lead>()
              //  .HasMany(l => l.Customers);




            modelBuilder.Entity<WorkDescription>()
                .ToTable("WorkDescription");
            modelBuilder.Entity<WorkDescription>()
                .Property(w => w.JobId).IsRequired();
            modelBuilder.Entity<WorkDescription>()
                .HasOne(w => w.Job);
            


            modelBuilder.Entity<Schedule>()
                .ToTable("Schedule");

            modelBuilder.Entity<Schedule>()
                .Property(s => s.CreatorId)
                .IsRequired();

            modelBuilder.Entity<Schedule>()
                .Property(s => s.DateCreated)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Schedule>()
                .Property(s => s.DateUpdated)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Schedule>()
                .Property(s => s.Type)
                .HasDefaultValue(ScheduleType.Work);

            modelBuilder.Entity<Schedule>()
                .Property(s => s.Status)
                .HasDefaultValue(ScheduleStatus.Valid);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Creator)
                .WithMany(x => x.SchedulesCreated);

            modelBuilder.Entity<User>()
              .ToTable("User");

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Attendee>()
              .ToTable("Attendee");

            modelBuilder.Entity<Attendee>()
                .HasOne(a => a.User)
                .WithMany(u => u.SchedulesAttended)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Attendee>()
                .HasOne(a => a.Schedule)
                .WithMany(s => s.Attendees)
                .HasForeignKey(a => a.ScheduleId);

        }
    }
}
