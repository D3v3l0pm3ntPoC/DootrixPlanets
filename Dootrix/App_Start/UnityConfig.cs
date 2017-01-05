using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Web.Http;

namespace Dootrix.Planets.Api.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        /// <summary>Registers the type mappings specified in the unity configuration file with the Unity container
        /// and subsequently lazy loads the container.</summary>
        /// <param name="container">The unity container to configure.</param>
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            IUnityContainer container = new UnityContainer().LoadConfiguration();
            container.RegisterInstance<HttpConfiguration>(GlobalConfiguration.Configuration);

            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion
    }
}
