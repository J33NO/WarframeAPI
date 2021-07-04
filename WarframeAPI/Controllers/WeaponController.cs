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
        public PrimaryScraper scraper = new PrimaryScraper();

        [HttpGet]
        [Route("GetAllPrimary")]
        public List<Primary> GetAllPrimary()
        {
            bool updatePrimary = scraper.DataNeedsToBeScraped("Primary");
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
                scraper.ScrapePrimaryInfo();
                scraper.UpdateScrapeData("Primary");
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

        [HttpGet]
        [Route("GetPrimary/{name}")]
        public Primary GetPrimary(string name)
        {
            bool updatePrimary = scraper.DataNeedsToBeScraped("Primary");
            if (updatePrimary == false)
            {
                Primary primaryWeapon = ctx.Primary.Where(x => x.name == name).FirstOrDefault();
                if (primaryWeapon != null)
                {
                    return primaryWeapon;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                scraper.ScrapePrimaryInfo();
                scraper.UpdateScrapeData("Primary");
                Primary primaryWeapon = ctx.Primary.Where(x => x.name == name).FirstOrDefault();
                if (primaryWeapon != null)
                {
                    return primaryWeapon;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
