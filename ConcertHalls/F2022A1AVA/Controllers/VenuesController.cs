using F2022A1AVA.Data;
using F2022A1AVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A1AVA.Controllers
{
    public class VenuesController : Controller
    {
        private Manager m = new Manager();
        // GET: Venues
        public ActionResult Index()
        {
            var Venues = m.VenueGetAll();
            return View(Venues);
        }

        // GET: Venues/Details/5
        public ActionResult Details(int? id)
        {
            var venue = m.VenueGetOne(id.GetValueOrDefault());
            if (venue == null)
                return HttpNotFound();
            return View(venue);
        }

        // GET: Venues/Create
        public ActionResult Create()
        {
            VenueAddViewModel venue = new VenueAddViewModel();
            return View(venue);
        }

        // POST: Venues/Create
        [HttpPost]
        public ActionResult Create(VenueAddViewModel venue)
        {
            if (!ModelState.IsValid)
                return View(venue);
            var newVenue = m.VenueAdd(venue);
            try
            {
                if (newVenue == null)
                    return View(venue);

                return RedirectToAction("Details", new { id = newVenue.VenueId });
            }
            catch
            {
                return View(venue);
            }
        }

        // GET: Venues/Edit/5
        public ActionResult Edit(int? id)
        {
            var venue = m.VenueGetOne(id.GetValueOrDefault());
            if (venue == null)
                return HttpNotFound();

            var formObj = m.mapper.Map<VenueBaseViewModel, VenueEditFormViewModel>(venue);
            return View(formObj);
        }

        // POST: Venues/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, VenueEditViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Edit", new { id = model.VenueId });

            if (model.VenueId != m.VenueGetOne(id.GetValueOrDefault()).VenueId)
                return RedirectToAction("Index");

                var editVenue = m.VenueEdit(model);
                if (editVenue == null)
                    return RedirectToAction("Edit", new { id = model.VenueId });

                return RedirectToAction("Details", new { id = model.VenueId });
        }

        // GET: Venues/Delete/5 
        public ActionResult Delete(int? id)
        {
            var venue = m.VenueGetOne(id.GetValueOrDefault());
            if (venue == null)
                return RedirectToAction("Index");

            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var res = m.VenueDelete(id.GetValueOrDefault());

            return RedirectToAction("Index");
        }
    }
}
