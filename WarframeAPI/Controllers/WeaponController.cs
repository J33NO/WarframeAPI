using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WarframeAPI.DAL;
using WarframeAPI.Models;
using WarframeAPI.Scrapers;

namespace WarframeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {

        [HttpGet]
        [Route("GetAllPrimary")]
        public List<Weapons> GetAllPrimary()
        {
            using (var ctx = new LocalContext())
            {
                var weapons = (from w in ctx.Weapon
                               select w).ToList();

                if(weapons != null)
                {
                    return weapons;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
