using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ForumProject.Repository;
using ForumProject.Entities;
using ForumProject.Controllers;
using System.Web.Mvc;
using System.Security.Principal;
using ForumProject.Models;
using System.Web;
using System.Security.Claims;

namespace ForumProject.Tests.Unit
{
    [TestFixture]
    class ForumTests
    {

        [Test]
        public void List_Test()
        {
            Mock<IMainCategoryByCitiesRepository> mock = new Mock<IMainCategoryByCitiesRepository>();
            mock.Setup(m => m.MainCategoryByCities).Returns(new MainCategoryByCities[]
            {
                   new MainCategoryByCities(){ MainCategoryByCitiesId=1,CityName="Aleksandrów"  },
                    new MainCategoryByCities(){ MainCategoryByCitiesId=2,CityName="Bydgoszcz"  },
                     new MainCategoryByCities(){ MainCategoryByCitiesId=3,CityName="Chełmno"  },
                      new MainCategoryByCities(){ MainCategoryByCitiesId=4,CityName="Grudziądz"  },
                       new MainCategoryByCities(){ MainCategoryByCitiesId=5,CityName="Świecie"  }


            });

            Mock<IIntermediateCategoryRepository> mock2 = new Mock<IIntermediateCategoryRepository>();




            HomeController controller = new HomeController(mock.Object, mock2.Object);

            IEnumerable<MainCategoryByCities> result = (IEnumerable<MainCategoryByCities>)controller.List().Model;
            MainCategoryByCities[] mainCategoryArray = result.ToArray();

            Assert.IsTrue(mainCategoryArray.Length == 5);
            Assert.AreEqual(mainCategoryArray[0].CityName, "Aleksandrów");
            Assert.AreEqual(mainCategoryArray[2].CityName, "Chełmno");
            Assert.AreEqual(mainCategoryArray[4].MainCategoryByCitiesId, 5);

        }

        [Test]
        public void Show_Topics_Test()
        {

            ICollection<IntermediateCategory> intercat = new List<IntermediateCategory> {

                          new IntermediateCategory(){IntermediateCategoryId=1 ,NameOfMainCategory="Zdrowie " ,MainCategoryByCitiesId=1    },
                         new IntermediateCategory(){IntermediateCategoryId=2 ,NameOfMainCategory="Kuchnia " ,MainCategoryByCitiesId=1     },
                          new IntermediateCategory(){IntermediateCategoryId=3 ,NameOfMainCategory="Sport " ,MainCategoryByCitiesId=1     },
                           new IntermediateCategory(){IntermediateCategoryId=4 ,NameOfMainCategory="Kino" ,MainCategoryByCitiesId=1     }

            }.ToList();

            ICollection<Topic> topics = new List<Topic> { new Topic { TopicId = 1, TopicData = "Data 1", TopicName = "Topic 1", IntermediateCategoryId = 1 },
                new Topic { TopicId = 2, TopicData = "Data 2", TopicName = "Topic 2", IntermediateCategoryId = 1 },
                new Topic { TopicId = 3, TopicData = "Data 3", TopicName = "Topic 3", IntermediateCategoryId = 1 }

           };


            Mock<IIntermediateCategoryRepository> mockI = new Mock<IIntermediateCategoryRepository>();
            mockI.Setup(m => m.Get(1)).Returns(new IntermediateCategory() { IntermediateCategoryId = 1, NameOfMainCategory = "Zdrowie ", MainCategoryByCitiesId = 1, Topic = topics });

            Mock<ITopicRepository> mockT = new Mock<ITopicRepository>();

            TopicController controller = new TopicController(mockT.Object, mockI.Object);

            IEnumerable<Topic> topicResult = (IEnumerable<Topic>)controller.Show_Topics(1).Model;

            ViewResult resultx = controller.Show_Topics(1) as ViewResult;
            string text = resultx.ViewName;

            Topic[] result = topicResult.ToArray();

            Assert.IsTrue(result.Length == 3);
            Assert.AreEqual(result[0].TopicId, 1);
            Assert.AreEqual(result[1].TopicName, "Topic 2");
            Assert.AreEqual(result[2].TopicData, "Data 3");

        }


