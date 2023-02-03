using AutoMapper;
using F2022A5AVA.Data;
using F2022A5AVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

// ************************************************************************************
// WEB524 Project Template V2 == 2227-d1585903-93c8-4917-b5b6-12ede58da067
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace F2022A5AVA.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<Genre, GenreBaseViewModel>();

                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Artist, ArtistWithDetailViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();

                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Album, AlbumWithDetailViewModel>();
                cfg.CreateMap<AlbumAddViewModel, Album>();

                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
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

        // ###################### Genre Methods ######################
        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            var genres = ds.Genres.OrderBy(g => g.Name);

            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(genres);
        }

        // ###################### Artist Methods ######################
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var artists = ds.Artists.OrderBy(a => a.Name);

            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(artists);
        }

        public ArtistWithDetailViewModel ArtistGetOne(int id)
        {
            var artist = ds.Artists.Include("Albums").SingleOrDefault(a => a.Id == id);

            return artist == null ? null :
                mapper.Map<Artist, ArtistWithDetailViewModel>(artist);
        }

        public ArtistBaseViewModel ArtistAdd(ArtistAddViewModel newArtist)
        {
            if (newArtist == null)
                return null;

            var genre = ds.Genres.Find(newArtist.GenreId);

            var addedArtist = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newArtist));
            addedArtist.Executive = HttpContext.Current.User.Identity.Name;
            addedArtist.Genre = genre.Name;

            if (addedArtist.Executive == null)
                return null;

            ds.SaveChanges();

            return addedArtist == null ? null : mapper.Map<Artist, ArtistBaseViewModel>(addedArtist);
        }

        // ###################### Album Methods ######################
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var albums = ds.Albums.OrderBy(a => a.ReleaseDate);

            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(albums);
        }

        public AlbumWithDetailViewModel AlbumGetOne(int id)
        {
            var album = ds.Albums.Include("Artists")
                                 .Include("Tracks")
                                 .SingleOrDefault(a => a.Id == id);

            if (album == null)
                return null;

            var result = mapper.Map<Album, AlbumWithDetailViewModel>(album);
            result.ArtistNames = album.Artists.Select(a => a.Name);
            return result;
        }

        public AlbumWithDetailViewModel AlbumAdd(AlbumAddViewModel newAlbum)
        {
            var genre = ds.Genres.Find(newAlbum.GenreId);

            var artists = new List<Artist>();
            foreach (var id in newAlbum.ArtistIds)
            {
                var findArtist = ds.Artists.Find(id);
                if (findArtist != null)
                    artists.Add(findArtist);
            }

            var tracks = new List<Track>();
            if (newAlbum.TrackIds != null)
            {
                foreach (var id in newAlbum.TrackIds)
                {
                    var findTrack = ds.Tracks.Find(id);
                    if (findTrack != null)
                        tracks.Add(findTrack);
                }
            }

            if (artists == null)
                return null;

            var addedAlbum = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newAlbum));
            addedAlbum.Artists = artists;
            addedAlbum.Tracks = tracks;
            addedAlbum.Genre = genre.Name;
            addedAlbum.Coordinator = HttpContext.Current.User.Identity.Name;

            ds.SaveChanges();

            return addedAlbum == null ? null :
                mapper.Map<Album, AlbumWithDetailViewModel>(addedAlbum);
        }

        // ###################### Track Methods ######################
        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            var tracks = ds.Tracks.OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(tracks);
        }

        public TrackWithDetailViewModel TrackGetOne(int id)
        {
            var track = ds.Tracks.Include("Albums.Artists").SingleOrDefault(t => t.Id == id);

            if (track == null)
                return null;

            var result = mapper.Map<Track, TrackWithDetailViewModel>(track);
            result.AlbumNames = track.Albums.Select(t => t.Name);

            return result;
        }

        public TrackWithDetailViewModel TrackAdd(TrackAddViewModel newTrack)
        {
            var album = ds.Albums.Find(newTrack.AlbumId);
            var genre = ds.Genres.Find(newTrack.GenreId);
            if (album == null || genre == null)
                return null;

            var addedTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));
            addedTrack.Genre = genre.Name;
            addedTrack.Albums.Add(album);
            addedTrack.Clerk = HttpContext.Current.User.Identity.Name;

            ds.SaveChanges();
            return addedTrack == null ? null :
                mapper.Map<Track, TrackWithDetailViewModel>(addedTrack);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllByArtistId(int id)
        {
            var artists = ds.Artists.Include("Albums.Tracks").SingleOrDefault(a => a.Id == id);
            if (artists == null)
                return null;

            var artistTracks = new List<Track>();
            foreach (var album in artists.Albums)
                artistTracks.AddRange(album.Tracks);

            artistTracks = artistTracks.Distinct().ToList();

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>
                (artistTracks.OrderBy(a => a.Name));
        }

        // *** Add your methods above this line **


        #region Role Claims

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        #endregion

        #region Load Data Methods

        // Add some programmatically-generated objects to the data store
        // You can write one method or many methods but remember to
        // check for existing data first.  You will call this/these method(s)
        // from a controller action.


        public void RemoveDataTrack()
        {
            foreach (var e in ds.Tracks)
            {
                ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
            }

            ds.SaveChanges();
        }

        public void RemoveDataGenre()
        {
            foreach (var e in ds.Genres)
            {
                ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
            }

            ds.SaveChanges();
        }

        public void RemoveDataArtist()
        {
            foreach (var e in ds.Artists)
            {
                ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
            }

            ds.SaveChanges();
        }
        public void RemoveDataAlbum()
        {
            foreach (var e in ds.Albums)
            {
                ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
            }

            ds.SaveChanges();
        }

        public bool LoadDataTrack()
        {
            var user = HttpContext.Current.User.Identity.Name;

            if (ds.Tracks.Count() > 0)
                return false;

            var masterOfPuppets = ds.Albums.SingleOrDefault(m => m.Name == "Master of Puppets");
            ds.Tracks.Add(new Track()
            {
                Albums = new List<Album> { masterOfPuppets },
                Clerk = user,
                Composers = "James Hetfield, Cliff Burton, Lars Ulrich, Kirk Hammett",
                Name = "Master of Puppets (Remastered)",
                Genre = "Thrash Metal"
            });

            ds.Tracks.Add(new Track()
            {
                Albums = new List<Album> { masterOfPuppets },
                Clerk = user,
                Composers = "James Hetfield, Cliff Burton, Lars Ulrich",
                Name = "Battery",
                Genre = "Thrash Metal"
            });

            ds.Tracks.Add(new Track()
            {
                Albums = new List<Album> { masterOfPuppets },
                Clerk = user,
                Composers = "James Hetfield, Cliff Burton, Lars Ulrich",
                Name = "Orion",
                Genre = "Thrash Metal"
            });

            ds.Tracks.Add(new Track()
            {
                Albums = new List<Album> { masterOfPuppets },
                Clerk = user,
                Composers = "James Hetfield, Cliff Burton, Lars Ulrich, Kirk Hammett",
                Name = "Disposable Heroes",
                Genre = "Thrash Metal"
            });

            ds.Tracks.Add(new Track()
            {
                Albums = new List<Album> { masterOfPuppets },
                Clerk = user,
                Composers = "James Hetfield, Lars Ulrich",
                Name = "Leper Messiah",
                Genre = "Thrash Metal"
            });

            var rideTheLightning = ds.Albums.SingleOrDefault(r => r.Name == "Ride The Lightning");
            ds.Tracks.Add(new Track()
            {
                Albums = new List<Album> { rideTheLightning },
                Clerk = user,
                Composers = "James Hetfield, Cliff Burton, Lars Ulrich, Kirk Hammett",
                Name = "Fade to Black",
                Genre = "Thrash Metal"
            });

            ds.Tracks.Add(new Track()
            {
                Albums = new List<Album> { rideTheLightning },
                Clerk = user,
                Composers = "James Hetfield, Cliff Burton, Lars Ulrich",
                Name = "For Whom The Bell Tolls",
                Genre = "Thrash Metal"
            });

            ds.Tracks.Add(new Track()
            {
                Albums = new List<Album> { rideTheLightning },
                Clerk = user,
                Composers = "James Hetfield, Cliff Burton, Lars Ulrich, Kirk Hammett",
                Name = "Creeping Death",
                Genre = "Thrash Metal"
            });

            ds.Tracks.Add(new Track()
            {
                Albums = new List<Album> { rideTheLightning },
                Clerk = user,
                Composers = "James Hetfield, Cliff Burton, Lars Ulrich, Jason Newstead",
                Name = "Ride The Lightning (Remastered)",
                Genre = "Thrash Metal"
            });

            ds.Tracks.Add(new Track()
            {
                Albums = new List<Album> { rideTheLightning },
                Clerk = user,
                Composers = "James Hetfield, Cliff Burton, Lars Ulrich",
                Name = "Fight Fire With Fire",
                Genre = "Thrash Metal"
            });

            ds.SaveChanges();

            return true;
        }

        public bool LoadDataAlbum()
        {
            var user = HttpContext.Current.User.Identity.Name;

            if (ds.Albums.Count() > 0)
                return false;

            var metallica = ds.Artists.SingleOrDefault(m => m.Name == "Metallica");
            ds.Albums.Add(new Album()
            {
                Artists = new List<Artist> { metallica },
                Coordinator = user,
                Name = "Master of Puppets",
                Genre = "Thrash Metal",
                ReleaseDate = new DateTime(1986, 3, 3),
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/b/b2/Metallica_-_Master_of_Puppets_cover.jpg"
            });

            ds.Albums.Add(new Album()
            {
                Artists = new List<Artist> { metallica },
                Coordinator = user,
                Name = "Ride The Lightning",
                Genre = "Thrash Metal",
                ReleaseDate = new DateTime(1984, 7, 27),
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/f/f4/Ridetl.png"
            });

            ds.SaveChanges();

            return true;
        }

        public bool LoadDataGenre()
        {
            if (ds.Genres.Count() > 0)
                return false;

            ds.Genres.Add(new Genre()
            {
                Name = "Progressive Rock"
            });

            ds.Genres.Add(new Genre()
            {
                Name = "Alternative Rock"
            });

            ds.Genres.Add(new Genre()
            {
                Name = "Symphonic Metal"
            });

            ds.Genres.Add(new Genre()
            {
                Name = "Thrash Metal"
            });

            ds.Genres.Add(new Genre()
            {
                Name = "Electronic"
            });

            ds.Genres.Add(new Genre()
            {
                Name = "Pop"
            });

            ds.Genres.Add(new Genre()
            {
                Name = "Classic"
            });

            ds.Genres.Add(new Genre()
            {
                Name = "Punk Rock"
            });

            ds.Genres.Add(new Genre()
            {
                Name = "Country"
            });

            ds.Genres.Add(new Genre()
            {
                Name = "Jazz"
            });

            ds.SaveChanges();

            return true;
        }

        public bool LoadDataArtist()
        {
            var user = HttpContext.Current.User.Identity.Name;

            if (ds.Artists.Count() > 0)
            {
                return false;
            }

            ds.Artists.Add(new Artist()
            {
                BirthOrStartDate = new DateTime(1981, 10, 28),
                Executive = user,
                Name = "Metallica",
                Genre = "Thrash Metal",
                UrlArtist = "https://i.scdn.co/image/ab6761610000e5eb8101d13bdd630b0889acd2fd"
            });

            ds.Artists.Add(new Artist()
            {
                BirthName = "George Roger Waters",
                BirthOrStartDate = new DateTime(1943, 9, 6),
                Executive = user,
                Name = "Roger Waters",
                Genre = "Progressive Rock",
                UrlArtist = "https://guitar.com/wp-content/uploads/2022/10/roger-waters-2018-getty@2000x1500.jpg"
            });

            ds.Artists.Add(new Artist()
            {
                BirthOrStartDate = new DateTime(2002, 5, 17),
                Executive = user,
                Name = "Epica",
                Genre = "Symphonic Metal",
                UrlArtist = "http://www.epica.nl/1020_epi_omega_meta-2.jpg"
            });

            ds.SaveChanges();

            return true;
        }

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Role claims ***
            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim()
                {
                    Name = "Executive"
                });

                ds.RoleClaims.Add(new RoleClaim()
                {
                    Name = "Coordinator"
                });

                ds.RoleClaims.Add(new RoleClaim()
                {
                    Name = "Clerk"
                });

                ds.RoleClaims.Add(new RoleClaim()
                {
                    Name = "Staff"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                RemoveDataAlbum();
                RemoveDataArtist();
                RemoveDataGenre();
                RemoveDataTrack();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    #endregion

    #region RequestUser Class

    // This "RequestUser" class includes many convenient members that make it
    // easier work with the authenticated user and render user account info.
    // Study the properties and methods, and think about how you could use this class.

    // How to use...
    // In the Manager class, declare a new property named User:
    //    public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value:
    //    User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }

        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }

        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }

        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

    #endregion

}