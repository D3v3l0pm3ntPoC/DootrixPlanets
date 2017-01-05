using Dootrix.Planets.DataAccess.DataProviders.Interface;
using Dootrix.Planets.DataAccess.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dootrix.Planets.Api.Controllers
{
    public class PlanetsController : ApiController
    {
        private IPlanetsReadOnlyDataProvider readOnlyDataProvider;

        /// <summary>
        /// Default constructor with dependency on the planets (readonly) data provider.
        /// Will be injected by the unity ioc
        /// </summary>
        /// <param name="readOnlyDataProvider">Planets readonly data provider (for retrieving planets)</param>
        public PlanetsController(IPlanetsReadOnlyDataProvider readOnlyDataProvider)
        {
            this.readOnlyDataProvider = readOnlyDataProvider;
        }

        /// <summary>
        /// Returns all planets in the data store.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> ListAll()
        {
            IEnumerable<Planet> planets = await readOnlyDataProvider.ListAllAsync();

            if(planets == null || planets.Count() == 0)
            {
                return ResponseMessage(Request.CreateResponse(
                        HttpStatusCode.NotFound, "No planets found in the data store."));
            }

            return Ok(planets);
        }

        /// <summary>
        /// Gets a planet by name from the datastore
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return ResponseMessage(Request.CreateResponse(
                        HttpStatusCode.BadRequest, $"{ nameof(name) } is required and cannot be null."));
            }

            Planet match = await readOnlyDataProvider.GetByNameAsync(name);

            if(match == default(Planet))
            {
                return ResponseMessage(Request.CreateResponse(
                        HttpStatusCode.NotFound, $"No matching planet found with name: { name }"));
            }

            return Ok(match);
        }
    }
}