using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IoC.CastleWindsor
{
    public class DependencyResolverFactory
    {
        private static readonly IWindsorContainer CONTAINER = new WindsorContainer();
        private static bool _isInitialize = false;

        public static void Initialize(string[] assembliesNames = null, bool includeCommon = true)
        {
            if (_isInitialize)
                return;
            try
            {
                var collectionResolver = new ArrayResolver(CONTAINER.Kernel, true);
                CONTAINER.Kernel.Resolver.AddSubResolver(collectionResolver);

                var installersList = new List<IWindsorInstaller>();


                if (assembliesNames != null)
                {
                    installersList.AddRange(assembliesNames.Select(FromAssembly.Named));
                }

                CONTAINER.Install(installersList.ToArray());

                _isInitialize = true;
            }
            catch (FileLoadException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to apply Castle Windsor installer from the assembly " + exception.Message, exception);
            }
        }

        public static IDependencyInjectionResolver GetResolver()
        {
            return new CastleDependencyInjectionResolver(CONTAINER);
        }


        public static IDependencyInjectionResolver GetResolverForTests()
        {
            return new CastleDependencyInjectionResolver(new WindsorContainer());
        }
    }
}
