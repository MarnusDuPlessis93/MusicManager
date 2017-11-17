namespace MusicManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thh1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MusicLibraryId = c.Int(nullable: false),
                        SongByte = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MusicLibraries", t => t.MusicLibraryId, cascadeDelete: true)
                .Index(t => t.MusicLibraryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "MusicLibraryId", "dbo.MusicLibraries");
            DropIndex("dbo.Songs", new[] { "MusicLibraryId" });
            DropTable("dbo.Songs");
        }
    }
}
