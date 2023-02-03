using F2022A5AVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A5AVA.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {
        private Manager m = new Manager();
        // GET: Artist
        public ActionResult Index()
        {
            var artists = m.ArtistGetAll();
            return View(artists);
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
            var artist = m.ArtistGetOne(id.GetValueOrDefault());
            if (artist == null)
                return HttpNotFound();

            return View(artist);
        }

        // GET: Artist/Create
        [Authorize (Roles = "Executive")]
        public ActionResult Create()
        {
            var form = new ArtistAddFormViewModel();
            var genres = m.GenreGetAll();

            form.GenreList = new SelectList(genres,
                                            dataValueField: "Id",
                                            dataTextField: "Name");
            return View(form);
        }

        // POST: Artist/Create
        [HttpPost]
        public ActionResult Create(ArtistAddViewModel newArtist)
        {
            if (!ModelState.IsValid)
                return View(newArtist);

            var addedArtist = m.ArtistAdd(newArtist);
            if (addedArtist == null)
                return View(newArtist);

            return RedirectToAction("details", new { id = addedArtist.Id });
        }

        // GET: Album/Create
        [Authorize(Roles = "Coordinator")]
        [Route("Artist/{id}/AddAlbum")]
        public ActionResult AddAlbum(int? id)
        {
            var artist = m.ArtistGetOne(id.GetValueOrDefault());
            if (artist == null)
                return HttpNotFound();

            var form = new AlbumAddFormViewModel
            {
                ArtistName = artist.Name
            };

            var genres = m.GenreGetAll();
            var artists = m.ArtistGetAll();
            var artistIds = new int[] { artist.Id };
            var tracks = m.TrackGetAllByArtistId(artist.Id);

            form.GenreList = new SelectList(genres,
                                            dataValueField: "Id",
                                            dataTextField: "Name");

            form.ArtistList = new MultiSelectList(artists,
                                                  dataValueField: "Id",
                                                  dataTextField: "Name",
                                                  selectedValues: artistIds);

            form.TrackList = new MultiSelectList(tracks,
                                                 dataValueField: "Id",
                                                 dataTextField: "Name");

            return View(form);
        }

        // POST: Album/Create
        [HttpPost]
        [Route("Artist/{id}/AddAlbum")]
        public ActionResult AddAlbum(AlbumAddViewModel newAlbum)
        {
            if (!ModelState.IsValid)
                return View(newAlbum);

            var addedAlbum = m.AlbumAdd(newAlbum);
            if (addedAlbum == null)
                return View(newAlbum);

            return RedirectToAction("details", "Album", new { id = addedAlbum.Id });
        }
    }
}
