using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Dootrix.Planets.DataAccess.Model
{

    public class Profile
    {
        public string Diameter { get; set; }
        public string Mass { get; set; }
        public string Moons { get; set; }
        public string OrbitDistance { get; set; }
        public string OrbitPeriod { get; set; }
        public string SurfaceTemperature { get; set; }
        public int Ring { get; set; }
    }

    public class Planet
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Name { get; set; }
        public List<string> ImageLinks { get; set; }
        public string Description { get; set; }
        public Profile Profile { get; set; }
    }

    public class PlanetsCollection
    {
        public List<Planet> Planets { get; set; }
    }
}
