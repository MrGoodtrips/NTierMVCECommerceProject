using Bogus.DataSets;
using Project.COMMON.Tools;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.StrategyPattern
{
    public class MyInit :CreateDatabaseIfNotExists<MyContext>
    {
        // Seed metodu bir veritabanı oluşturulurken o ve oluşturulan model içerisine bilgi girilmesini sağlar...
        protected override void Seed(MyContext context)
        {
            #region Admin

            AppUser au = new AppUser();
            au.UserName = "admin";
            au.Password = DemonCrypt.Crypt("admin");
            au.Email = "serhatozturk.mr.goodtrips@gmail.com";
            au.Role = ENTITIES.Enums.UserRole.Admin;
            context.AppUsers.Add(au);
            context.SaveChanges();

            #endregion

            #region NormalUsers

            for (int i = 0; i < 10; i++)
            {
                AppUser app = new AppUser();
                app.UserName = new Internet("tr").UserName();
                app.Password = new Internet("tr").Password();
                app.Email = new Internet("tr").Email();
                context.AppUsers.Add(app);
            }
            context.SaveChanges();

            for (int i = 2; i < 12; i++)
            {
                AppUserProfile profile = new AppUserProfile();

                profile.ID = i;

                profile.FirstName = new Name("tr").FirstName();
                profile.LastName = new Name("tr").LastName();
                context.AppUserProfiles.Add(profile);
            }
            context.SaveChanges();

            #endregion

            #region Categories

            for (int i = 0; i < 10; i++)
            {
                Category c = new Category();
                c.CategoryName = new Commerce("tr").Categories(1)[0];
                c.Description = new Lorem("tr").Sentence(10);
                c.Products = new List<Product>();

                for (int j = 0; j < 30; j++)
                {
                    Product p = new Product();
                    p.ProductName = new Commerce("tr").ProductName();
                    p.UnitPrice = Convert.ToDecimal(new Commerce("tr").Price());
                    p.ImagePath = new Images().Nightlife();
                    c.Products.Add(p);
                }
                context.Categories.Add(c);
                context.SaveChanges();
            }

            #endregion

        }
    }
}
