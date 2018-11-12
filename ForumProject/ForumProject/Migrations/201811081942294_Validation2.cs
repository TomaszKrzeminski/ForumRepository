namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validation2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IntermediateCategories", "NameOfMainCategory", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IntermediateCategories", "NameOfMainCategory", c => c.String());
        }
    }
}
