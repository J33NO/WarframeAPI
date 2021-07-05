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
        private readonly LocalContext _context;

        public WeaponController(LocalContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        [Route("GetAllPrimary")]
        public List<Primary> GetAllPrimary()
        {
            PrimaryScraper scraper = new PrimaryScraper(_context);
            bool updatePrimary = scraper.DataNeedsToBeScraped("Primary");
            if (updatePrimary == false)
            {
                List<Primary> primaryWeapons = _context.Primary.ToList();
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
                List<Primary> primaryWeapons = _context.Primary.ToList();
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
            PrimaryScraper scraper = new PrimaryScraper(_context);
            bool updatePrimary = scraper.DataNeedsToBeScraped("Primary");
            if (updatePrimary == false)
            {
                Primary primaryWeapon = _context.Primary.Where(x => x.name == name).FirstOrDefault();
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
                Primary primaryWeapon = _context.Primary.Where(x => x.name == name).FirstOrDefault();
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
