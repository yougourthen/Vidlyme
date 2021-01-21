namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9aea7ee8-7cd5-4a9c-a727-3fcf2c0dbdc9', N'guest@vidly.com', 0, N'AN0CWBlqNXI8LcmCBMykqYPHdQPXA+5ni2O4zqunVXhMhkIYnzNKk5k9s8ssnNK9Qw==', N'3fed016b-1699-4433-8b76-0adb66e19c4a', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'fa1c96b2-b9e3-4b78-a404-0235dbdad4b0', N'admin@vidly.com', 0, N'ANQnsbLUjim/F8/K4N7CMnVFsBSS37c1iFNE2yYbPBXeNReMzSw0VeeJka3SNN4v2A==', N'88f022a3-90f6-4412-83c3-0386c5f0c668', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'83047524-3c4d-4bae-988f-b256ea9f2a05', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fa1c96b2-b9e3-4b78-a404-0235dbdad4b0', N'83047524-3c4d-4bae-988f-b256ea9f2a05')

");
        }
        
        public override void Down()
        {
        }
    }
}
