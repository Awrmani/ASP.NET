using AutoMapper;
using F2022A2AVA.Data;
using F2022A2AVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// ************************************************************************************
// WEB524 Project Template V1 == 2227-82db4399-ece9-430f-8b87-59b7eb0709aa
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace F2022A2AVA.Controllers
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
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<Invoice, InvoiceBaseViewModel>();
                cfg.CreateMap<Invoice, InvoiceWithDetailViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineBaseViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceWithDetailViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineWithDetailViewModel>();
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
        public IEnumerable<TrackWithDetailViewModel> TrackGetAll()
        {
            var tracks = ds.Tracks.Include("Album")
                                  .Include("Genre")
                                  .OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(tracks);
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetBluesJazz()
        {
            var bluesJazz = ds.Tracks.Include("Genre")
                                     .Include("Album")
                                     .Where(t => t.GenreId == 2 || t.GenreId == 6)
                                     .OrderBy(t => t.Genre.Name)
                                     .ThenBy(t => t.Name);

            return bluesJazz == null ? null :
                mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(bluesJazz);
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetCantrellStaley()
        {
            var cantrellStaley = ds.Tracks.Include("Album")
                                          .Include("Genre")
                                          .Where(t => t.Composer.Contains("Jerry Cantrell") && t.Composer.Contains("Layne Staley"))
                                          .OrderBy(t => t.Composer)
                                          .ThenBy(t => t.Name);

            return cantrellStaley == null ? null : 
                mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(cantrellStaley);
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetTop50Longest()
        {
            var top50Longest = ds.Tracks.Include("Album")
                                        .Include("Genre")
                                        .OrderByDescending(t => t.Milliseconds)
                                        .Take(50)
                                        .OrderBy(t => t.Name);

            return top50Longest == null ? null :
                mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(top50Longest);
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetTop50Smallest()
        {
            var top50Smallest = ds.Tracks.Include("Genre")
                                         .Include("Album")
                                         .OrderBy(t => t.Bytes)
                                         .Take(50)
                                         .OrderBy(t => t.Name);

            return top50Smallest == null ? null :
                mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(top50Smallest);
        }

        public IEnumerable<InvoiceBaseViewModel> InvoiceGetAll()
        {
            var invoices = ds.Invoices.OrderByDescending(i => i.InvoiceDate);

            return mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBaseViewModel>>(invoices);
        }

        public InvoiceWithDetailViewModel InvoiceGetByIdWithDetail(int id)
        {
            var invoice = ds.Invoices.Include("Customer.Employee")
                                     .Include("InvoiceLines.Track.Genre")
                                     .Include("InvoiceLines.Track.Album.Artist")
                                     .Include("InvoiceLines.Track.MediaType")
                                     .FirstOrDefault(i => i.InvoiceId == id);

            return invoice == null ? null :
                mapper.Map<Invoice, InvoiceWithDetailViewModel>(invoice);
        }
    }
}