using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A3AVA.Controllers
{
    public class MediaTypesController : Controller
    {
        private Manager m = new Manager();

        // GET: MediaTypes
        public ActionResult Index()
        {
            var mediatypes = m.MediaTypeGetAll();

            return View(mediatypes);
        }
    }
}
