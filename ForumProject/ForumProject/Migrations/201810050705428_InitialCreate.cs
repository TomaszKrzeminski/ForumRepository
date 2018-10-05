namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        CommentContent = c.String(),
                        CommentTime = c.DateTime(nullable: false),
                        TopicID = c.Int(nullable: false),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .ForeignKey("dbo.Topics", t => t.TopicID, cascadeDelete: true)
                .Index(t => t.TopicID)
                .Index(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        TopicName = c.Int(nullable: false),
                        TopicTime = c.DateTime(nullable: false),
                        IntermediateCategoryId = c.Int(nullable: false),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TopicId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .ForeignKey("dbo.IntermediateCategories", t => t.IntermediateCategoryId, cascadeDelete: true)
                .Index(t => t.IntermediateCategoryId)
                .Index(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.IntermediateCategories",
                c => new
                    {
                        IntermediateCategoryId = c.Int(nullable: false, identity: true),
                        NameOfMainCategory = c.String(),
                    })
                .PrimaryKey(t => t.IntermediateCategoryId);
            
            CreateTable(
                "dbo.MainCategoryByCities",
                c => new
                    {
                        MainCategoryByCitiesId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.MainCategoryByCitiesId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.MainCategoryByCitiesIntermediateCategories",
                c => new
                    {
                        MainCategoryByCities_MainCategoryByCitiesId = c.Int(nullable: false),
                        IntermediateCategory_IntermediateCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MainCategoryByCities_MainCategoryByCitiesId, t.IntermediateCategory_IntermediateCategoryId })
                .ForeignKey("dbo.MainCategoryByCities", t => t.MainCategoryByCities_MainCategoryByCitiesId, cascadeDelete: true)
                .ForeignKey("dbo.IntermediateCategories", t => t.IntermediateCategory_IntermediateCategoryId, cascadeDelete: true)
                .Index(t => t.MainCategoryByCities_MainCategoryByCitiesId)
                .Index(t => t.IntermediateCategory_IntermediateCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Topics", "IntermediateCategoryId", "dbo.IntermediateCategories");
            DropForeignKey("dbo.MainCategoryByCitiesIntermediateCategories", "IntermediateCategory_IntermediateCategoryId", "dbo.IntermediateCategories");
            DropForeignKey("dbo.MainCategoryByCitiesIntermediateCategories", "MainCategoryByCities_MainCategoryByCitiesId", "dbo.MainCategoryByCities");
            DropForeignKey("dbo.Comments", "TopicID", "dbo.Topics");
            DropForeignKey("dbo.Topics", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.MainCategoryByCitiesIntermediateCategories", new[] { "IntermediateCategory_IntermediateCategoryId" });
            DropIndex("dbo.MainCategoryByCitiesIntermediateCategories", new[] { "MainCategoryByCities_MainCategoryByCitiesId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Topics", new[] { "ApplicationUserID" });
            DropIndex("dbo.Topics", new[] { "IntermediateCategoryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "ApplicationUserID" });
            DropIndex("dbo.Comments", new[] { "TopicID" });
            DropTable("dbo.MainCategoryByCitiesIntermediateCategories");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MainCategoryByCities");
            DropTable("dbo.IntermediateCategories");
            DropTable("dbo.Topics");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
        }
    }
}
