namespace Biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataBase1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ksiazkas", "DataPrzeczytania", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ksiazkas", "DataPrzeczytania", c => c.DateTime(nullable: false));
        }
    }
}