        [Test]
        public void Show_Topics_Test_id_out_of_range()
        {

            ICollection<IntermediateCategory> intercat = new List<IntermediateCategory> {

                          new IntermediateCategory(){IntermediateCategoryId=1 ,NameOfMainCategory="Zdrowie " ,MainCategoryByCitiesId=1    },
                         new IntermediateCategory(){IntermediateCategoryId=2 ,NameOfMainCategory="Kuchnia " ,MainCategoryByCitiesId=1     },
                          new IntermediateCategory(){IntermediateCategoryId=3 ,NameOfMainCategory="Sport " ,MainCategoryByCitiesId=1     },
                           new IntermediateCategory(){IntermediateCategoryId=4 ,NameOfMainCategory="Kino" ,MainCategoryByCitiesId=1     },
                           new IntermediateCategory() { IntermediateCategoryId = 0, NameOfMainCategory = "Error",Topic=new List<Topic>()}
            }.ToList();

          


            Mock<IIntermediateCategoryRepository> mockI = new Mock<IIntermediateCategoryRepository>();
           
            mockI.Setup(m => m.Get(666)).Returns(new IntermediateCategory() { IntermediateCategoryId = 1, NameOfMainCategory = "Error",Topic=new List<Topic>()});
            Mock<ITopicRepository> mockT = new Mock<ITopicRepository>();

            TopicController controller = new TopicController(mockT.Object, mockI.Object);

            ViewResult result = controller.Show_Topics(666) as ViewResult ;

            string viewName =result.ViewName;

            Assert.AreEqual(result.ViewName.ToString(), "Error");

        }
















        [Test]
        public void Add_New_Topic_Test()
        {






            Mock<ITopicRepository> topicRepo = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> interRepo = new Mock<IIntermediateCategoryRepository>();


            TopicController controller = new TopicController(topicRepo.Object, interRepo.Object, () => "koral2323");

            int id = 5;

            int result = controller.Add_New_Topic(id).ViewBag.InterId;

            Assert.AreEqual(id, result);
            Assert.AreNotEqual(1, result);

        }


        [Test]
        public void Add_New_Topic_Route_Test()
        {
            Mock<ITopicRepository> topicRepo = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> interRepo = new Mock<IIntermediateCategoryRepository>();


            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("koral2323");
            principal.Setup(c => c.Identity.IsAuthenticated).Returns(true);

            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);







            TopicController controller = new TopicController(topicRepo.Object, interRepo.Object);
            controller.ControllerContext = controllerContext.Object;

            Topic topic = new Topic();
            topic.IntermediateCategoryId = 3;

            RedirectToRouteResult result = controller.Add_New_Topic(topic);

            Assert.AreEqual(result.RouteValues["action"], "Show_Topics");
            Assert.AreEqual(result.RouteValues["controller"], "Topic");
            Assert.AreEqual(result.RouteValues["id"], 3);








        }



