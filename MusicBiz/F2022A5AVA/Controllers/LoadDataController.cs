using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A5AVA.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class LoadDataController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: LoadData
        public ActionResult Index()
        {
            if (m.LoadData())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        public ActionResult Genre()
        {
            if (m.LoadDataGenre())
            {
                ViewBag.Result = "Genre Data was Loaded";
            }
            else
            {
                ViewBag.Result = "(Done)";
            }

            return View("result");
        }

        public ActionResult Artist()
        {
            if (m.LoadDataArtist())
            {
                ViewBag.Result = "Artist Data was Loaded";
            }
            else
            {
                ViewBag.Result = "(Done)";
            }

            return View("result");
        }

        public ActionResult Album()
        {
            if (m.LoadDataAlbum())
            {
                ViewBag.Result = "Album Data was Loaded";
            }
            else
            {
                ViewBag.Result = "(Done)";
            }

            return View("result");
        }

        public ActionResult Track()
        {
            if (m.LoadDataTrack())
            {
                ViewBag.Result = "Track Data was Loaded";
            }
            else
            {
                ViewBag.Result = "(Done)";
            }

            return View("result");
        }

        public ActionResult Remove()
        {
            if (m.RemoveData())
            {
                ViewBag.Result = "Data was Removed";
            }
            else
            {
                ViewBag.Result = "(Done)";
            }

            return View("result");
        }

        public ActionResult RemoveDatabase()
        {
            if (m.RemoveDatabase())
            {
                return Content("database has been removed");
            }
            else
            {
                return Content("could not remove database");
            }
        }

    }
}