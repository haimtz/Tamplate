using Castle.Windsor;

namespace IoC.CastleWindsor
{
    public interface IDependencyInjectionResolver
    {
        IWindsorContainer GetResolver { get; }
    }
}
