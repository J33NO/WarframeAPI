using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeleniumCSharpHelper;
using System.Collections.ObjectModel;
using WarframeAPI.Models;
using Microsoft.Extensions.Configuration;
using WarframeAPI.DAL;
using System.Data.Entity.Migrations;
using Microsoft.Extensions.Options;

namespace WarframeAPI.Scrapers
{
    public class PrimaryScraper
    {
        private readonly LocalContext _context;
        public IWebDriver driver;
        private readonly MyConfiguration _myConfiguration;



        public PrimaryScraper(LocalContext ctx, MyConfiguration myConfiguration)
        {
            _context = ctx;
            _myConfiguration = myConfiguration;

        }

        public void ScrapePrimaryInfo()
        {
            string appUrl = _myConfiguration.PrimaryWeaponUrl;
            int i = 1;
            List<Primary> weapons = new List<Primary>();
            WebDriverExtensions.Browser_NavigateChrome(ref driver, appUrl, title: "Weapon Comparison | WARFRAME Wiki | Fandom", maximize: true);
            By weaponLink = By.XPath("//*[@id='tabber-0cddd21e4906375db194dc7c826bbe9e']/div[1]/div/div/table/tbody/tr");
            ReadOnlyCollection<IWebElement> elements = driver.FindElements(weaponLink);


            foreach (IWebElement weaponElement in elements)
            {
                string path = "//*[@id='tabber-0cddd21e4906375db194dc7c826bbe9e']/div[1]/div/div/table/tbody/tr[" + i + "]";
                By weaponName = By.XPath(path + "/td[1]/a");
                By weaponTrigger = By.XPath(path + "/td[2]");
                By weaponDmg = By.XPath(path + "/td[3]");
                By weaponCritChance = By.XPath(path + "/td[4]");
                By weaponCritDmgMultiplier = By.XPath(path + "/td[5]");
                By weaponStatusChance = By.XPath(path + "/td[6]");
                By weaponFireRate = By.XPath(path + "/td[7]");
                By weaponRivenDispo = By.XPath(path + "/td[8]");
                By weaponMasteryReq = By.XPath(path + "/td[9]");
                By weaponMagSize = By.XPath(path + "/td[10]");
                By weaponAmmoCap = By.XPath(path + "/td[11]");
                By weaponReloadSpd = By.XPath(path + "/td[12]");
                By weaponProjType = By.XPath(path + "/td[13]");
                By weaponPunchThrough = By.XPath(path + "/td[14]");
                By weaponAccuracy = By.XPath(path + "/td[15]");
                By weaponIntro = By.XPath(path + "/td[16]/a");

                string weapon_critChance = weaponElement.FindElement(weaponCritChance).Text == "N/A" ? "0" : weaponElement.FindElement(weaponCritChance).Text;
                string weapon_statusChance = weaponElement.FindElement(weaponStatusChance).Text;

                Primary weapon = new Primary
                {
                    name = weaponElement.FindElement(weaponName).Text,
                    trigger = weaponElement.FindElement(weaponTrigger).Text,
                    dmg = Convert.ToDouble(weaponElement.FindElement(weaponDmg).Text),
                    critChance = (Convert.ToDouble(weapon_critChance.Replace("%", "")) / 100),
                    critDmgMultiplier = weaponElement.FindElement(weaponCritDmgMultiplier).Text,
                    statusChance = (Convert.ToDouble(weapon_statusChance.Replace("%", "")) / 100),
                    fireRate = Convert.ToDouble(weaponElement.FindElement(weaponFireRate).Text),
                    rivenDispo = Convert.ToDouble(weaponElement.FindElement(weaponRivenDispo).Text == "N/A" ? "0" : weaponElement.FindElement(weaponRivenDispo).Text),
                    masteryReq = Convert.ToInt32(weaponElement.FindElement(weaponMasteryReq).Text == "N/A" ? "0" : weaponElement.FindElement(weaponMasteryReq).Text),
                    magSize = Convert.ToInt32(weaponElement.FindElement(weaponMagSize).Text),
                    ammoCap = Convert.ToInt32(weaponElement.FindElement(weaponAmmoCap).Text == "N/A" ? "0" : weaponElement.FindElement(weaponAmmoCap).Text),
                    reloadSpeed = weaponElement.FindElement(weaponReloadSpd).Text,
                    projectileType = weaponElement.FindElement(weaponProjType).Text,
                    punchThrough = weaponElement.FindElement(weaponPunchThrough).Text,
                    accuracy = Convert.ToDouble(weaponElement.FindElement(weaponAccuracy).Text == "N/A" ? "0" : weaponElement.FindElement(weaponAccuracy).Text),
                    intro = weaponElement.FindElement(weaponIntro).Text
                };
                weapons.Add(weapon);
                i++;
            }
            driver.Close();
            foreach(Primary weapon in weapons)
            {
                _context.Primary.Update(weapon);
            }
            _context.SaveChanges();
        }

        public bool DataNeedsToBeScraped(string category)
        {
            DateTime minDate = new DateTime(1992, 2, 28);
            DateTime today = DateTime.Today;
            ScrapeData scrapeData = _context.ScrapeData.SingleOrDefault(x => x.category == category);
            DateTime lastScraped = scrapeData is null ? minDate : Convert.ToDateTime(scrapeData.lastScrapeDate);

            if (lastScraped == minDate)
            {
                //Never been scraped, go seed db.
                return true;
            }
            else if (today.Day - lastScraped.Day >= 4)
            {
                //At least 4 days since last update, go scrape.
                return true;
            }
            else
            {
                //Data has been scraped within the last day, do nothing.
                return false;
            }
        }

        public void UpdateScrapeData(string category)
        {
            ScrapeData scrapeData = _context.ScrapeData.Where(x => x.category == category).FirstOrDefault();
            if (scrapeData == null)
            {
                scrapeData = new ScrapeData();
            }
            scrapeData.lastScrapeDate = DateTime.Today;
            scrapeData.category = category;
            _context.ScrapeData.Update(scrapeData);
            _context.SaveChanges();
        }
    }
}
