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
    }








}