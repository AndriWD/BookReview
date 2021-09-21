namespace BookReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewM : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Reviews", "BookId", "dbo.Books");
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropIndex("dbo.Reviews", new[] { "BookId" });
            DropTable("dbo.Authors");
            DropTable("dbo.Books");
            DropTable("dbo.Reviews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        TextOfReview = c.String(nullable: false),
                        BookId = c.Int(nullable: false),
                        Vote = c.Int(nullable: false),
                        VoteCounter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        NameOfBook = c.String(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Vote = c.Int(nullable: false),
                        VoteCounter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SurName = c.String(nullable: false),
                        Vote = c.Int(nullable: false),
                        VoteCounter = c.Int(nullable: false),
                        AboutOfAuthor = c.String(),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateIndex("dbo.Reviews", "BookId");
            CreateIndex("dbo.Books", "AuthorId");
            AddForeignKey("dbo.Reviews", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
            AddForeignKey("dbo.Books", "AuthorId", "dbo.Authors", "AuthorId", cascadeDelete: true);
        }
    }
}
