using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarframeAPI.Models
{
    public class Primary
    {
        public int id { get; set; }
        public string name { get; set; }
        public string trigger { get; set; }
        public double dmg { get; set; }
        public string dmgType { get; set; }
        public string critChance { get; set; }
        public string critDmgMultiplier { get; set; }
        public string statusChance { get; set; }
        public double fireRate { get; set; }
        public string rivenDispo { get; set; }
        public string masteryReq { get; set; }
        public int magSize { get; set; }
        public string ammoCap { get; set; }
        public string reloadSpeed { get; set; }
        public string projectileType { get; set; }
        public string punchThrough { get; set; }
        public string accuracy { get; set; }
        public string intro { get; set; }
    }
}
