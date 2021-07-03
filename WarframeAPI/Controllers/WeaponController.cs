using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WarframeAPI.DAL;
using WarframeAPI.Models;
using WarframeAPI.Scrapers;

namespace WarframeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        public LocalContext ctx = new LocalContext();

        [HttpGet]
        [Route("GetAllPrimary")]
        public List<Primary> GetAllPrimary()
        {
            bool updatePrimary = ScraperController.DataNeedsToBeScraped("Primary");
            if (updatePrimary == false)
            {
                List<Primary> primaryWeapons = ctx.Primary.ToList();
                if(primaryWeapons != null)
                {
                    return primaryWeapons;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                WeaponScraper.ScrapePrimaryInfo();
                ScraperController.UpdateScrapeData("Primary");
                List<Primary> primaryWeapons = ctx.Primary.ToList();
                if (primaryWeapons != null)
                {
                    return primaryWeapons;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
