using Microsoft.Owin.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dootrix.Planets.Api.Tests.IntegrationTests
{
    [SetUpFixture]
    public class BootstrapSelfHostServer
    {
        private IDisposable hostServer;
        public const string BASE_ADDRESS = "http://localhost:48929/";

        [OneTimeSetUp]
        public void RunServer()
        {
            hostServer = WebApp.Start<Startup>(BASE_ADDRESS);
        }

        [OneTimeTearDown]
        public void ShutdownServer()
        {
            hostServer?.Dispose();
        }
    }
}
