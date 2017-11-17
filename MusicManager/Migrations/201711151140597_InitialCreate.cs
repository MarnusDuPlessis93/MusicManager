namespace MusicManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MusicLibraries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SongName = c.String(),
                        Album = c.String(),
                        Artist = c.String(),
                        DateOfAlbum = c.DateTime(),
                        GenreId = c.Int(),
                        SongPath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .Index(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MusicLibraries", "GenreId", "dbo.Genres");
            DropIndex("dbo.MusicLibraries", new[] { "GenreId" });
            DropTable("dbo.MusicLibraries");
            DropTable("dbo.Genres");
        }
    }
}