        [Test]
        public void Go_To_Topic_Test1()
        {

            ICollection<IntermediateCategory> intercat = new List<IntermediateCategory> {

                          new IntermediateCategory(){IntermediateCategoryId=1 ,NameOfMainCategory="Zdrowie " ,MainCategoryByCitiesId=1    },
                         new IntermediateCategory(){IntermediateCategoryId=2 ,NameOfMainCategory="Kuchnia " ,MainCategoryByCitiesId=1     },
                          new IntermediateCategory(){IntermediateCategoryId=3 ,NameOfMainCategory="Sport " ,MainCategoryByCitiesId=1     },
                           new IntermediateCategory(){IntermediateCategoryId=4 ,NameOfMainCategory="Kino" ,MainCategoryByCitiesId=1     }



            }.ToList();


            Mock<IIntermediateCategoryRepository> interRepo = new Mock<IIntermediateCategoryRepository>();



            ICollection<Topic> topics = new List<Topic> { new Topic { TopicId = 1, TopicData = "Data 1", TopicName = "Topic 1", IntermediateCategoryId = 1 },
                new Topic { TopicId = 2, TopicData = "Data 2", TopicName = "Topic 2", IntermediateCategoryId = 1},
                new Topic { TopicId = 3, TopicData = "Data 3", TopicName = "Topic 3", IntermediateCategoryId = 1 }

           };


            //var roles = new List<ApplicationRoles> { new ApplicationRole { Id = "1" } };
            var appUserMock = new Mock<ApplicationUser>();
            appUserMock.SetupAllProperties();
            appUserMock.Setup(m => m.UserName).Returns("koral2323");

            var appUser = appUserMock.Object;

            appUser.Id = "c8ba6ee1-d2d0-49c0-983b-d76515b35218";


            Mock<ITopicRepository> topicRepo = new Mock<ITopicRepository>();


            ICollection<Comment> commentList = new List<Comment>() { new Comment() { ApplicationUserID = "1", CommentContent = "Comment Content", CommentID = 1, TopicID = 2 } };
            topicRepo.Setup(r => r.Get(3)).Returns(new Topic { TopicId = 3, TopicData = "Data 3", TopicName = "Topic 3", IntermediateCategoryId = 1, ApplicationUserID = "c8ba6ee1-d2d0-49c0-983b-d76515b35218", Comment = commentList, ApplicationUser = appUserMock.Object });


            TopicViewModel viewModel = new TopicViewModel();
            viewModel.comment_List.AddRange(commentList);
            viewModel.topic = new Topic { TopicId = 3, TopicData = "Data 3", TopicName = "Topic 3", IntermediateCategoryId = 1, ApplicationUserID = "c8ba6ee1-d2d0-49c0-983b-d76515b35218", Comment = commentList, ApplicationUser = appUserMock.Object };

            TopicController controller = new TopicController(topicRepo.Object, interRepo.Object);


            TopicViewModel result = (TopicViewModel)controller.Go_To_Topic(3).Model;


            Assert.AreEqual(viewModel.topic.TopicData, result.topic.TopicData);
            Assert.AreEqual(viewModel.topic.TopicId, result.topic.TopicId);
            Assert.AreEqual(viewModel.topic.TopicName, result.topic.TopicName);
            Assert.AreNotEqual(viewModel.topic.TopicData, "brak");

        }



        [Test]
        public void Go_To_Topic_Out_Of_range_test()
        {

           

            Mock<IIntermediateCategoryRepository> interRepo = new Mock<IIntermediateCategoryRepository>();

            

           Mock<ITopicRepository> topicRepo = new Mock<ITopicRepository>();


           
            topicRepo.Setup(r => r.Get(It.IsInRange<int>(50,1000,Range.Inclusive))).Returns(new Topic { TopicId = 0, TopicName = "Error", Comment = new List<Comment>() });


           

            TopicController controller = new TopicController(topicRepo.Object, interRepo.Object);


            ViewResult result = controller.Go_To_Topic(60) as ViewResult;

            Assert.AreEqual ("Error",result.ViewName);
          

        }




        [Test]
        public void Add_New_Comment_Test()
        {

            ICollection<Comment> commentList = new List<Comment>() { new Comment() { ApplicationUserID = "1", CommentContent = "Comment Content", CommentID = 1, TopicID = 2 } };

            Mock<ITopicRepository> topicRepo = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> interRepo = new Mock<IIntermediateCategoryRepository>();
            topicRepo.Setup(t => t.Get(3)).Returns(new Topic { TopicId = 3, TopicData = "Data 3", TopicName = "Topic 3", IntermediateCategoryId = 1, Comment = commentList });



            TopicController controller = new TopicController(topicRepo.Object, interRepo.Object);


            AddingCommentViewModel result = (AddingCommentViewModel)((controller.Add_New_Comment(3) as ViewResult).Model);

            AddingCommentViewModel check = new AddingCommentViewModel();
            check.topic = new Topic { TopicId = 3, TopicData = "Data 3", TopicName = "Topic 3", IntermediateCategoryId = 1, Comment = commentList };


            Assert.AreEqual(result.topic.TopicId, check.topic.TopicId);




        }

        [Test]
        public void Add_New_Comment_Test2()
        {




            Mock<ITopicRepository> topicRepo = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> interRepo = new Mock<IIntermediateCategoryRepository>();




            TopicController controller = new TopicController(topicRepo.Object, interRepo.Object, () => "koral2323");

            RedirectToRouteResult result = controller.Add_New_Comment(new Comment() { CommentContent = "None", CommentID = 1, TopicID = 1 });


            Assert.AreEqual(result.RouteValues["action"], "Go_To_Topic");
            Assert.AreEqual(result.RouteValues["controller"], "Topic");
            Assert.AreEqual(result.RouteValues["id"], 1);
            Assert.AreNotEqual(result.RouteValues["id"], 3);



        }




    }
}