using F2022A3AVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A3AVA.Controllers
{
    public class PlaylistsController : Controller
    {
        private Manager m = new Manager();
        // GET: Playlists
        public ActionResult Index()
        {
            var playlists = m.PlaylistGetAll();
            return View(playlists);
        }

        // GET: Playlists/Details/5
        public ActionResult Details(int? id)
        {
            var playlist = m.PlaylistGetById(id.GetValueOrDefault());
            if (playlist == null)
                return HttpNotFound();

            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public ActionResult Edit(int? id)
        {
            var playlist = m.PlaylistGetById(id.GetValueOrDefault());

            if (playlist == null)
                return HttpNotFound();
            else
            {
                var form = m.mapper.Map<PlaylistEditTracksFormViewModel>(playlist);

                var selectedValues = playlist.Tracks.Select(tr => tr.TrackId);

                form.TrackList = new MultiSelectList
                    (items: m.TrackGetAllWithDetails(),
                    dataValueField: "TrackId",
                    dataTextField: "NameFull",
                    selectedValues: selectedValues);

                form.Tracks = playlist.Tracks;

                return View(form);
            }
        }

        // POST: Playlists/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracksViewModel newPlaylist)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("edit", new { id = newPlaylist.PlaylistId });

            if (id.GetValueOrDefault() != newPlaylist.PlaylistId)
                return RedirectToAction("index");

            var editedPlaylist = m.PlaylistEditTracks(newPlaylist);


            if (editedPlaylist == null)
                return RedirectToAction("index");
            else
            {
                return RedirectToAction("details", "Playlists", new { id = newPlaylist.PlaylistId });
            }

        }
    }
}
