namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "TopicData", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "TopicData");
        }
    }
}
