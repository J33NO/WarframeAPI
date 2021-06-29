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

namespace WarframeAPI.Scrapers
{
    public class WeaponScraper
    {
        public static IWebDriver driver;

        public WeaponScraper()
        {

        }

        public List<Weapons> GetPrimaryInfo()
        {
            string appUrl = "https://warframe.fandom.com/wiki/Weapon_Comparison";
            int i = 1;
            List<Weapons> weapons = new List<Weapons>();
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

                IWebElement weapon_name = weaponElement.FindElement(weaponName);
                IWebElement weapon_trigger = weaponElement.FindElement(weaponTrigger);
                IWebElement weapon_dmg = weaponElement.FindElement(weaponDmg);
                IWebElement weapon_critChance = weaponElement.FindElement(weaponCritChance);
                IWebElement weapon_critDmgMult = weaponElement.FindElement(weaponCritDmgMultiplier);
                IWebElement weapon_statusChance = weaponElement.FindElement(weaponStatusChance);
                IWebElement weapon_fireRate = weaponElement.FindElement(weaponFireRate);
                IWebElement weapon_rivenDispo = weaponElement.FindElement(weaponRivenDispo);
                IWebElement weapon_masteryReq = weaponElement.FindElement(weaponMasteryReq);
                IWebElement weapon_magSize = weaponElement.FindElement(weaponMagSize);
                IWebElement weapon_ammoCap = weaponElement.FindElement(weaponAmmoCap);
                IWebElement weapon_reloadSpd = weaponElement.FindElement(weaponReloadSpd);
                IWebElement weapon_projType = weaponElement.FindElement(weaponProjType);
                IWebElement weapon_punchThrough = weaponElement.FindElement(weaponPunchThrough);
                IWebElement weapon_accuracy = weaponElement.FindElement(weaponAccuracy);
                IWebElement weapon_intro = weaponElement.FindElement(weaponIntro);

                Weapons weapon = new Weapons
                {
                    name = weapon_name.Text,
                    trigger = weapon_trigger.Text,
                    dmg = Convert.ToDouble(weapon_dmg.Text),
                    critChance = weapon_critChance.Text,
                    critDmgMultiplier = weapon_critDmgMult.Text,
                    statusChance = weapon_statusChance.Text,
                    fireRate = Convert.ToDouble(weapon_fireRate.Text),
                    rivenDispo = weapon_rivenDispo.Text,
                    masteryReq = weapon_masteryReq.Text,
                    magSize = Convert.ToInt32(weapon_magSize.Text),
                    ammoCap = weapon_ammoCap.Text,
                    reloadSpeed = weapon_reloadSpd.Text,
                    projectileType = weapon_projType.Text,
                    punchThrough = weapon_punchThrough.Text,
                    accuracy = weapon_accuracy.Text,
                    intro = weapon_intro.Text
                };
                weapons.Add(weapon);
                i++;
            }
            driver.Close();
            return weapons;

        }
    }
}
