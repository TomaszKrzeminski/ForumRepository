using ForumProject.Entities;
using ForumProject.Models;
using ForumProject.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumProject.Controllers
{
    public class TopicController : Controller
    {
        ITopicRepository repository;
        IIntermediateCategoryRepository repositoryInter;
        private Func<string> GetUserId;
        public TopicController(ITopicRepository repository,IIntermediateCategoryRepository repositoryInter)
        {
            this.repository = repository;
            this.repositoryInter = repositoryInter;
            GetUserId = () => User.Identity.GetUserId();
        }


        public TopicController(ITopicRepository repository, IIntermediateCategoryRepository repositoryInter, Func<string> GetUserId)
        {
            this.repository = repository;
            this.repositoryInter = repositoryInter;
            this.GetUserId = GetUserId;

        }


        public ViewResult Show_Topics(int id)
        {

            ViewBag.IntermediateCategory_Id = id;


            //List<Topic> TopicList = repositoryInter.Get(id).Topic.ToList();


            IntermediateCategory category = repositoryInter.Get(id);

            if(category.NameOfMainCategory=="Error")
            {
                return View("Error");
            }
            else
            {
                List<Topic> TopicList = category.Topic.ToList();
                return View("Show_Topics",TopicList);
            }


           
        }

      [Authorize]
        public ViewResult Add_New_Topic(int id)
        {
            //MainCategoryByCities maincategory = repository.Get_MainCategoryByCities_To_Add(id);
            //ViewBag.InterId = maincategory.MainCategoryByCitiesId;

            ViewBag.InterId = id;

            return View(new Topic());
        }


        [HttpPost]
        public RedirectToRouteResult Add_New_Topic(Topic topic)
        {

            string UserId = User.Identity.GetUserId();



            repository.Add_To_Topics_And_User(topic, UserId);

            return RedirectToAction("Show_Topics",new {controller="Topic",action="Show_Topics",id=topic.IntermediateCategoryId });
        }

        public ViewResult Go_To_Topic(int id)
        {


            Topic topic = repository.Get(id);

            if(topic.TopicName=="Error")
            {
                return View("Error");
            }

            List<Comment> commentList = new List<Comment>();

            if(topic.Comment.ToList().Count>0)
            {
                commentList = topic.Comment.ToList();
            }

            TopicViewModel viewModel = new TopicViewModel();
            viewModel.topic = topic;

            if (commentList != null)
            {
                viewModel.comment_List = commentList;
            }

            string UserName = topic.ApplicationUser.UserName;

            viewModel.userName = UserName;


            return View(viewModel);

            

        }


        [Authorize]
        public ActionResult Add_New_Comment(int id)
        {

            //Topic topic = repository.Get_Topic_By_Id(id);
            Topic topic = repository.Get(id);

            AddingCommentViewModel adding = new AddingCommentViewModel();

            adding.topic = topic;



            return View(adding);
            
        }

        [HttpPost]
        [Authorize]
        public RedirectToRouteResult Add_New_Comment(Comment comment)
        {
            //string UserId = User.Identity.GetUserId();
            string UserId = GetUserId();
            repository.Add_Comment(comment, UserId);

            return RedirectToAction("Go_To_Topic", new { controller = "Topic", action = "Go_To_Topic", id = comment.TopicID });

        }



    }
}