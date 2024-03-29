﻿using AutoMapper;
using F2022A1AVA.Data;
using F2022A1AVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// ************************************************************************************
// WEB524 Project Template V1 == 2227-33302760-ed57-42bb-add9-541a93c58b0a
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace F2022A1AVA.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();
                cfg.CreateMap<Venue, VenueBaseViewModel>();
                cfg.CreateMap<VenueAddViewModel, Venue>();
                cfg.CreateMap<VenueBaseViewModel, VenueEditFormViewModel>();
                //cfg.CreateMap<Venue, VenueEditViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }


        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().
        public IEnumerable<VenueBaseViewModel> VenueGetAll()
        {
            VenueBaseViewModel v = new VenueBaseViewModel();
            var venues = mapper.Map<IEnumerable<Venue>, IEnumerable<VenueBaseViewModel>>(ds.Venues).OrderBy(p => p.Company);
            //venues.OrderBy<ds.Venues>(v.Company);
            return venues;
        }

        public VenueBaseViewModel VenueGetOne(int id)
        {
            var venue = ds.Venues.Find(id);
            return venue == null ? null : mapper.Map<Venue, VenueBaseViewModel>(venue);
        }

        public VenueBaseViewModel VenueAdd(VenueAddViewModel newVenue)
        {
            var addItem = ds.Venues.Add(mapper.Map<VenueAddViewModel, Venue>(newVenue));
            ds.SaveChanges();

            return addItem == null ? null : mapper.Map<Venue, VenueBaseViewModel>(addItem);
        }

        public VenueBaseViewModel VenueEdit(VenueEditViewModel venueEdit)
        {
            var venue = ds.Venues.Find(venueEdit.VenueId);
            if (venue == null)
                return null;
            else
            {
                ds.Entry(venue).CurrentValues.SetValues(venueEdit);
                ds.SaveChanges();

                return mapper.Map<Venue, VenueBaseViewModel>(venue);
            }
        }

        public bool VenueDelete(int id)
        {
            var item = ds.Venues.Find(id);
            if (item == null)
                return false;
            
            ds.Venues.Remove(item);
            ds.SaveChanges();
            return true;
        }
    }
}