using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Itaparica.Core;
using Itaparica.Web.App_Data;
using Itaparica.Web.Infra.Container;

namespace Itaparica.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            SetupContainer();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void SetupContainer()
        {
            _container = new DefaultContainer();

            ((DefaultContainer)_container).SetupForWeb();

            _container.Install(new WindsorControllerInstaller());
            var windsorControllerFactory = new WindsorControllerFactory(_container);

            _container.Register(Component.For<DataService>());


            ControllerBuilder.Current.SetControllerFactory(windsorControllerFactory);
        }
    }
}
