using ForumProject.Entities;
using ForumProject.Models;
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

            IList<IntermediateCategory> defaultIntermediateCategories = new List<IntermediateCategory>();
            defaultIntermediateCategories.Add(new IntermediateCategory {IntermediateCategoryId=1,NameOfMainCategory="Zdrowie" });
            defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 2, NameOfMainCategory = "Kuchnia" });
            defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 3, NameOfMainCategory = "Sport " });
            defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 4, NameOfMainCategory = "Kino " });
            defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 5, NameOfMainCategory = "Motoryzacja " });
            defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 6, NameOfMainCategory = "Polityka " });
            defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 7, NameOfMainCategory = "Rodzina " });
            defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 8, NameOfMainCategory = "Dom i Ogród " });
            defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 9, NameOfMainCategory = "Praca " });
            defaultIntermediateCategories.Add(new IntermediateCategory { IntermediateCategoryId = 10, NameOfMainCategory = "Towarzyskie " });


            //context.IntermediateCategories.AddRange(defaultIntermediateCategories);

            IList<MainCategoryByCities> defaultCities = new List<MainCategoryByCities>();
            defaultCities.Add(new MainCategoryByCities {CityName="Bydgoszcz",MainCategoryByCitiesId=1,IntermediateCategory=defaultIntermediateCategories });
            defaultCities.Add(new MainCategoryByCities { CityName = "Chełmno", MainCategoryByCitiesId = 1, IntermediateCategory = defaultIntermediateCategories });
            defaultCities.Add(new MainCategoryByCities { CityName = "Grudziądz", MainCategoryByCitiesId = 2, IntermediateCategory = defaultIntermediateCategories });
            defaultCities.Add(new MainCategoryByCities { CityName = "Świecie", MainCategoryByCitiesId = 3, IntermediateCategory = defaultIntermediateCategories });
            defaultCities.Add(new MainCategoryByCities { CityName = "Toruń", MainCategoryByCitiesId = 4, IntermediateCategory = defaultIntermediateCategories });
            defaultCities.Add(new MainCategoryByCities { CityName = "Włocławek", MainCategoryByCitiesId = 5, IntermediateCategory = defaultIntermediateCategories });
            defaultCities.Add(new MainCategoryByCities { CityName = "Inowrocław", MainCategoryByCitiesId = 6, IntermediateCategory = defaultIntermediateCategories });
            defaultCities.Add(new MainCategoryByCities { CityName = "Osielsko", MainCategoryByCitiesId = 7, IntermediateCategory = defaultIntermediateCategories });
            defaultCities.Add(new MainCategoryByCities { CityName = "Brodnica", MainCategoryByCitiesId = 8, IntermediateCategory = defaultIntermediateCategories });
            defaultCities.Add(new MainCategoryByCities { CityName = "Warlubie", MainCategoryByCitiesId = 9, IntermediateCategory = defaultIntermediateCategories });

            context.MainCategoryByCities.AddRange(defaultCities);

            context.SaveChanges();

            base.Seed(context);


        }


    }
}