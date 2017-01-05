using Dootrix.Planets.DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dootrix.Planets.DataAccess.DataProviders.Interface
{
    public interface IPlanetsReadOnlyDataProvider
    {
        Task<IEnumerable<Planet>> ListAllAsync();
        Task<Planet> GetByNameAsync(string name);
    }

    public interface IPlanetsDataProvider : IPlanetsReadOnlyDataProvider
    {
        Task Create(Planet planet);
        Task CreateMultiple(IEnumerable<Planet> planets);
    }
}
