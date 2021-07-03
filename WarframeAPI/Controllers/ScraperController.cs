using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using WarframeAPI.DAL;
using WarframeAPI.Models;

namespace WarframeAPI.Controllers
{
    public class ScraperController : ControllerBase
    {
        public static bool DataNeedsToBeScraped(string category)
        {
            DateTime minDate = new DateTime(1992, 2, 28);
            DateTime today = DateTime.Today;
            LocalContext ctx = new LocalContext();
            ScrapeData scrapeData = ctx.ScrapeData.Where(x => x.category == category).FirstOrDefault();
            DateTime lastScraped = scrapeData is null ? minDate : Convert.ToDateTime(scrapeData.lastScrapeDate);

            if (lastScraped == minDate)
            {
                //Never been scraped, go seed db
                return true;
            }
            else if (today.Day - lastScraped.Day >= 1)
            {
                //Greater than 1 day since last scrape, go scrape
                return true;
            }
            else
            {
                //Data has been scraped within the last day, do nothing
                return false;
            }
        }

        public static void UpdateScrapeData(string category)
        {
            var ctx = new LocalContext();
            ScrapeData scrapeData = ctx.ScrapeData.Where(x => x.category == category).FirstOrDefault();
            if(scrapeData == null)
            {
                scrapeData = new ScrapeData();
            }
            scrapeData.lastScrapeDate = DateTime.Today;
            scrapeData.category = category;
            ctx.ScrapeData.AddOrUpdate(scrapeData);
            ctx.SaveChanges();
        }
    }
}
