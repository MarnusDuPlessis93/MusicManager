namespace MusicManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thiss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicLibraries", "AlbumArt", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MusicLibraries", "AlbumArt");
        }
    }
}
