namespace Biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ksiazkas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tytul = c.String(),
                        Autor = c.String(),
                        DataPrzeczytania = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ksiazkas");
        }
    }
}
