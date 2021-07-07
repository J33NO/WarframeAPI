using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using WarframeAPI.DAL;
using WarframeAPI.Models;
using WarframeAPI.Scrapers;

namespace WarframeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrimaryController : ControllerBase
    {
        private readonly LocalContext _context;
        private readonly MyConfiguration _myConfiguration;


        public PrimaryController(LocalContext ctx, MyConfiguration myConfiguration)
        {
            _context = ctx;
            _myConfiguration = myConfiguration;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<Primary> GetAll()
        {
            PrimaryScraper scraper = new PrimaryScraper(_context, _myConfiguration);
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
        [Route("GetSingle/{name}")]
        public Primary GetSingle(string name)
        {
            PrimaryScraper scraper = new PrimaryScraper(_context, _myConfiguration);
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
