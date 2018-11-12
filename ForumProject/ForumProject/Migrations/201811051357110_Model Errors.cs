namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelErrors : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Topics", "TopicName", c => c.String(maxLength: 5));
            AlterColumn("dbo.Topics", "TopicData", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Topics", "TopicData", c => c.String());
            AlterColumn("dbo.Topics", "TopicName", c => c.String());
        }
    }
}
