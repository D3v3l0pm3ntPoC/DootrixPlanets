using Dootrix.Planets.DataAccess.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dootrix.UI.Controllers
{
    public class PlanetController : Controller
    {
        private string planetApi = ConfigurationManager.AppSettings["PlanetsAPI"];
        private HttpClient httpClient;

        public PlanetController()
        {
            if(string.IsNullOrWhiteSpace(planetApi))
            {
                throw new ArgumentNullException("The Planet API url is required, please correct this.");
            }

            httpClient = new HttpClient { BaseAddress = new Uri(planetApi) };
        }

        /// <summary>
        /// Retrieve a planet from the cloud REST api
        /// </summary>
        /// <param name="name">Name of planet to retrieve</param>
        /// <returns>Planet (if found)</returns>
        [HttpPost]
        public async Task<ActionResult> FindPlanetByName(FormCollection formDetails)
        {
            string searchQuery = formDetails["name"];
            HttpResponseMessage response = await httpClient.GetAsync($"api/Planets/GetByName?name={ searchQuery }");
            Planet match = JsonConvert.DeserializeObject<Planet>(response.Content.ReadAsStringAsync().Result);

            TempData["planet"] = match;
            return RedirectToAction(nameof(LoadPlanetDetails));
        }

        /// <summary>
        /// Get all planets in the cloud based data store (using the cloud api)
        /// </summary>
        /// <returns>Collection of planets (if any)</returns>
        [HttpGet]
        public async Task<ActionResult> ListAllPlanets()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"api/Planets/ListAllAsync");
            IEnumerable<Planet> collection = JsonConvert.DeserializeObject<IEnumerable<Planet>>
                                                            (response.Content.ReadAsStringAsync().Result);

            return Json(collection, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Load the details view for the planet object
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LoadPlanetDetails()
        {
            return View((Planet)TempData["planet"]);
        }

        /// <summary>
        /// Free up all resources
        /// </summary>
        public new void Dispose()
        {
            base.Dispose();
            httpClient?.Dispose();
        }
    }
}
