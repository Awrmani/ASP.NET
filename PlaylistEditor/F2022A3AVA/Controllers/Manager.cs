using AutoMapper;
using F2022A3AVA.Data;
using F2022A3AVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// ************************************************************************************
// WEB524 Project Template V1 == 2227-cd2793a8-63aa-42b6-b58c-665b9e7d2e8b
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace F2022A3AVA.Controllers
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

                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();

                cfg.CreateMap<MediaType, MediaTypeBaseViewModel>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();

                cfg.CreateMap<Playlist, PlaylistBaseViewModel>();
                cfg.CreateMap<Playlist, PlaylistWithDetailViewModel>();
                cfg.CreateMap<PlaylistBaseViewModel, PlaylistEditTracksFormViewModel>();
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
        public IEnumerable<TrackWithDetailViewModel> TrackGetAllWithDetails()
        {
            var tracks = ds.Tracks.Include("Album")
                                  .Include("Album.Artist")
                                  .Include("MediaType")
                                  .OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(tracks);
        }

        public TrackWithDetailViewModel TrackGetOne(int id) 
        {
            var track = ds.Tracks.Include("Album")
                                 .Include("Album.Artist")
                                 .Include("MediaType")
                                 .FirstOrDefault(i => i.TrackId == id);

            return track == null ? null :
                mapper.Map<Track, TrackWithDetailViewModel>(track);
        }

        public TrackWithDetailViewModel TrackAdd(TrackAddViewModel newTrack) 
        {
            var album = ds.Albums.Find(newTrack.AlbumId);
            var mediaType = ds.MediaTypes.Find(newTrack.MediaTypeId);

            if (album == null || mediaType == null)
                return null;
            else
            {
                var addedTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));

                addedTrack.Album = album;
                addedTrack.MediaType = mediaType;
                ds.SaveChanges();

                return addedTrack == null ? null :
                    mapper.Map<Track, TrackWithDetailViewModel>(addedTrack);
            }
        }

        public IEnumerable<ArtistBaseViewModel> ArtistGetAll() 
        {
            var artists = ds.Artists.OrderBy(a => a.Name);

            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(artists);
        }

        public IEnumerable<AlbumBaseViewModel> AlbumGetAll() 
        {
            var albums = ds.Albums.OrderBy(a => a.Title);

            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(albums);
        }

        public AlbumBaseViewModel AlbumGetOne(int id) 
        {
            var album = ds.Albums.Find(id);

            return album == null ? null :
                mapper.Map<Album, AlbumBaseViewModel>(album);
        }

        public IEnumerable<MediaTypeBaseViewModel> MediaTypeGetAll() 
        {
            var mediaTypes = ds.MediaTypes.OrderBy(m => m.Name);

            return mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBaseViewModel>>(mediaTypes);
        }

        // ############################ Playlist Functions ############################
        public IEnumerable<PlaylistBaseViewModel> PlaylistGetAll() 
        {
            var playlists = ds.Playlists.Include("Tracks")
                                       .OrderBy(p => p.Name);

            return mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistBaseViewModel>>(playlists);
        }

        public PlaylistWithDetailViewModel PlaylistGetById(int id) 
        {
            var playlist = ds.Playlists.Include("Tracks")
                                       .FirstOrDefault(i => i.PlaylistId == id);

            return playlist == null ? null :
                mapper.Map<Playlist, PlaylistWithDetailViewModel>(playlist);
        }

        public PlaylistWithDetailViewModel PlaylistEditTracks(PlaylistEditTracksViewModel newPlaylist) 
        {
            var playlist = ds.Playlists.Include("Tracks")
                                       .FirstOrDefault(i => i.PlaylistId == newPlaylist.PlaylistId);

            if (playlist == null)
                return null;
            else
            {
                playlist.Tracks.Clear();

                foreach(var trackId in newPlaylist.TrackIds)
                {
                    var Track = ds.Tracks.Find(trackId);
                    playlist.Tracks.Add(Track);
                }
                ds.SaveChanges();

                return mapper.Map<Playlist, PlaylistWithDetailViewModel>(playlist);
            }

        }
    }
}