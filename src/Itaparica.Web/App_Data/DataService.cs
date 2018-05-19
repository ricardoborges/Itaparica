using Castle.Core;
using Castle.MicroKernel;
using Itaparica.Core.Domain.Services;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Itaparica.Web.App_Data
{
    public class DataService : IService, IStartable
    {
        private readonly IKernel _kernel;

        public DataService(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Start()
        {
            new SchemaExport(_kernel.Resolve<Configuration>()).Create(true, true);
        }

        public void Stop()
        {
        }
    }
}