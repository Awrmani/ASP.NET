using F2022A5AVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A5AVA.Controllers
{
    [Authorize]
    public class AlbumController : Controller
    {
        private Manager m = new Manager();
        
        // GET: Album
        public ActionResult Index()
        {
            var albums = m.AlbumGetAll();
            return View(albums);
        }

        // GET: Album/Details/5
        public ActionResult Details(int? id)
        {
            var album = m.AlbumGetOne(id.GetValueOrDefault());
            if (album == null)
                return HttpNotFound();

            return View(album);
        }

        // GET: Album/Create
        [Authorize (Roles = "Clerk")]
        [Route("Album/{id}/AddTrack")]
        public ActionResult AddTrack(int? id)
        {
            var album = m.AlbumGetOne(id.GetValueOrDefault());
            if (album == null)
                return HttpNotFound();

            var form = new TrackAddFormViewModel
            {
                AlbumId = album.Id,
                AlbumName = album.Name
            };

            var genres = m.GenreGetAll();
            form.GenreList = new SelectList(genres,
                                            dataValueField: "Id",
                                            dataTextField: "Name");

            return View(form);
        }

        // POST: Album/Create
        [HttpPost]
        [Route("Album/{id}/AddTrack")]
        public ActionResult AddTrack(TrackAddViewModel newTrack)
        {
            if (!ModelState.IsValid)
                return View(newTrack);

            var addedTrack = m.TrackAdd(newTrack);
            if (addedTrack == null)
                return View(newTrack);

            return RedirectToAction("details", "Track", new { id = addedTrack.Id });
        }
    }
}
