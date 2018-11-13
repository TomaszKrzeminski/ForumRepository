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

            mockI.Setup(m => m.Get(666)).Returns(new IntermediateCategory() { IntermediateCategoryId = 1, NameOfMainCategory = "Error", Topic = new List<Topic>() });
            Mock<ITopicRepository> mockT = new Mock<ITopicRepository>();

            TopicController controller = new TopicController(mockT.Object, mockI.Object);

            ViewResult result = controller.Show_Topics(666) as ViewResult;

            string viewName = result.ViewName;

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

            RedirectToRouteResult result = controller.Add_New_Topic(topic) as RedirectToRouteResult;

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



            topicRepo.Setup(r => r.Get(It.IsInRange<int>(50, 1000, Range.Inclusive))).Returns(new Topic { TopicId = 0, TopicName = "Error", Comment = new List<Comment>() });




            TopicController controller = new TopicController(topicRepo.Object, interRepo.Object);


            ViewResult result = controller.Go_To_Topic(60) as ViewResult;

            Assert.AreEqual("Error", result.ViewName);


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

            RedirectToRouteResult result = controller.Add_New_Comment(new Comment() { CommentContent = "None", CommentID = 1, TopicID = 1 }) as RedirectToRouteResult;


            Assert.AreEqual(result.RouteValues["action"], "Go_To_Topic");
            Assert.AreEqual(result.RouteValues["controller"], "Topic");
            Assert.AreEqual(result.RouteValues["id"], 1);
            Assert.AreNotEqual(result.RouteValues["id"], 3);



        }


        [Test]
        public void ChangeCities_Returns_EmptyList()
        {

            MainCategoryByCities cities = new MainCategoryByCities();

            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();
            repository.Setup(x => x.GetAll()).Returns((IEnumerable<MainCategoryByCities>)null);

            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);


            List<MainCategoryByCities> list = (List<MainCategoryByCities>)((controller.ChangeCities() as ViewResult).Model);


            Assert.AreEqual(0, list.Count);



        }


        [Test]
        public void ChangeCities_Returns_List()
        {


            List<MainCategoryByCities> listofMain = new List<MainCategoryByCities>() {

                   new MainCategoryByCities(){ MainCategoryByCitiesId=1,CityName="Aleksandrów"  },
                    new MainCategoryByCities(){ MainCategoryByCitiesId=2,CityName="Bydgoszcz"  },
                     new MainCategoryByCities(){ MainCategoryByCitiesId=3,CityName="Chełmno"  },
                      new MainCategoryByCities(){ MainCategoryByCitiesId=4,CityName="Grudziądz"  },
                       new MainCategoryByCities(){ MainCategoryByCitiesId=5,CityName="Świecie"  }
 };

            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();
            repository.Setup(m => m.GetAll()).Returns(new MainCategoryByCities[]
             {
                   new MainCategoryByCities(){ MainCategoryByCitiesId=1,CityName="Aleksandrów"  },
                    new MainCategoryByCities(){ MainCategoryByCitiesId=2,CityName="Bydgoszcz"  },
                     new MainCategoryByCities(){ MainCategoryByCitiesId=3,CityName="Chełmno"  },
                      new MainCategoryByCities(){ MainCategoryByCitiesId=4,CityName="Grudziądz"  },
                       new MainCategoryByCities(){ MainCategoryByCitiesId=5,CityName="Świecie"  }


             });

            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);


            List<MainCategoryByCities> list = (List<MainCategoryByCities>)((controller.ChangeCities() as ViewResult).Model);


            Assert.AreEqual(listofMain[0].CityName, list[0].CityName);
            Assert.AreEqual(listofMain[1].CityName, list[1].CityName);
            Assert.AreNotEqual(listofMain[2].CityName, list[3].CityName);

        }


        [Test]
        public void ChangeCategories_Returns_List()
        {

            MainCategoryByCities city = new MainCategoryByCities()
            {
                MainCategoryByCitiesId = 5,
                CityName = "Świecie",
                IntermediateCategory = new List<IntermediateCategory>()
            {


                  new IntermediateCategory(){IntermediateCategoryId=1 ,NameOfMainCategory="Zdrowie" ,MainCategoryByCitiesId=5    },
                         new IntermediateCategory(){IntermediateCategoryId=2 ,NameOfMainCategory="Kuchnia" ,MainCategoryByCitiesId=5     },
                          new IntermediateCategory(){IntermediateCategoryId=3 ,NameOfMainCategory="Sport" ,MainCategoryByCitiesId=5     },
                           new IntermediateCategory(){IntermediateCategoryId=4 ,NameOfMainCategory="Kino" ,MainCategoryByCitiesId=5     }




            }
            };


            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();
            repository.Setup(m => m.Get(5)).Returns(city);

            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);


            List<IntermediateCategory> list = (List<IntermediateCategory>)((controller.ChangeCategories(5) as ViewResult).Model);


            Assert.AreEqual("Kuchnia", list[1].NameOfMainCategory);
            Assert.AreEqual(2, list[1].IntermediateCategoryId);
            Assert.AreNotEqual("Brak", list[3].NameOfMainCategory);


        }





        [Test]
        public void ChangeCategories_Returns_New_List()
        {

            MainCategoryByCities city = new MainCategoryByCities()
            {
                MainCategoryByCitiesId = 0,
                CityName = "None",

            };


            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();
            repository.Setup(m => m.Get(666)).Returns(city);

            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);


            ViewResult result = controller.ChangeCategories(666) as ViewResult;

            Assert.AreEqual(result.ViewName, "Error");



        }

        [Test]
        public void AddCategory_Has_Specified_Id()
        {
            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();
            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            IntermediateCategory category = (IntermediateCategory)((controller.AddCategory(1) as ViewResult).Model);

            Assert.AreEqual(1, category.MainCategoryByCitiesId);




        }


        [Test]
        public void Add_Category_Returns_True()
        {
            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();
            repository.Setup(r => r.AddIntermediateCategory(It.IsAny<IntermediateCategory>())).Returns(true);
            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            IntermediateCategory category = new IntermediateCategory();

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            ViewResult result = controller.AddCategory(category) as ViewResult;
            string viewName = result.ViewName;

            Assert.AreEqual("Error", viewName);



        }

        [Test]
        public void Add_Category_Returns_False()
        {

            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();
            repository.Setup(r => r.AddIntermediateCategory(It.IsAny<IntermediateCategory>())).Returns(false);
            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            IntermediateCategory category = new IntermediateCategory();
            category.MainCategoryByCitiesId = 12;

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            RedirectToRouteResult result = controller.AddCategory(category) as RedirectToRouteResult;


            Assert.AreEqual(result.RouteValues["action"], "ChangeCategories");
            Assert.AreEqual(result.RouteValues["controller"], "Admin");
            Assert.AreEqual(result.RouteValues["id"], category.MainCategoryByCitiesId);


        }


        [Test]
        public void DeleteCategory()
        {



            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();


            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();

            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();
            repositoryInter.Setup(r => r.Get(2)).Returns(new IntermediateCategory() { IntermediateCategoryId = 2, NameOfMainCategory = "Zdrowie ", MainCategoryByCitiesId = 1 });
            repositoryInter.Setup(re => re.Remove(It.IsAny<IntermediateCategory>())).Returns(true);

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            RedirectToRouteResult result = controller.DeleteCategory(2) as RedirectToRouteResult;


            Assert.AreEqual(result.RouteValues["action"], "ChangeCategories");
            Assert.AreEqual(result.RouteValues["controller"], "Admin");
            Assert.AreEqual(result.RouteValues["id"], 1);



        }

        [Test]
        public void DeleteCategory_One_Left()
        {

            //Write Test change method

            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();


            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();

            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();
            repositoryInter.Setup(r => r.Get(2)).Returns(new IntermediateCategory() { IntermediateCategoryId = 2, NameOfMainCategory = "Zdrowie ", MainCategoryByCitiesId = 1 });
            repositoryInter.Setup(re => re.Remove(It.IsAny<IntermediateCategory>())).Returns(false);

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            ViewResult result = controller.DeleteCategory(2) as ViewResult;



            Assert.AreEqual("You have to keep at least one Category", controller.ViewBag.ErrorMessage);

            Assert.AreEqual("Error", result.ViewName);



        }



        [Test]
        public void EditCategory_Returns_Specified_Result()
        {



            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();


            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();

            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();
            repositoryInter.Setup(r => r.Get(2)).Returns(new IntermediateCategory() { IntermediateCategoryId = 2, NameOfMainCategory = "Zdrowie", MainCategoryByCitiesId = 1 });


            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            IntermediateCategory result = (IntermediateCategory)(controller.EditCategory(2) as ViewResult).Model;




            Assert.AreEqual("Zdrowie", result.NameOfMainCategory);



        }


        [Test]
        public void EditCategory_Returns_View_Error_When_Id_Doesnt_Found()
        {



            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();


            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();

            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();
            repositoryInter.Setup(r => r.Get(666)).Returns(new IntermediateCategory() { IntermediateCategoryId = 0, NameOfMainCategory = "Error", MainCategoryByCitiesId = 0 });


            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            ViewResult result = controller.EditCategory(666) as ViewResult;




            Assert.AreEqual("Error", result.ViewName);
            Assert.AreEqual("Category with that Id does not exists", controller.ViewBag.ErrorMessage);


        }


        //[Test]
        //public void EditCategory_Returns_View_Error_When_Category_Exists()
        //{



        //    Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();


        //    Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();

        //    Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();
        //    repositoryInter.Setup(r => r.Get(666)).Returns(new IntermediateCategory() { IntermediateCategoryId = 0, NameOfMainCategory = "Error", MainCategoryByCitiesId = 0 });


        //    AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

        //    ViewResult result = controller.EditCategory(666) as ViewResult;




        //    Assert.AreEqual("Error", result.ViewName);
        //    Assert.AreEqual("Category with that Id does not exists", controller.ViewBag.ErrorMessage);


        //}





        [Test]
        public void EditCategory_Check_If_Category_Exists_Returns_Category()
        {



            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();


            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();

            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();
            repositoryInter.Setup(r => r.Get(2)).Returns(new IntermediateCategory() { IntermediateCategoryId = 2, NameOfMainCategory = "Zdrowie", MainCategoryByCitiesId = 1 });


            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            IntermediateCategory result = (IntermediateCategory)(controller.EditCategory(2) as ViewResult).Model;




            Assert.AreEqual("Zdrowie", result.NameOfMainCategory);



        }



        [Test]
        public void EditCategory_Returns_View_Error_When_Category_With_Same_Name_Exists()
        {

            IntermediateCategory category = new IntermediateCategory();
            category.IntermediateCategoryId = 1;
            category.MainCategoryByCitiesId = 2;
            category.NameOfMainCategory = "Zdrowie";
            category.Topic = new List<Topic>();



            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();


            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();

            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();
            repositoryInter.Setup(r => r.ChangeIntermediateCategory(It.IsAny<IntermediateCategory>())).Returns(false);


            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            ViewResult result = controller.EditCategory(category) as ViewResult;




            Assert.AreEqual("Error", result.ViewName);
            Assert.AreEqual("Category Exists", controller.ViewBag.ErrorMessage);


        }




        [Test]
        public void EditCategory_Redirects_To_ChangeCategories()
        {

            IntermediateCategory category = new IntermediateCategory();
            category.IntermediateCategoryId = 1;
            category.MainCategoryByCitiesId = 2;
            category.NameOfMainCategory = "Zdrowie";
            category.Topic = new List<Topic>();



            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();


            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();

            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();
            repositoryInter.Setup(r => r.ChangeIntermediateCategory(It.IsAny<IntermediateCategory>())).Returns(true);
            repositoryInter.Setup(x => x.Get(1)).Returns(category);

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            RedirectToRouteResult result = controller.EditCategory(category) as RedirectToRouteResult;




            Assert.AreEqual(result.RouteValues["controller"], "Admin");
            Assert.AreEqual(result.RouteValues["action"], "ChangeCategories");
            Assert.AreEqual(result.RouteValues["id"], category.MainCategoryByCitiesId);



        }



        [Test]
        public void Delete_Topic_Redirects_To_Show_Topics()
        {

            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();
            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            repositoryTopic.Setup(t => t.Get(It.IsAny<int>())).Returns(new Topic() { TopicName = "Topic Name", TopicData = "text text text text", TopicId = 1, IntermediateCategoryId = 5 });


            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            RedirectToRouteResult result = controller.DeleteTopic(1) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["controller"], "Topic");
            Assert.AreEqual(result.RouteValues["action"], "Show_Topics");
            Assert.AreEqual(result.RouteValues["id"], 5);



        }


        [Test]
        public void RemoveComment_Returns_View_Error_Database_Problem()
        {

            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();
            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            repositoryTopic.Setup(t => t.GetTopicIdCommentId(It.IsAny<int>())).Returns(1);
            repositoryTopic.Setup(x => x.DeleteCommentFromTopic(It.IsAny<int>())).Returns(false);


            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            ViewResult result = controller.RemoveComment(1) as ViewResult;

            Assert.AreEqual("Error", result.ViewName);
            Assert.AreEqual("Problem with delecting Comment", controller.ViewBag.ErrorMessage);

        }


        [Test]
        public void RemoveComment_Redirects_To_Go_To_Topic()
        {

            Mock<IMainCategoryByCitiesRepository> repository = new Mock<IMainCategoryByCitiesRepository>();
            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            repositoryTopic.Setup(t => t.GetTopicIdCommentId(It.IsAny<int>())).Returns(1);
            repositoryTopic.Setup(x => x.DeleteCommentFromTopic(It.IsAny<int>())).Returns(true);


            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repository.Object);

            RedirectToRouteResult result = controller.RemoveComment(1) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["controller"], "Topic");
            Assert.AreEqual(result.RouteValues["action"], "Go_To_Topic");
            Assert.AreEqual(result.RouteValues["id"], 1);









        }




        //Model Validation Tests


        [Test]
        public void Add_New_Topic_Dont_Save_InValid_Changes()
        {
            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            TopicController controller = new TopicController(repositoryTopic.Object,repositoryInter.Object,()=>"ewa2323");
            Topic topic = new Topic() {TopicName=null,TopicData=null };
            controller.ModelState.AddModelError("TopicName", "Pole nie może pozostawać puste");
            controller.ModelState.AddModelError("TopicData", "Pole nie może pozostawać puste");
            ActionResult result = controller.Add_New_Topic(topic);

            repositoryTopic.Verify(x => x.Add_To_Topics_And_User(It.IsAny<Topic>(),It.IsAny<string>()), Times.Never);


        }

        [Test]
        public void Add_New_Topic_Dont_Save_Changes_Return_View_Add_New_Topic()
        {
            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            TopicController controller = new TopicController(repositoryTopic.Object, repositoryInter.Object, () => "ewa2323");
            Topic topic = new Topic() { TopicName = null, TopicData = null };
            controller.ModelState.AddModelError("TopicName", "Pole nie może pozostawać puste");
            controller.ModelState.AddModelError("TopicData", "Pole nie może pozostawać puste");
            ViewResult result =(ViewResult)controller.Add_New_Topic(topic);
            Assert.AreEqual( "Add_New_Topic", result.ViewName);
           


        }



        [Test]
        public void Add_New_Comment_Dont_Save_InValid_Changes()
        {
            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();

            TopicController controller = new TopicController(repositoryTopic.Object, repositoryInter.Object, () => "ewa2323");
           Comment comment = new Comment() { CommentContent = null};
            controller.ModelState.AddModelError("CommentContent", "Pole nie może pozostawać puste");
          
            ActionResult result = controller.Add_New_Comment(comment);
            ViewResult resultView = (ViewResult)result;

            repositoryTopic.Verify(x => x.Add_Comment(It.IsAny<Comment>(), It.IsAny<string>()), Times.Never);
            Assert.AreEqual(resultView.ViewName, "Add_New_Comment");

        }


        [Test]
        public void Add_Category_Dont_Save_InValid_Changes()
        {
            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();
            Mock<IMainCategoryByCitiesRepository> repositoryMain = new Mock<IMainCategoryByCitiesRepository>();

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repositoryMain.Object);
            IntermediateCategory category = new IntermediateCategory() { NameOfMainCategory=null };
            controller.ModelState.AddModelError("NameOfMainCategory", "Pole nie może pozostawać puste");

            ActionResult result = controller.AddCategory(category);
            ViewResult resultView = (ViewResult)result;

            repositoryMain.Verify(x => x.AddIntermediateCategory(It.IsAny<IntermediateCategory>()),Times.Never);
            Assert.AreEqual(resultView.ViewName, "AddCategory");

      
        }

        [Test]
        public void Edit_Category_Dont_Save_InValid_Changes()
        {
            Mock<ITopicRepository> repositoryTopic = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> repositoryInter = new Mock<IIntermediateCategoryRepository>();
            Mock<IMainCategoryByCitiesRepository> repositoryMain = new Mock<IMainCategoryByCitiesRepository>();

            AdminController controller = new AdminController(repositoryTopic.Object, repositoryInter.Object, repositoryMain.Object);
            IntermediateCategory category = new IntermediateCategory() { NameOfMainCategory = null };
            controller.ModelState.AddModelError("NameOfMainCategory", "Pole nie może pozostawać puste");

            ActionResult result = controller.EditCategory(category);
            ViewResult resultView = (ViewResult)result;

            repositoryInter.Verify(x => x.ChangeIntermediateCategory(It.IsAny<IntermediateCategory>()), Times.Never);
            Assert.AreEqual(resultView.ViewName, "EditCategory");


        }









    }
}