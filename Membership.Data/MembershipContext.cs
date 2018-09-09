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

            //optionsBuilder
            //    .UseLazyLoadingProxies();
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


            modelBuilder.Entity<Member>(m =>
            {
                m.HasData(new[]
                {
                    new Member
                    {
                        Id = 1,
                        Email = "donald@hush.com",
                        MembershipNumber = 2001,
                        Name = "Donald",
                        ClubId = 1
                    },
                    new Member
                    {
                        Id = 2,
                        Email = "dolly@hush.com",
                        MembershipNumber = 2002,
                        Name = "Dolly",
                        ClubId = 1
                    },
                    new Member
                    {
                        Id = 3,
                        Email = "goofy@hush.com",
                        MembershipNumber = 2003,
                        Name = "Goofy",
                        ClubId = 2
                    },
                    new Member
                    {
                        Id = 4,
                        Email = "pluto@hush.com",
                        MembershipNumber = 2004,
                        Name = "Pluto",
                        ClubId = 3
                    }
                });
            });


        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Club> Clubs { get; set; }
    }
}
