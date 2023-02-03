namespace F2022A5AVA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedEntityList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Coordinator = c.String(nullable: false, maxLength: 120),
                        Name = c.String(nullable: false, maxLength: 120),
                        Genre = c.String(nullable: false, maxLength: 120),
                        ReleaseDate = c.DateTime(nullable: false),
                        UrlAlbum = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BirthName = c.String(maxLength: 120),
                        BirthOrStartDate = c.DateTime(nullable: false),
                        Executive = c.String(nullable: false, maxLength: 120),
                        Name = c.String(nullable: false, maxLength: 120),
                        Genre = c.String(nullable: false, maxLength: 120),
                        UrlArtist = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Clerk = c.String(nullable: false, maxLength: 120),
                        Composers = c.String(maxLength: 120),
                        Name = c.String(nullable: false, maxLength: 120),
                        Genre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 120),
                        Album_Id = c.Int(),
                        Artist_Id = c.Int(),
                        Track_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id)
                .ForeignKey("dbo.Tracks", t => t.Track_Id)
                .Index(t => t.Album_Id)
                .Index(t => t.Artist_Id)
                .Index(t => t.Track_Id);
            
            CreateTable(
                "dbo.ArtistAlbums",
                c => new
                    {
                        Artist_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artist_Id, t.Album_Id })
                .ForeignKey("dbo.Artists", t => t.Artist_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Artist_Id)
                .Index(t => t.Album_Id);
            
            CreateTable(
                "dbo.TrackAlbums",
                c => new
                    {
                        Track_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Track_Id, t.Album_Id })
                .ForeignKey("dbo.Tracks", t => t.Track_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Track_Id)
                .Index(t => t.Album_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Genres", "Track_Id", "dbo.Tracks");
            DropForeignKey("dbo.Genres", "Artist_Id", "dbo.Artists");
            DropForeignKey("dbo.Genres", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.TrackAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.TrackAlbums", "Track_Id", "dbo.Tracks");
            DropForeignKey("dbo.ArtistAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.ArtistAlbums", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.TrackAlbums", new[] { "Album_Id" });
            DropIndex("dbo.TrackAlbums", new[] { "Track_Id" });
            DropIndex("dbo.ArtistAlbums", new[] { "Album_Id" });
            DropIndex("dbo.ArtistAlbums", new[] { "Artist_Id" });
            DropIndex("dbo.Genres", new[] { "Track_Id" });
            DropIndex("dbo.Genres", new[] { "Artist_Id" });
            DropIndex("dbo.Genres", new[] { "Album_Id" });
            DropTable("dbo.TrackAlbums");
            DropTable("dbo.ArtistAlbums");
            DropTable("dbo.Genres");
            DropTable("dbo.Tracks");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}
