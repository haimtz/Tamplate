using Castle.Windsor;

namespace IoC.CastleWindsor
{
    public class CastleDependencyInjectionResolver : IDependencyInjectionResolver
    {
        private readonly IWindsorContainer _container;

        public CastleDependencyInjectionResolver(IWindsorContainer container)
        {
            _container = container;
        }

        public IWindsorContainer GetResolver => _container;
    }
}
