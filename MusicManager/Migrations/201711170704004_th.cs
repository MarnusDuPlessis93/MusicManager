namespace MusicManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class th : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicLibraries", "Song", c => c.Binary(storeType: "image"));
            DropColumn("dbo.MusicLibraries", "SongPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MusicLibraries", "SongPath", c => c.String());
            DropColumn("dbo.MusicLibraries", "Song");
        }
    }
}
