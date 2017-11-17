namespace MusicManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MusicLibraries", "AlbumArt", c => c.Binary(storeType: "image"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MusicLibraries", "AlbumArt", c => c.Byte(nullable: false));
        }
    }
}
