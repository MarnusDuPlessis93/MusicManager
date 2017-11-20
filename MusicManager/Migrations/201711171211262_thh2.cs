namespace MusicManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thh2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Songs", "MusicLibraryId", "dbo.MusicLibraries");
            DropIndex("dbo.Songs", new[] { "MusicLibraryId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Songs", "MusicLibraryId");
            AddForeignKey("dbo.Songs", "MusicLibraryId", "dbo.MusicLibraries", "Id", cascadeDelete: true);
        }
    }
}
