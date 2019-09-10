using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    public class ContentCacheController : Controller
    {

        public IActionResult PurgeCache()
        {
            return Ok();
        }

        public IActionResult GetCacheAge()
        {
            return Ok();
        }
    }
}
