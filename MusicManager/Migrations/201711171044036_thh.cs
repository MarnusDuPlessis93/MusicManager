namespace MusicManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thh : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MusicLibraries", "Song");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MusicLibraries", "Song", c => c.Binary(storeType: "image"));
        }
    }
}
