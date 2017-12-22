using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SuperCom.Host.Controllers;

namespace SuperCom.Host
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ValuesController>().ImplementedBy<ValuesController>());
        }
    }
}