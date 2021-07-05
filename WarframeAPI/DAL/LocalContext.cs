using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using WarframeAPI.Extensions;
using WarframeAPI.Models;

namespace WarframeAPI.DAL
{
    public class LocalContext : DbContext
    {
        public LocalContext(DbContextOptions<LocalContext> options)
        : base(options)
        {

        }

        public DbSet<Primary> Primary { get; set; }
        public DbSet<ScrapeData> ScrapeData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();
        }
    }
}
