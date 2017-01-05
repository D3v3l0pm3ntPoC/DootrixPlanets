using Dootrix.Planets.DataAccess.Model;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dootrix.Planets.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class PlanetsControllerTest
    {
        private HttpClient httpClient;
        private const int NO_OF_PLANETS = 9;
        private const string remoteRestApi = "http://planets.olan-george.com/";

        [SetUp]
        public void Configure()
        {
            //This was used during development prior to hosting the rest api
            //httpClient = new HttpClient { BaseAddress = new Uri(BootstrapSelfHostServer.BASE_ADDRESS + "api/Planets/ListAll") };

            //Remote version of REST api
            httpClient = new HttpClient { BaseAddress = new Uri(remoteRestApi) };
        }

        [Test]
        public void Planets_ListAll_ShouldPass()
        {
            HttpResponseMessage response = httpClient.GetAsync("api/Planets/ListAll").Result;
            Assert.That(response != null && response.StatusCode == System.Net.HttpStatusCode.OK);

            IEnumerable<Planet> planets = JsonConvert.DeserializeObject<IEnumerable<Planet>>(response.Content.ReadAsStringAsync().Result);
            Assert.That(planets.Count() > 0 && planets.Count() == NO_OF_PLANETS);
        }

        [Test]
        public void Planets_GetByName_ShouldPass()
        {
            string searchQuery = "Venus";
            HttpResponseMessage response = httpClient.GetAsync($"api/Planets/GetByName?name={ searchQuery }").Result;
            Assert.That(response != null && response.StatusCode == System.Net.HttpStatusCode.OK);

            Planet planet = JsonConvert.DeserializeObject<Planet>(response.Content.ReadAsStringAsync().Result);
            Assert.That(planet != null && string.Equals(planet.Name, searchQuery, StringComparison.InvariantCultureIgnoreCase));
        }

        [TearDown]
        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}
