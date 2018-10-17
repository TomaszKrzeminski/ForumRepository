using ForumProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumProject.Entities;
using ForumProject.Models;
using Microsoft.AspNet.Identity;
using System.Linq.Expressions;

namespace ForumProject.Concrete
{
    //public class EFTopicRepository : ITopicRepository
    //{
    //    private ApplicationDbContext context = new ApplicationDbContext();



    //    public EFTopicRepository(ApplicationDbContext context)
    //    {
    //        this.context = context;
    //    }


    //    public EFTopicRepository()
    //    {

    //    }








    //    public IEnumerable<Topic> Topics
    //    {
    //        get { return context.Topics; }
    //    }

    //    public bool Add_New_Comment_To_Topic(Comment comment,string UserId)
    //    {

    //        Topic topic = context.Topics.Find(comment.TopicID);
    //        topic.Comment.Add(comment);

    //        ApplicationUser user = context.Users.Find(UserId);
    //        user.Comments.Add(comment);

    //        context.SaveChanges();

    //        return true;






    //    }

    //    public bool Add_New_Topic_To_Database(Topic topic,string UserId)
    //    {

    //        //int MainCategoryId = context.IntermediateCategories.Include("MainCategoryByCities").Where(x => x.IntermediateCategoryId == topic.IntermediateCategoryId).Select(y => y.MainCategoryByCitiesId).First();
    //        //MainCategoryByCities main = context.MainCategoryByCities.Find(MainCategoryId);
    //        //main.

    //        ApplicationUser user = context.Users.Find(UserId);
    //        user.Topics.Add(topic);



    //        IntermediateCategory inter = context.IntermediateCategories.Find(topic.IntermediateCategoryId);
    //        inter.Topic.Add(topic);
    //        context.SaveChanges();
    //        return true;


    //    }

    //    public MainCategoryByCities Get_MainCategoryByCities_To_Add(int id)
    //    {
    //        IntermediateCategory inter = context.IntermediateCategories.Include("MainCategoryByCities").Where(x => x.IntermediateCategoryId == id).First();

    //        MainCategoryByCities maincategory = inter.MainCategoryByCities;

    //        return maincategory;

    //    }

    //    public IEnumerable<Topic> Get_Topics_ByIntermediateCategory(int id)
    //    {
    //        IntermediateCategory category = context.IntermediateCategories.Include("Topic").Where(x => x.IntermediateCategoryId == id).First();
    //        IEnumerable<Topic> topics = category.Topic;

    //        return topics;
    //    }

    //    public Topic Get_Topic_By_Id(int id)
    //    {
    //        Topic topic = context.Topics.Find(id);

    //        if(topic!=null)
    //        {
    //           return topic;
    //        }
    //        else
    //        {
    //            Topic t = null;
               
    //            return t;
    //        }
           
    //    }

    //    public TopicViewModel Get_Topic_ViewModel(int id)
    //    {
    //        Topic topic = context.Topics.Where(t => t.TopicId == id).First();
    //        List<Comment> commentList = new List<Comment>();

    //        commentList = topic.Comment.ToList();

    //        TopicViewModel viewModel = new TopicViewModel();
    //        viewModel.topic = topic;

    //        if (commentList != null)
    //        {
    //            viewModel.comment_List = commentList;
    //        }

    //        string UserName = topic.ApplicationUser.UserName;

    //        viewModel.userName = UserName;




    //        return viewModel;



    //    }
    //}

    //public class EFMainCategoryByCitiesRepository : IMainCategoryByCitiesRepository
    //{
    //    private ApplicationDbContext context = new ApplicationDbContext();


    //    public EFMainCategoryByCitiesRepository(ApplicationDbContext context)
    //    {
    //        this.context = context;
    //    }


    //    public EFMainCategoryByCitiesRepository()
    //    {

    //    }





    //    public IEnumerable<MainCategoryByCities> MainCategoryByCities
    //    {
    //        get { return context.MainCategoryByCities; }
    //    }
    //}


    //public class EFIntermediateCategoryRepository : IIntermediateCategoryRepository
    //{
    //    private ApplicationDbContext context = new ApplicationDbContext();

    //    public EFIntermediateCategoryRepository(ApplicationDbContext context)
    //    {
    //        this.context = context;
    //    }


    //    public EFIntermediateCategoryRepository()
    //    {
            
    //    }


    //    public IEnumerable<IntermediateCategory> IntermediateCategory
    //    {
    //        get { return context.IntermediateCategories; }
    //    }

    //    public IEnumerable<IntermediateCategory> GetIntermediateCategory_ById(int id)
    //    {

    //         MainCategoryByCities city=context.MainCategoryByCities.Include("IntermediateCategory").Where(x => x.MainCategoryByCitiesId == id).First();
    //         IEnumerable<IntermediateCategory> Categories = city.IntermediateCategory.ToList();

    //        if(Categories!=null)
    //        {
    //            return Categories;
    //        }
    //        else
    //        {
    //            return Categories = new List<IntermediateCategory>();
    //        }


    //    }
    //}









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

        public void Add_To_Topics_And_User(Topic topic,string UserId)
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
           return context.Topics.Find(id);
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

        public IEnumerable<MainCategoryByCities> MainCategoryByCities { get {return context.MainCategoryByCities; } }

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
          return  context.MainCategoryByCities.Find(id);
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
            return context.IntermediateCategories.Find(id);
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