using Dootrix.Planets.DataAccess.DataProviders.Interface;
using Dootrix.Planets.DataAccess.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dootrix.Planets.DataAccess.DataProviders
{
    /// <summary>
    /// Data provider for querying the data store. Exposes CRUD methods
    /// for manipulating planets in the datastore
    /// </summary>
    public class PlanetsDataProvider : PlanetsReadOnlyDataProvider, IPlanetsDataProvider
    {
        private PlanetFilter filter;
        private IDbDataContext dataContext;

        /// <summary>
        /// Default constructor with dependency on data context and planet filter (required by the base class)
        /// </summary>
        /// <param name="dataContext">Data context for getting an active connection to the planets collections</param>
        /// <param name="filter">Filter for retrieving planet by name</param>
        public PlanetsDataProvider(IDbDataContext context, PlanetFilter filter) 
            : base(context, filter)
        {
            dataContext = context;          
        }

        /// <summary>
        /// Inserts a new planet in the datastore collection
        /// </summary>
        /// <param name="entity">Planet data to persist</param>
        /// <returns>Task (could have been a void, but tasks offer advantages 
        /// such as state monitoring, fault notification etc)</returns>
        public async Task Create(Planet entity)
        {
            await planetCollection.InsertOneAsync(entity, new InsertOneOptions
            {
                BypassDocumentValidation = false
            });
        }

        /// <summary>
        /// Inserts multiple planets in the datastore collection
        /// </summary>
        /// <param name="entities">Collection of planets to persist</param>
        /// <returns>Continuation task</returns>
        public async Task CreateMultiple(IEnumerable<Planet> entities)
        {
            await planetCollection.InsertManyAsync(entities, new InsertManyOptions
            {
                BypassDocumentValidation = false,
                IsOrdered = true
            });
        }
    }
}
