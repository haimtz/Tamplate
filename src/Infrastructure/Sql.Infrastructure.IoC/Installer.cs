using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SuperCom.Infrastructure;
using System.Configuration;
using Common.Const;

namespace Sql.Infrastructure.IoC
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISqlConnection>()
                .ImplementedBy<SqlConnectionWrapper>()
                .UsingFactoryMethod(CreateSqlConnection));
        }

        private SqlConnectionWrapper CreateSqlConnection(IKernel kernel)
        {
            var connectionstring = ConfigurationManager.ConnectionStrings[Const.ConnectionStrings.CONNECTION_STRING].ToString();

            return new SqlConnectionWrapper(connectionstring);
        }
    }
}
