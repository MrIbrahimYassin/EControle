using EControle.Core.Data.Models;
using EControle.Core.Data.Types;

namespace EControle.Data.EF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EControle.Data.EF.Connection.DataCtx>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EControle.Data.EF.Connection.DataCtx db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
             db.Users.AddOrUpdate(new User
            {
                Id = 1,
                UserName = "Admin",
                Password = "",
                Phone = "0965438114",
                SetNewPassword = true,
                CreateDate = DateTime.Now,
                Gender = Gender.Male,
                IsActive = true,
                Name = "Super Admin",
                Role = Role.SuperAdmin, 
                Serial = Guid.NewGuid().ToString()
            });
            db.Users.AddOrUpdate(new User
            {
                Id = 2,
                UserName = "agent1",
                Password = "",
                Phone = "0964",
                SetNewPassword = true,
                CreateDate = DateTime.Now,
                Gender = Gender.Male,
                IsActive = true,
                Name = "Agent 1",
                Role = Role.Agent,
                Serial = Guid.NewGuid().ToString()

            }
            );
            //db.SaveChanges();
            //var user = db.Users.Find(1);
            //var user2 = db.Users.Find(2);
            //var flag = Guid.NewGuid();
            //db.InternalTransactions.AddOrUpdate(new InternalTransaction
            //{
            //    Id = 1,
            //    CreateDate = DateTime.Now,
            //    UserId = 1,
            //    Amount = 1000000,
            //    Direction = Direction.In,
            //    TransState = TransState.Completed,
            //    CreatedBy = user?.Serial,
            //    CompleteDate = DateTime.Now,
            //    CompletedBy = user?.Serial,
            //    Flag = flag,
            //    Comment = "Starting Balance",
            //    CreatorName = user?.Name
            //});
            //for (int i = 1; i < 100; i++)
            //{
            //    flag = Guid.NewGuid();
            //    //S
            //    db.InternalTransactions.AddOrUpdate(new InternalTransaction
            //    {

            //        CreateDate = DateTime.Now,
            //        UserId = 1,
            //        Amount = decimal.Parse((5.5 * i).ToString()),
            //        Direction = Direction.Out,
            //        TransState = TransState.Completed,
            //        CreatedBy = user.Serial,
            //        CompleteDate = DateTime.Now,
            //        CompletedBy = user.Serial,
            //        Flag = flag,
            //        Comment = $"Trans No: {i}",
            //        CreatorName = user.Name
            //    });
            //    //R
            //    db.InternalTransactions.AddOrUpdate(new InternalTransaction
            //    {
            //        CreateDate = DateTime.Now,
            //        UserId = 2,
            //        Amount = decimal.Parse((5.5 * i).ToString()),
            //        Direction = Direction.In,
            //        TransState = TransState.Completed,
            //        CreatedBy = user.Serial,
            //        CompleteDate = DateTime.Now,
            //        CompletedBy = user.Serial,
            //        Flag = flag,
            //        Comment = $"Trans No: {i}",
            //        CreatorName = user.Name
            //    });
            //}
            //db.SaveChanges();

        }
    }
}
