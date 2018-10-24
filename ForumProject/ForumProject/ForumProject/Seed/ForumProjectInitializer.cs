using ForumProject.Entities;
using ForumProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ForumProject.Seed
{
   
    public class ForumProjectInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {


            //IList<Category> defaultCategory = new List<Category>();
            //defaultCategory.Add(new Category { CategoryID = 8, categoryname = CategoryName.Motoryzacja, Description = "Motoryzacja" });
            //defaultCategory.Add(new Category { CategoryID = 7, categoryname = CategoryName.Elektronika, Description = "Elektronika" });


            //context.Categories.AddRange(defaultCategory);

            //IList<SubCategory> defaultSubCategory = new List<SubCategory>();
            //defaultSubCategory.Add(new SubCategory { SubCategoryID = 0, CategoryID = 8, subcategoryname = SubCategoryName.Czesci, Description = "Części" });
            //defaultSubCategory.Add(new SubCategory { SubCategoryID = 1, CategoryID = 7, subcategoryname = SubCategoryName.Komputery, Description = "Komputery" });
            //defaultSubCategory.Add(new SubCategory { SubCategoryID = 2, CategoryID = 8, subcategoryname = SubCategoryName.MotocykleISkutery, Description = "MotocykleiSkutery" });
            //defaultSubCategory.Add(new SubCategory { SubCategoryID = 3, CategoryID = 8, subcategoryname = SubCategoryName.Samochod, Description = "Samochody" });
            //defaultSubCategory.Add(new SubCategory { SubCategoryID = 4, CategoryID = 8, subcategoryname = SubCategoryName.SamochodyCiezarowe, Description = "Cieżarówki" });
            //defaultSubCategory.Add(new SubCategory { SubCategoryID = 5, CategoryID = 7, subcategoryname = SubCategoryName.TelefonyKomórkowe, Description = "TelefonyKomórkowe" });
            //defaultSubCategory.Add(new SubCategory { SubCategoryID = 6, CategoryID = 7, subcategoryname = SubCategoryName.Telewizory, Description = "Telewizory" });

            //context.SubCategories.AddRange(defaultSubCategory);

            //IList<Car> defaultCar = new List<Car>();

            //Car suzi = new Car { bodytype = BodyType.hatchback, Color = "Black", DateOfProduction = DateTime.Now, Make = "Suzuki" };
            //context.Advertisments.Add(suzi);
            //context.SaveChanges();

            //IList<IntermediateCategory> defaultIntermediateCategories = new List<IntermediateCategory>();
            //defaultIntermediateCategories.Add(new IntermediateCategory {IntermediateCategoryId=1,NameOfMainCategory="Zdrowie" });
            //defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 2, NameOfMainCategory = "Kuchnia" });
            //defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 3, NameOfMainCategory = "Sport " });
            //defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 4, NameOfMainCategory = "Kino " });
            //defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 5, NameOfMainCategory = "Motoryzacja " });
            //defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 6, NameOfMainCategory = "Polityka " });
            //defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 7, NameOfMainCategory = "Rodzina " });
            //defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 8, NameOfMainCategory = "Dom i Ogród " });
            //defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 9, NameOfMainCategory = "Praca " });
            //defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 10, NameOfMainCategory = "Towarzyskie " });


           





            List <MainCategoryByCities> defaultCities = new List<MainCategoryByCities>();
            defaultCities.Add(new MainCategoryByCities { CityName = "Bydgoszcz", IntermediateCategory = new List<IntermediateCategory>() });
            defaultCities.Add(new MainCategoryByCities { CityName = "Chełmno", IntermediateCategory = new List<IntermediateCategory>() });
            defaultCities.Add(new MainCategoryByCities { CityName = "Grudziądz", IntermediateCategory = new List<IntermediateCategory>() });
            defaultCities.Add(new MainCategoryByCities { CityName = "Świecie", IntermediateCategory = new List<IntermediateCategory>() });
            defaultCities.Add(new MainCategoryByCities { CityName = "Toruń", IntermediateCategory = new List<IntermediateCategory>() });
            defaultCities.Add(new MainCategoryByCities { CityName = "Włocławek", IntermediateCategory = new List<IntermediateCategory>() });
            defaultCities.Add(new MainCategoryByCities { CityName = "Osielsko", IntermediateCategory = new List<IntermediateCategory>() });
            defaultCities.Add(new MainCategoryByCities { CityName = "Brodnica", IntermediateCategory = new List<IntermediateCategory>() });
            //defaultCities.Add(new MainCategoryByCities { CityName = "Warlubie",  IntermediateCategory = new List<IntermediateCategory>() });

            defaultCities.ForEach(x => context.MainCategoryByCities.Add(x));
            context.SaveChanges();

            List<IntermediateCategory> defaultIntermediateCategories = new List<IntermediateCategory>();

            for (int i = 1; i < 9; i++)
            {


            defaultIntermediateCategories.Add(new IntermediateCategory {  NameOfMainCategory = "Zdrowie",MainCategoryByCitiesId=i });
            defaultIntermediateCategories.Add(new IntermediateCategory { NameOfMainCategory = "Kuchnia", MainCategoryByCitiesId = i });
            defaultIntermediateCategories.Add(new IntermediateCategory {  NameOfMainCategory = "Sport ", MainCategoryByCitiesId = i });
            defaultIntermediateCategories.Add(new IntermediateCategory {  NameOfMainCategory = "Kino ", MainCategoryByCitiesId = i });
            defaultIntermediateCategories.Add(new IntermediateCategory {  NameOfMainCategory = "Polityka ", MainCategoryByCitiesId = i });
            defaultIntermediateCategories.Add(new IntermediateCategory {  NameOfMainCategory = "Rodzina ", MainCategoryByCitiesId = i });
            defaultIntermediateCategories.Add(new IntermediateCategory {  NameOfMainCategory = "Dom i Ogród ", MainCategoryByCitiesId = i });
            defaultIntermediateCategories.Add(new IntermediateCategory {  NameOfMainCategory = "Praca ", MainCategoryByCitiesId = i });
            defaultIntermediateCategories.Add(new IntermediateCategory {  NameOfMainCategory = "Towarzyskie ", MainCategoryByCitiesId = i });

            }


            

            defaultIntermediateCategories.ForEach(x => context.IntermediateCategories.Add(x));
            context.SaveChanges();







            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string[] roleNames = new string[] { "Administrator", "User" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                if (!roleManager.RoleExists(roleName))
                {
                    roleResult = roleManager.Create(new IdentityRole(roleName));
                }
            }

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser user = new ApplicationUser();
           
            user.PasswordHash = "Password";
            user.UserName = "Administrator";
            user.Email = "koral2323@gmail.com";
            context.Users.Add(user);
            context.SaveChanges();
            UserManager.AddToRole(context.Users.FirstOrDefault().Id, "Administrator");

            context.SaveChanges();




            //int defaultCityCount = defaultCities.Count;
            //int defaultIntermediateCategoriesCount = defaultIntermediateCategories.Count;


            //for (int i = 0; i < defaultCityCount; i++)
            //{



            //    for (int j = 0; j < defaultIntermediateCategoriesCount; j++)
            //    {
            //        defaultCities[i].IntermediateCategory.Add(defaultIntermediateCategories[j]);
            //    }





            //}


            List<MainCategoryByCities> list = defaultCities;




           

           
           
            //    context.MainCategoryByCities.Add(defaultCities[0]);
            //context.MainCategoryByCities.Add(defaultCities[1]);
            //context.MainCategoryByCities.Add(defaultCities[2]);
            //context.SaveChanges();
           



           

            base.Seed(context);


        }


    }
}