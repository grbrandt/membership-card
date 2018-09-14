using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Membership.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Membership.API.Models
{
    public class MembershipContext : DbContext
    {
        public MembershipContext(DbContextOptions<MembershipContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Club>(c =>
            {
                c.HasData(
                    new Club
                    {
                        Id = 1,
                        Name = "The Magicians",
                    },
                    new Club
                    {
                        Id = 2,
                        Name = "Amazing Gumball",
                    },
                    new Club
                    {
                        Id = 3,
                        Name = "The Wart",
                    }
                    );
            });

            modelBuilder.Entity<Club>().OwnsOne(p => p.Settings)
                .HasData(
                new
                {
                    ClubId = 1,
                    QRDefaultValidityPeriod = TimeSpan.FromMinutes(10)
                },
                new
                {
                    ClubId = 2,
                    QRDefaultValidityPeriod = TimeSpan.FromMinutes(10)

                },
                new
                {
                    ClubId = 3,
                    QRDefaultValidityPeriod = TimeSpan.FromMinutes(10)

                });


            modelBuilder.Entity<Person>(m =>
            {
                m.HasData(new[]
                {
                    new Person
                    {
                        Id = 1,
                        Email = "donald@hush.com",
                        Name = "Donald",
                    },
                    new Person
                    {
                        Id = 2,
                        Email = "dolly@hush.com",
                        Name = "Dolly",
                    },
                    new Person
                    {
                        Id = 3,
                        Email = "goofy@hush.com",
                        Name = "Goofy",
                    },
                    new Person
                    {
                        Id = 4,
                        Email = "pluto@hush.com",
                        Name = "Pluto",
                    }
                });
            });

            modelBuilder.Entity<Data.Entities.Membership>()
                .HasKey(c => new { c.ClubId, c.MemberId });

            //modelBuilder.Entity<Data.Entities.Membership>()
            //    .HasOne(cl => cl.Club)
            //    .WithMany(c => c.Memberships)
            //    .HasForeignKey(cl => cl.ClubId);

            //modelBuilder.Entity<Data.Entities.Membership>()
            //    .HasOne(m => m.Member)
            //    .WithMany(mc => mc.Memberships)
            //    .HasForeignKey(m => m.MemberId);
                
                

        }

        public DbSet<Person> Members { get; set; }
        public DbSet<Club> Clubs { get; set; }
    }
}
