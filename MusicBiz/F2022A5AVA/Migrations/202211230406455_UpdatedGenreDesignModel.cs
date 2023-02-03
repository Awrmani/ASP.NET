namespace F2022A5AVA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedGenreDesignModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Genres", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.Genres", "Artist_Id", "dbo.Artists");
            DropForeignKey("dbo.Genres", "Track_Id", "dbo.Tracks");
            DropIndex("dbo.Genres", new[] { "Album_Id" });
            DropIndex("dbo.Genres", new[] { "Artist_Id" });
            DropIndex("dbo.Genres", new[] { "Track_Id" });
            DropColumn("dbo.Genres", "Album_Id");
            DropColumn("dbo.Genres", "Artist_Id");
            DropColumn("dbo.Genres", "Track_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Genres", "Track_Id", c => c.Int());
            AddColumn("dbo.Genres", "Artist_Id", c => c.Int());
            AddColumn("dbo.Genres", "Album_Id", c => c.Int());
            CreateIndex("dbo.Genres", "Track_Id");
            CreateIndex("dbo.Genres", "Artist_Id");
            CreateIndex("dbo.Genres", "Album_Id");
            AddForeignKey("dbo.Genres", "Track_Id", "dbo.Tracks", "Id");
            AddForeignKey("dbo.Genres", "Artist_Id", "dbo.Artists", "Id");
            AddForeignKey("dbo.Genres", "Album_Id", "dbo.Albums", "Id");
        }
    }
}
