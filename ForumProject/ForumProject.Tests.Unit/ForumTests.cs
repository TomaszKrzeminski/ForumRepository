using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ForumProject.Repository;
using ForumProject.Entities;
using ForumProject.Controllers;
using System.Web.Mvc;
using System.Web;
using System.Security.Principal;
using System.IO;
using ForumProject.Models;

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
            mockI.Setup(m => m.Get(1)).Returns(new IntermediateCategory() { IntermediateCategoryId = 1, NameOfMainCategory = "Zdrowie ", MainCategoryByCitiesId = 1,Topic=topics });







            Mock<ITopicRepository> mockT = new Mock<ITopicRepository>();




              TopicController controller = new TopicController(mockT.Object, mockI.Object);

            IEnumerable<Topic> topicResult = (IEnumerable<Topic>)controller.Show_Topics(1).Model;

            Topic[] result = topicResult.ToArray();

            Assert.IsTrue(result.Length == 3);
            Assert.AreEqual(result[0].TopicId, 1);
            Assert.AreEqual(result[1].TopicName, "Topic 2");
            Assert.AreEqual(result[2].TopicData, "Data 3");
    




        }




        [Test]
        public void Add_New_Topic_Test()
        {






            Mock<ITopicRepository> topicRepo = new Mock<ITopicRepository>();
            Mock<IIntermediateCategoryRepository> interRepo = new Mock<IIntermediateCategoryRepository>();


            TopicController controller = new TopicController(topicRepo.Object, interRepo.Object);

            int id = 5;

            int result =controller.Add_New_Topic(id).ViewBag.InterId;

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

            ICollection<Topic> topics = new List<Topic> { new Topic { TopicId = 1, TopicData = "Data 1", TopicName = "Topic 1", IntermediateCategoryId = 1 },
                new Topic { TopicId = 2, TopicData = "Data 2", TopicName = "Topic 2", IntermediateCategoryId = 1},
                new Topic { TopicId = 3, TopicData = "Data 3", TopicName = "Topic 3", IntermediateCategoryId = 1 }

           };

            Mock<ITopicRepository> topicRepo = new Mock<ITopicRepository>();

            ICollection<Comment> commentList = new List<Comment>() { new Comment() { ApplicationUserID = "1", CommentContent = "Comment Content", CommentID = 1, TopicID = 2 } };
            topicRepo.Setup(r => r.Get(3)).Returns(new Topic { TopicId = 2, TopicData = "Data 2", TopicName = "Topic 2", IntermediateCategoryId = 1 });

            ApplicationUser user = new ApplicationUser();
            user.Topics.








        }












        //        /// <summary>
        //        /// ////////////////
        //        /// </summary>






        //        [Test]
        //        public void when_passing_MainCategoryId_you_get_specified_IntermediateCategory()
        //        {
        //            List<IntermediateCategory> InterList = new List<IntermediateCategory>()
        //            {
        //                new IntermediateCategory(){IntermediateCategoryId=1 ,NameOfMainCategory="Zdrowie " ,MainCategoryByCitiesId=1    },
        //                 new IntermediateCategory(){IntermediateCategoryId=2 ,NameOfMainCategory="Kuchnia " ,MainCategoryByCitiesId=1     },
        //                  new IntermediateCategory(){IntermediateCategoryId=3 ,NameOfMainCategory="Sport " ,MainCategoryByCitiesId=1     },
        //                   new IntermediateCategory(){IntermediateCategoryId=4 ,NameOfMainCategory="Kino" ,MainCategoryByCitiesId=1     }


        //            };

        //            List<IntermediateCategory> InterList2 = new List<IntermediateCategory>()
        //            {
        //                new IntermediateCategory(){IntermediateCategoryId=5 ,NameOfMainCategory="Zdrowie " ,MainCategoryByCitiesId=2     },
        //                 new IntermediateCategory(){IntermediateCategoryId=6 ,NameOfMainCategory="Kuchnia " ,MainCategoryByCitiesId=2     },
        //                  new IntermediateCategory(){IntermediateCategoryId=7 ,NameOfMainCategory="Sport " ,MainCategoryByCitiesId=2     },
        //                   new IntermediateCategory(){IntermediateCategoryId=8 ,NameOfMainCategory="Kino",MainCategoryByCitiesId=2      }


        //            };

        //            List<IntermediateCategory> InterList3 = new List<IntermediateCategory>()
        //            {
        //                new IntermediateCategory(){IntermediateCategoryId=9 ,NameOfMainCategory="Zdrowie " , MainCategoryByCitiesId=3    },
        //                 new IntermediateCategory(){IntermediateCategoryId=10 ,NameOfMainCategory="Kuchnia ",MainCategoryByCitiesId=3     },
        //                  new IntermediateCategory(){IntermediateCategoryId=11 ,NameOfMainCategory="Sport ", MainCategoryByCitiesId=3    },
        //                   new IntermediateCategory(){IntermediateCategoryId=12 ,NameOfMainCategory="Kino" ,MainCategoryByCitiesId=3    }


        //            };
        //            List<IntermediateCategory> InterList4 = new List<IntermediateCategory>()
        //            {
        //                new IntermediateCategory(){IntermediateCategoryId=13 ,NameOfMainCategory="Zdrowie " ,MainCategoryByCitiesId=4   },
        //                 new IntermediateCategory(){IntermediateCategoryId=14 ,NameOfMainCategory="Kuchnia ",MainCategoryByCitiesId=4     },
        //                  new IntermediateCategory(){IntermediateCategoryId=15 ,NameOfMainCategory="Sport ",MainCategoryByCitiesId=4       },
        //                   new IntermediateCategory(){IntermediateCategoryId=16 ,NameOfMainCategory="Kino" ,MainCategoryByCitiesId=4      }


        //            };


        //            List<IntermediateCategory> InterList5 = new List<IntermediateCategory>()
        //            {
        //                new IntermediateCategory(){IntermediateCategoryId=17 ,NameOfMainCategory="Zdrowie "     },
        //                 new IntermediateCategory(){IntermediateCategoryId=18 ,NameOfMainCategory="Kuchnia "     },
        //                  new IntermediateCategory(){IntermediateCategoryId=19 ,NameOfMainCategory="Sport "     },
        //                   new IntermediateCategory(){IntermediateCategoryId=20 ,NameOfMainCategory="Kino"     }


        //            };




        //            Mock<IMainCategoryByCitiesRepository> mock = new Mock<IMainCategoryByCitiesRepository>();
        //            mock.Setup(x => x.MainCategoryByCities).Returns(new MainCategoryByCities[]
        //            {
        //                new MainCategoryByCities(){ MainCategoryByCitiesId=1,CityName="Aleksandrów",IntermediateCategory=InterList  },
        //                new MainCategoryByCities(){ MainCategoryByCitiesId=2,CityName="Bydgoszcz",IntermediateCategory=InterList2  },
        //                 new MainCategoryByCities(){ MainCategoryByCitiesId=3,CityName="Chełmno",IntermediateCategory=InterList3  },
        //                  new MainCategoryByCities(){ MainCategoryByCitiesId=4,CityName="Grudziądz",IntermediateCategory=InterList4  },
        //                   new MainCategoryByCities(){ MainCategoryByCitiesId=5,CityName="Świecie",IntermediateCategory=InterList5  }


        //            });


        //            //Mock<IIntermediateCategoryRepository> mock2 = new Mock<IIntermediateCategoryRepository>();
        //            //mock2.Setup(x => x.IntermediateCategory).Returns(new IntermediateCategory[]
        //            //{
        //            //     new IntermediateCategory(){IntermediateCategoryId=1 ,NameOfMainCategory="Zdrowie "     },
        //            //     new IntermediateCategory(){IntermediateCategoryId=2 ,NameOfMainCategory="Kuchnia "     },
        //            //      new IntermediateCategory(){IntermediateCategoryId=3 ,NameOfMainCategory="Sport "     },
        //            //       new IntermediateCategory(){IntermediateCategoryId=4 ,NameOfMainCategory="Kino"     },
        //            //       new IntermediateCategory(){IntermediateCategoryId=5 ,NameOfMainCategory="Zdrowie "     },
        //            //     new IntermediateCategory(){IntermediateCategoryId=6 ,NameOfMainCategory="Kuchnia "     },
        //            //      new IntermediateCategory(){IntermediateCategoryId=7 ,NameOfMainCategory="Sport "     },
        //            //       new IntermediateCategory(){IntermediateCategoryId=8 ,NameOfMainCategory="Kino"     },
        //            //        new IntermediateCategory(){IntermediateCategoryId=9 ,NameOfMainCategory="Zdrowie "     },
        //            //     new IntermediateCategory(){IntermediateCategoryId=10 ,NameOfMainCategory="Kuchnia "     },
        //            //      new IntermediateCategory(){IntermediateCategoryId=11 ,NameOfMainCategory="Sport "     },
        //            //       new IntermediateCategory(){IntermediateCategoryId=12 ,NameOfMainCategory="Kino"     },
        //            //        new IntermediateCategory(){IntermediateCategoryId=13 ,NameOfMainCategory="Zdrowie "     },
        //            //     new IntermediateCategory(){IntermediateCategoryId=14 ,NameOfMainCategory="Kuchnia "     },
        //            //      new IntermediateCategory(){IntermediateCategoryId=15 ,NameOfMainCategory="Sport "     },
        //            //       new IntermediateCategory(){IntermediateCategoryId=16 ,NameOfMainCategory="Kino"     },
        //            //       new IntermediateCategory(){IntermediateCategoryId=17 ,NameOfMainCategory="Zdrowie "     },
        //            //     new IntermediateCategory(){IntermediateCategoryId=18 ,NameOfMainCategory="Kuchnia "     },
        //            //      new IntermediateCategory(){IntermediateCategoryId=19 ,NameOfMainCategory="Sport "     },
        //            //       new IntermediateCategory(){IntermediateCategoryId=20 ,NameOfMainCategory="Kino"     }


        //            //});

        //            //HomeController controll = new HomeController(mock.Object, mock2.Object);

        //            //IEnumerable<IntermediateCategory> interList_To_Test = (IEnumerable<IntermediateCategory>)controll.Show_IntermediateCategory_List(1).Model;

        //            //     IntermediateCategory[] inter=  interList_To_Test.ToArray();           



        //            //Console.WriteLine(interList_To_Test.ToList().First().NameOfMainCategory);

        //            //IIntermediateCategoryRepository repository = mock2.Object;



        //            //IEnumerable<IntermediateCategory> inter = repository.GetIntermediateCategory_ById(1);

        //            //Assert.AreEqual(inter.ToArray().First(), "Zdrowie ");


        //        }

        //        //[Test]
        //        //public void Get_Topic_By_Id_()
        //        //{
        //        //    // Arrange
        //        //    var testObject = new Topic();
        //        //    testObject.TopicId = 1;
        //        //    testObject.TopicName = "New Topic";

        //        //    var context = new Mock<ApplicationDbContext>();
        //        //    var dbSetMock = new Mock<DbSet<Topic>>();

        //        //    context.Setup(x => x.Topics).Returns(dbSetMock.Object);
        //        //    dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(testObject);

        //        //    // Act
        //        //    var repository = new EFTopicRepository(context.Object);
        //        //    Topic topic = repository.Get_Topic_By_Id(1);

        //        //    // Assert
        //        //    context.Verify(x => x.Topics);
        //        //    dbSetMock.Verify(x => x.Find(It.IsAny<int>()));
        //        //    Assert.AreEqual(topic.TopicName, "New Topic");
        //        //}

        //        //[Test]
        //        //public void Give_Main_Category_Id_and_GetIntermediateCategory_ById()
        //        //{
        //        //    var testObjMain = new MainCategoryByCities();
        //        //    var testList = new List<MainCategoryByCities>() {  };


        //        //    IntermediateCategory cat1 = new IntermediateCategory() {MainCategoryByCitiesId=3, IntermediateCategoryId = 1, NameOfMainCategory = "Zdrowie " };
        //        //    IntermediateCategory cat2 = new IntermediateCategory() { MainCategoryByCitiesId = 3, IntermediateCategoryId = 2, NameOfMainCategory = "Kuchnia " };
        //        //    IntermediateCategory cat3 = new IntermediateCategory() { MainCategoryByCitiesId = 3, IntermediateCategoryId = 3, NameOfMainCategory = "Sport " };
        //        //    IntermediateCategory cat4 = new IntermediateCategory() { MainCategoryByCitiesId = 3, IntermediateCategoryId = 4, NameOfMainCategory = "Kino" };




        //        //    testObjMain.MainCategoryByCitiesId = 3;
        //        //    testObjMain.CityName = "Chełmno";
        //        //    testObjMain.IntermediateCategory = new List<IntermediateCategory>();
        //        //    testObjMain.IntermediateCategory.Add(cat1);
        //        //    testObjMain.IntermediateCategory.Add(cat2);
        //        //    testObjMain.IntermediateCategory.Add(cat3);
        //        //    testObjMain.IntermediateCategory.Add(cat4);

        //        //    testList.Add(testObjMain);

        //        //    var dbSetMock = new Mock<DbSet<MainCategoryByCities>>();
        //        //    dbSetMock.As<IQueryable<MainCategoryByCities>>().Setup(x => x.Provider).Returns
        //        //                                               (testList.AsQueryable().Provider);
        //        //    dbSetMock.As<IQueryable<MainCategoryByCities>>().Setup(x => x.Expression).Returns
        //        //                                               (testList.AsQueryable().Expression);
        //        //    dbSetMock.As<IQueryable<MainCategoryByCities>>().Setup(x => x.ElementType).Returns
        //        //                                               (testList.AsQueryable().ElementType);
        //        //    dbSetMock.As<IQueryable<MainCategoryByCities>>().Setup(x => x.GetEnumerator()).Returns
        //        //                                               (testList.AsQueryable().GetEnumerator());

        //        //    dbSetMock.Setup(x => x.Where(z => z.MainCategoryByCitiesId == 3)).Returns(testList.AsQueryable);
        //        //    var context = new Mock<ApplicationDbContext>();
        //        //    context.Setup(x => x.MainCategoryByCities).Returns(dbSetMock.Object);

        //        //    var repository = new EFIntermediateCategoryRepository(context.Object);
        //        //    var y = repository.IntermediateCategory;
        //        //    IEnumerable<IntermediateCategory> list = repository.GetIntermediateCategory_ById(3);

        //        //    Assert.AreEqual(testObjMain.IntermediateCategory, list);


        //        //}





    }
}
