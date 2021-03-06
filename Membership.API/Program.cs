﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Membership.API.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Membership.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Run();
            //BuildWebHost(args).Run();
        }

        public static IHost CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder => 
                { builder.UseStartup<Startup>();})
            .Build();

            using ( var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<MembershipContext>();
                context.Database.EnsureCreated();
            }

            return host;
        }
    }
}
