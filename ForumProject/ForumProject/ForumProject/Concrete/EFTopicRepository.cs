using ForumProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ForumProject.Entities;
using ForumProject.Models;
using Microsoft.AspNet.Identity;
using System.Linq.Expressions;



namespace ForumProject.Concrete
{
   
    public class EFTopicRepository : ITopicRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();



        public EFTopicRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public EFTopicRepository()
        {

        }

        public void Add_To_Topics_And_User(Topic topic, string UserId)
        {
            this.Add(topic);

            ApplicationUser user = context.Users.Find(UserId);
            user.Topics.Add(topic);
            context.SaveChanges();


        }


        public IEnumerable<Topic> Topics
        {
            get { return context.Topics; }
        }

        public void Add(Topic entity)
        {
            context.Topics.Add(entity);
            context.SaveChanges();
        }

        public void Remove(Topic entity)
        {
            context.Topics.Remove(entity);
        }

        public Topic Get(int id)
        {

            Topic topic;

            topic = context.Topics.Find(id);

            if(topic==null)
            {
                topic = new Topic();
                topic.Comment = new List<Comment>();
                topic.TopicName = "Error";
                return topic;
            }

            return topic;
        }

        public IEnumerable<Topic> GetAll()
        {
            return context.Topics;
        }

        public IEnumerable<Topic> Find(Expression<Func<Topic, bool>> predicate)
        {
            return context.Topics.Where(predicate);
        }

        public void Add_Comment(Comment comment, string UserId)
        {



            Topic topic = context.Topics.Find(comment.TopicID);
            topic.Comment.Add(comment);

            ApplicationUser user = context.Users.Find(UserId);
            user.Comments.Add(comment);

            context.SaveChanges();


        }
    }










    public class EFMainCategoryByCitiesRepository : IMainCategoryByCitiesRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IEnumerable<MainCategoryByCities> MainCategoryByCities { get { return context.MainCategoryByCities; } }

        public EFMainCategoryByCitiesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public EFMainCategoryByCitiesRepository()
        {

        }

        public void Add(MainCategoryByCities entity)
        {
            context.MainCategoryByCities.Add(entity);
            context.SaveChanges();
        }

        public void Remove(MainCategoryByCities entity)
        {
            context.MainCategoryByCities.Remove(entity);
        }

        public MainCategoryByCities Get(int id)
        {

            MainCategoryByCities result;

            result = context.MainCategoryByCities.Find(id);

            if (result == null)
            {
                result = new Entities.MainCategoryByCities();
                result.IntermediateCategory = new List<IntermediateCategory>();

            }

            return result;
        }

        public IEnumerable<MainCategoryByCities> GetAll()
        {
            return context.MainCategoryByCities;
        }

        public IEnumerable<MainCategoryByCities> Find(Expression<Func<MainCategoryByCities, bool>> predicate)
        {
            return context.MainCategoryByCities.Where(predicate);
        }
    }












    public class EFIntermediateCategoryRepository : IIntermediateCategoryRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public EFIntermediateCategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public EFIntermediateCategoryRepository()
        {

        }


        public IEnumerable<IntermediateCategory> IntermediateCategories
        {
            get { return context.IntermediateCategories; }
        }



        public void Add(IntermediateCategory entity)
        {
            context.IntermediateCategories.Add(entity);
            context.SaveChanges();
        }

        public void Remove(IntermediateCategory entity)
        {
            context.IntermediateCategories.Remove(entity);
        }

        public IntermediateCategory Get(int id)
        {
            IntermediateCategory category;
            category=context.IntermediateCategories.Find(id);

            if (category == null)
            {
                category= new IntermediateCategory();
                category.NameOfMainCategory = "Error";
                category.Topic = new List<Topic>();
                return category;
            }
            else
            {
                return category;
            }



        }

        public IEnumerable<IntermediateCategory> GetAll()
        {
            return context.IntermediateCategories;
        }

        public IEnumerable<IntermediateCategory> Find(Expression<Func<IntermediateCategory, bool>> predicate)
        {
            return context.IntermediateCategories.Where(predicate);
        }
    }

    


}