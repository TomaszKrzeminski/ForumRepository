using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace ForumProject.Tests.Unit
{
    [TestFixture]
    class RouteTests
    {  

        [Test]
        public void TestIncomingRoutes1()
        {

            TestRouteMatch("~/", "Home", "Index");
          
        }
        [Test]
        public void TestIncomingRoutes2()
        {

           
            TestRouteMatch("~/Admin/Index", "Admin", "Index");
            
        }
        [Test]
        public void TestIncomingRoutes3()
        {

           
            TestRouteMatch("~/Forum/Admin/Index", "Admin", "Index");
            
        }
        [Test]
        public void TestIncomingRoutes4()
        {

            TestRouteMatch("~/Admin/ChangeCategories/1", "Admin", "ChangeCategories", new { id = 1 });
          
        }
        [Test]
        public void TestIncomingRoutes5()
        {

            
            TestRouteMatch("~/Admin/ChangeCategories/3/9", "Admin","ChangeCategories",new { id = 3, text = 9 });
        }

        private HttpContextBase CreateHttpContext(string targetUrl=null,string httpMethod="GET")
        {
            //imitacja żądania

            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            //tworzenie imitacji odpowiedzi

            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m=>m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);


            //tworzenie imitacji kontextu z użyciem żadania i odpowiedzi


            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            //zwraca imitację kontextu

            return mockContext.Object;

        }


        private void TestRouteMatch(string url,string controller,string action,object routeProperties=null,string httpMethod="GET")
        {

            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));


            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));


        }

        private bool TestIncomingRouteResult(RouteData routeResult,string controller,string action,object propertySet=null)
        {

            Func<object, object, bool> valCompare = (v1, v2) => { return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0; };

            bool result = valCompare(routeResult.Values["controller"], controller) && valCompare(routeResult.Values["action"], action);

            if(propertySet!=null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo  pi in propInfo)
                {
                    //if (!(routeResult.Values.ContainsKey(pi.Name) && valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null)))) { result = false;
                    //    break;
                    //}

                    if (!(routeResult.Values.ContainsKey(pi.Name) && valCompare((routeResult.Values[pi.Name]).ToString(), (pi.GetValue(propertySet, null)).ToString())))
                    {
                        result = false;
                        break;
                    }

                }
            }
            return result;
        }


        private void TestRouteFail(string url)
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            Assert.IsTrue(result == null || result.Route == null);

        }







    }
}
