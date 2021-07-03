using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarframeAPI.Models
{

    public class ScrapeData
    {
        public int id { get; set; }
        public DateTime lastScrapeDate { get; set; }
        public string category { get; set; }
    }
}
