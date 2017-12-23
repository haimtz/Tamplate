using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Configuration;
using Common;

namespace Sql.Infrastructure.IoC
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISqlConnection>()
                .ImplementedBy<SqlConnectionWrapper>()
                .UsingFactoryMethod(CreateSqlConnection));

            container.Register(Component.For<ISqlCommand>().ImplementedBy<SqlCommandWrapper>());
            container.Register(Component.For<ISqlCommandFactory>().ImplementedBy<SqlCommandFactory>());

            container.Register(Component.For<ISqlAdapter>().ImplementedBy<SqlAdapterWrapper>());
            container.Register(Component.For<ISqlAdapterFactory>().ImplementedBy<SqlAdapterFactory>());

            container.Register(Component.For<IDataTableContext>().ImplementedBy<DataTableContext>());
        }

        private SqlConnectionWrapper CreateSqlConnection(IKernel kernel)
        {
            var connectionstring = ConfigurationManager.ConnectionStrings[Const.ConnectionStrings.CONNECTION_STRING].ToString();

            return new SqlConnectionWrapper(connectionstring);
        }
    }
}
