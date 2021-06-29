using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using WarframeAPI.Models;

namespace WarframeAPI.DAL
{
    public class LocalContext : DbContext
    {
        public LocalContext() : base("Server=.;Database=Warframe;Trusted_Connection=True;")
        {
        }

        public DbSet<Weapons> Weapon { get; set; }
    }
}
