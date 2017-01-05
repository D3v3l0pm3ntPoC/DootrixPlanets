using Dootrix.Planets.Api.Controllers;
using Dootrix.Planets.DataAccess.DataProviders;
using Dootrix.Planets.DataAccess.DataProviders.Interface;
using Dootrix.Planets.DataAccess.Model;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Dootrix.Planets.Api.Tests.UnitTests
{
    [TestFixture]
    public class PlanetsControllerTest
    {
        private Mock<IPlanetsReadOnlyDataProvider> readOnlyDataProvider;

        [SetUp]
        public void Setup()
        {
            readOnlyDataProvider = new Mock<IPlanetsReadOnlyDataProvider>();

            readOnlyDataProvider.Setup(dataProvider => dataProvider.ListAllAsync())
                                .Returns(Task.FromResult(ListAll()));

            readOnlyDataProvider.Setup(dataProvider => dataProvider.GetByNameAsync(It.IsAny<string>()))
                                .Returns(Task.FromResult(ListAll().FirstOrDefault()));
        }

        [Test]
        public void Planets_ListAll_ShouldPass()
        {
            PlanetsController planetsController = new PlanetsController(readOnlyDataProvider.Object);
            OkNegotiatedContentResult<IEnumerable<Planet>> response = 
                    (OkNegotiatedContentResult<IEnumerable<Planet>>)planetsController.ListAll().Result;

            Assert.That(response.Content.Count() > 0 && response.Content.Count() == ListAll().Count());
            readOnlyDataProvider.Verify(dataProvider => dataProvider.ListAllAsync(), Times.Once);
        }

        [Test]
        public void Planets_GetByName_ShouldPass()
        {
            string expectedResult = "Mercury";
            PlanetsController planetsController = new PlanetsController(readOnlyDataProvider.Object);
            OkNegotiatedContentResult<Planet> response = (OkNegotiatedContentResult<Planet>)
                                                                planetsController.GetByName("random planet").Result;

            Assert.That(response.Content != null && 
                    string.Equals(expectedResult, response.Content.Name, StringComparison.InvariantCultureIgnoreCase));
            readOnlyDataProvider.Verify(dataProvider => dataProvider.GetByNameAsync(It.IsAny<string>()), Times.Once);
        }

        private IEnumerable<Planet> ListAll()
        {

            FileInfo fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"planets.json");
            string fileContents = File.ReadAllText(fileInfo.FullName);
            PlanetsCollection planets = JsonConvert.DeserializeObject<PlanetsCollection>(fileContents);

            return planets.Planets.Take(3);
        }
    }
}
