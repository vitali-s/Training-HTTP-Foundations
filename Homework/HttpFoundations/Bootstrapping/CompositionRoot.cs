using System.Web.Http.Dependencies;
using HttpFoundations.Queries;
using Microsoft.Practices.Unity;

namespace HttpFoundations.Bootstrapping
{
    public class CompositionRoot
    {
        public IDependencyResolver Register()
        {
            var container = new UnityContainer();

            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default);

            container.RegisterType<ICompanyQuery, CompanyQuery>(new ContainerControlledLifetimeManager());

            return new UnityDependencyResolver(container);
        }
    }
}
