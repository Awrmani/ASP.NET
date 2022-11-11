using F2022A3AVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A3AVA.Controllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();

        // GET: Tracks
        public ActionResult Index()
        {
            var tracks = m.TrackGetAllWithDetails();
            return View(tracks);
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            var track = m.TrackGetOne(id.GetValueOrDefault());
            if (track == null)
                return HttpNotFound();

            return View(track);
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            var form = new TrackAddFormViewModel();

            form.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");
            form.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");

            return View(form);
        }

        // POST: Tracks/Create
        [HttpPost]
        public ActionResult Create(TrackAddViewModel newTrack)
        {
            if (!ModelState.IsValid)
                return View(newTrack);

            var addedTrack = m.TrackAdd(newTrack);

            if (addedTrack == null)
                return View(newTrack);
            else
                return RedirectToAction("details", new { id = addedTrack.TrackId });
        }
    }
}
