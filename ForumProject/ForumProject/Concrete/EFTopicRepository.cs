using ForumProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumProject.Entities;
using ForumProject.Models;

namespace ForumProject.Concrete
{
    public class EFTopicRepository : ITopicRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public IEnumerable<Topic> Topics
        {
            get { return context.Topics; }
        }

        public bool Add_New_Topic_To_Database(Topic topic)
        {

            //int MainCategoryId = context.IntermediateCategories.Include("MainCategoryByCities").Where(x => x.IntermediateCategoryId == topic.IntermediateCategoryId).Select(y => y.MainCategoryByCitiesId).First();
            //MainCategoryByCities main = context.MainCategoryByCities.Find(MainCategoryId);
            //main.

            IntermediateCategory inter = context.IntermediateCategories.Find(topic.IntermediateCategoryId);
            inter.Topic.Add(topic);
            context.SaveChanges();
            return true;


        }

        public MainCategoryByCities Get_MainCategoryByCities_To_Add(int id)
        {
            IntermediateCategory inter = context.IntermediateCategories.Include("MainCategoryByCities").Where(x => x.IntermediateCategoryId == id).First();

            MainCategoryByCities maincategory = inter.MainCategoryByCities;

            return maincategory;

        }

        public IEnumerable<Topic> Get_Topics_ByIntermediateCategory(int id)
        {
            IntermediateCategory category = context.IntermediateCategories.Include("Topic").Where(x => x.IntermediateCategoryId == id).First();
            IEnumerable<Topic> topics = category.Topic;

            return topics;
        }





    }

    public class EFMainCategoryByCitiesRepository : IMainCategoryByCitiesRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();



        public IEnumerable<MainCategoryByCities> MainCategoryByCities
        {
            get { return context.MainCategoryByCities; }
        }
    }


    public class EFIntermediateCategoryRepository : IIntermediateCategoryRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IEnumerable<IntermediateCategory> IntermediateCategory
        {
            get { return context.IntermediateCategories; }
        }

        public IEnumerable<IntermediateCategory> GetIntermediateCategory_ById(int id)
        {

             MainCategoryByCities city=context.MainCategoryByCities.Include("IntermediateCategory").Where(x => x.MainCategoryByCitiesId == id).First();
             IEnumerable<IntermediateCategory> Categories = city.IntermediateCategory.ToList();

            if(Categories!=null)
            {
                return Categories;
            }
            else
            {
                return Categories = new List<IntermediateCategory>();
            }


        }
    }








}