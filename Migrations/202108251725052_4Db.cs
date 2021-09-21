namespace BookReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4Db : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "AboutOfBook", c => c.String(unicode: false, storeType: "text"));
            AddColumn("dbo.Books", "CountOfPage", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "IsPositive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Authors", "AboutOfAuthor", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("dbo.Reviews", "TextOfReview", c => c.String(nullable: false, unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "TextOfReview", c => c.String(nullable: false));
            AlterColumn("dbo.Authors", "AboutOfAuthor", c => c.String());
            DropColumn("dbo.Reviews", "IsPositive");
            DropColumn("dbo.Books", "Year");
            DropColumn("dbo.Books", "CountOfPage");
            DropColumn("dbo.Books", "AboutOfBook");
        }
    }
}
