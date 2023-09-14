namespace Biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NazwaMigracji : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Filmies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tytul = c.String(nullable: false),
                        DataObejrzenia = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seriales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tytul = c.String(nullable: false),
                        DataObejrzenia = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Seriales");
            DropTable("dbo.Filmies");
        }
    }
}
