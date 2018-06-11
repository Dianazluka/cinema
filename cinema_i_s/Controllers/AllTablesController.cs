using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cinema_i_s.Controllers
{
    public class AllTablesController : Controller
    {
        [Authorize]
        // GET: AllTables
        public ActionResult Index()
        {
            return View();
        }

    }
}