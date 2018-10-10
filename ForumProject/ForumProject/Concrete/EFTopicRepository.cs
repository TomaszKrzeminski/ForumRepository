using ForumProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumProject.Entities;
using ForumProject.Models;
using Microsoft.AspNet.Identity;

namespace ForumProject.Concrete
{
    public class EFTopicRepository : ITopicRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public IEnumerable<Topic> Topics
        {
            get { return context.Topics; }
        }

        public bool Add_New_Comment_To_Topic(Comment comment,string UserId)
        {

            Topic topic = context.Topics.Find(comment.TopicID);
            topic.Comment.Add(comment);

            ApplicationUser user = context.Users.Find(UserId);
            user.Comments.Add(comment);

            context.SaveChanges();

            return true;






        }

        public bool Add_New_Topic_To_Database(Topic topic,string UserId)
        {

            //int MainCategoryId = context.IntermediateCategories.Include("MainCategoryByCities").Where(x => x.IntermediateCategoryId == topic.IntermediateCategoryId).Select(y => y.MainCategoryByCitiesId).First();
            //MainCategoryByCities main = context.MainCategoryByCities.Find(MainCategoryId);
            //main.

            ApplicationUser user = context.Users.Find(UserId);
            user.Topics.Add(topic);



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

        public Topic Get_Topic_By_Id(int id)
        {
            Topic topic = context.Topics.Find(id);

            if(topic!=null)
            {
               return topic;
            }
            else
            {
                Topic t = null;
               
                return t;
            }
           
        }

        public TopicViewModel Get_Topic_ViewModel(int id)
        {
            Topic topic = context.Topics.Where(t => t.TopicId == id).First();
            List<Comment> commentList = new List<Comment>();

            commentList = topic.Comment.ToList();

            TopicViewModel viewModel = new TopicViewModel();
            viewModel.topic = topic;

            if (commentList != null)
            {
                viewModel.comment_List = commentList;
            }

            string UserName = topic.ApplicationUser.UserName;

            viewModel.userName = UserName;




            return viewModel;



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