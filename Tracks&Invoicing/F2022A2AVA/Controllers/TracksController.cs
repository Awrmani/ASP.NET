using F2022A2AVA.Data;
using F2022A2AVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A2AVA.Controllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();
        // GET: Tracks
        public ActionResult Index()
        {
            var tracks = m.TrackGetAll();
            return View(tracks);
        }

        public ActionResult BluesJazz()
        {
            var tracks = m.TrackGetBluesJazz();
            return View("Index", tracks);
        }

        public ActionResult CantrellStaley()
        {
            var tracks = m.TrackGetCantrellStaley();
            return View("Index", tracks);
        }

        public ActionResult Top50Longest()
        {
            var tracks = m.TrackGetTop50Longest();
            return View("Index", tracks);
        }

        public ActionResult Top50Smallest()
        {
            var tracks = m.TrackGetTop50Smallest();
            return View("Index", tracks);
        }

        // GET: Tracks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tracks/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
