using Dootrix.Planets.Api.Controllers;
using Dootrix.Planets.DataAccess.DataProviders.Interface;
using Dootrix.Planets.DataAccess.Model;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Dootrix.Planets.Api.Tests.UnitTests
{
    [TestFixture]
    public class PlanetCreateTest
    {
        private IPlanetsDataProvider dataProvider;

        [SetUp]
        public void Configure()
        {
            dataProvider = BootstrapContainer.GetConfiguredContainer().Resolve<IPlanetsDataProvider>();
        }

        [Test]
        public void Planets_Create_ShouldPass()
        {
            //Ideally verify file actually exists, but in this case i'll pass...i know it does :)
            FileInfo fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"planets.json");
            string fileContents = File.ReadAllText(fileInfo.FullName);
            PlanetsCollection planets = JsonConvert.DeserializeObject<PlanetsCollection>(fileContents);

            //Returing a task (as previously mentioned) ensures that the execution
            //state of the task can be asserted on, and subsequently hooked into in
            //order to execute some post execution callback or operation
            Task createTask = dataProvider.CreateMultiple(planets.Planets);

            createTask.ContinueWith(t => 
            {
                Assert.That(t.IsCompleted && !t.IsFaulted && !t.IsCanceled);
            }).Wait();
        }

        [Test]
        public void Planets_GetByName_ShouldPass()
        {
            string searchItem = "Venus";
            Task<Planet> findOne = dataProvider.GetByNameAsync(searchItem);

            findOne.ContinueWith(t => 
            {
                Assert.That(t.IsCompleted && !t.IsFaulted && !t.IsCanceled);
                Planet p = t.Result;
                Assert.That(string.Equals(p.Name, searchItem, StringComparison.InvariantCultureIgnoreCase));
            }).Wait();
        }

        [TearDown]
        public void Dispose()
        {

        }
    }
}
