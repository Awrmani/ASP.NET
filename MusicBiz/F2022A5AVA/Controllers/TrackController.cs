using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A5AVA.Controllers
{
    [Authorize]
    public class TrackController : Controller
    {
        private Manager m = new Manager();

        // GET: Track
        public ActionResult Index()
        {
            var tracks = m.TrackGetAll();
            return View(tracks);
        }

        // GET: Track/Details/5
        public ActionResult Details(int? id)
        {
            var tracks = m.TrackGetOne(id.GetValueOrDefault());
            if (tracks == null)
                return HttpNotFound();

            return View(tracks);
        }
    }
}
