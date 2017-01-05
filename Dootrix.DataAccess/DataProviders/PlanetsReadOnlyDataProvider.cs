using Dootrix.Planets.DataAccess.DataProviders.Interface;
using Dootrix.Planets.DataAccess.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Dootrix.Planets.DataAccess.DataProviders
{
    /// <summary>
    /// Data provider for querying the data store. Only exposes methods
    /// for retrieving planets from the data store.
    /// </summary>
    public class PlanetsReadOnlyDataProvider : IPlanetsReadOnlyDataProvider
    {
        private IDbDataContext dataContext;
        private PlanetFilter filter;
        protected IMongoCollection<Planet> planetCollection;

        private string collectionName = ConfigurationManager.AppSettings[Constants.COLLECTION_NAME];

        /// <summary>
        /// Default constructor with dependency on the datacontext and planet filter,
        /// which will both be injected by the container.
        /// </summary>
        /// <param name="dataContext">Data context for getting an active connection to the planets collections</param>
        /// <param name="filter">Filter for retrieving planet by name</param>
        public PlanetsReadOnlyDataProvider(IDbDataContext dataContext, PlanetFilter filter)
        {
            this.dataContext = dataContext;
            this.filter = filter;
            this.planetCollection = dataContext.GetCollection<Planet>(collectionName);
        }

        /// <summary>
        /// Get a planet by name
        /// </summary>
        /// <param name="name">Name of planet to retrieve</param>
        /// <returns>Matched planet</returns>
        public async Task<Planet> GetByNameAsync(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException($"Planet { nameof(name) } is required and cannot be null.");
            }

            filter.Name = name;
            FilterDefinition<Planet> filterDefinition = filter.ToFilterDefinition();
            IAsyncCursor<Planet> searchResult = await planetCollection.FindAsync(filterDefinition);

            return searchResult.FirstOrDefault();
        }

        /// <summary>
        /// Lists all planets in the data store
        /// </summary>
        /// <returns>Collection of planets</returns>
        public async Task<IEnumerable<Planet>> ListAllAsync()
        {
            BsonDocument searchFilter = new BsonDocument();
            IAsyncCursor<Planet> planetCursor = await planetCollection.FindAsync(searchFilter);

            return await planetCursor.ToListAsync();
        }
    }
}
