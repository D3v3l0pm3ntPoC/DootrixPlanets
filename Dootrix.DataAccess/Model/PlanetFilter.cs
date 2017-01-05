using MongoDB.Driver;

namespace Dootrix.Planets.DataAccess.Model
{
    public class PlanetFilter
    {
        public string Name { get; set; }

        public FilterDefinition<Planet> ToFilterDefinition()
        {
            FilterDefinition<Planet> filterDefinition = Builders<Planet>.Filter.Empty; 

            if (!string.IsNullOrWhiteSpace(Name))
            {
                filterDefinition &= Builders<Planet>.Filter.Eq(planet => planet.Name, Name);
            }

            return filterDefinition;
        }
    }
}
