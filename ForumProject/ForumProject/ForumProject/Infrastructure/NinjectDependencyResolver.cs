using ForumProject.Concrete;
using ForumProject.Repository;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumProject.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
          return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ITopicRepository>().To<EFTopicRepository>();
            kernel.Bind<IMainCategoryByCitiesRepository>().To<EFMainCategoryByCitiesRepository>();
            kernel.Bind<IIntermediateCategoryRepository>().To<EFIntermediateCategoryRepository>();

        }

    }
}