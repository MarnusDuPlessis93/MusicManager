namespace MusicManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thh3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicLibraries", "SongId", c => c.Int());
            CreateIndex("dbo.MusicLibraries", "SongId");
            AddForeignKey("dbo.MusicLibraries", "SongId", "dbo.Songs", "Id");
            DropColumn("dbo.Songs", "MusicLibraryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "MusicLibraryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.MusicLibraries", "SongId", "dbo.Songs");
            DropIndex("dbo.MusicLibraries", new[] { "SongId" });
            DropColumn("dbo.MusicLibraries", "SongId");
        }
    }
}
