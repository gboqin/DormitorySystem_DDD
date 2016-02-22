using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace DormitorySystem.Repositories.Factories
{
    public class NinjectDependencyResolverForWebApi: NinjectDependencyScope, IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolverForWebApi(IKernel kernel)
            : base(kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel);
        }
    }
}
