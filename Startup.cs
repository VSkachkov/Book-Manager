using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManager.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookManager
{
    public class Startup
    {

        public IConfiguration Config { get; }
        public Startup(IConfiguration config) { Config = config; }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .UseStaticFiles()
                .UseMvc()
                .UseFileServer()
                .Run(async (c) => await c.Response.WriteAsync("Middleware could not handle this request..."));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var cn = @"Server=(localdb)\mssqllocaldb;Database=BookDb;Trusted_Connection=True;ConnectRetryCount=0";
            services
                .AddDbContext<BookContext>(o => o.UseSqlServer(cn))
                .AddMvc();
        }


    }
}
