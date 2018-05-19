using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Itaparica.Core.Domain.Controller;
using Itaparica.Web.Controllers;

namespace Itaparica.Web.Infra.Container
{
    /// <summary>
    /// Registra todos os controllers da aplicação no Windsor Container
    /// </summary>
    public class WindsorControllerInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Registra todos os controllers da aplicação.
        /// </summary>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<HomeController>()
                 .BasedOn(typeof(IItaparicaController))
                 .Configure(c => c.LifestyleTransient()));
        }
    }
}