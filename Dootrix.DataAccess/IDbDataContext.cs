using MongoDB.Driver;
using System.Configuration;
using System;

namespace Dootrix.Planets.DataAccess
{
    public interface IDbDataContext
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }

    /// <summary>
    /// Exposes an active connection to the database. Lifetime managed by 
    /// the IoC container, registered as a singleton so that it can be 
    /// reused across the lifetime of the application (rather than instantiating
    /// connections per request)
    /// </summary>
    public class DbDataContext : IDbDataContext
    {
        private string connectionString;
        private string databaseName;

        /// <summary>
        /// Default constructor accepting two paramters that will be injected
        /// by the IoC container with default values (this approach has been taken)
        /// because these details are not expected to change. Alternatively, injection
        /// of these values can be switched off and just read directly from a configuration/settings file
        /// </summary>
        /// <param name="connectionString">connection string to database</param>
        /// <param name="databaseName">database name</param>
        public DbDataContext(string connectionString, string databaseName)
        {
            this.connectionString = connectionString ?? ConfigurationManager.AppSettings[Constants.CONNECTION_STRING];
            this.databaseName = databaseName ?? ConfigurationManager.AppSettings[Constants.DATABASE_NAME];
        }

        /// <summary>
        /// Retrieves the collection to query
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="collectionName">Name of collection</param>
        /// <returns>IMongoCollection (connection to retrieved collection)</returns>
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return GetDatabase().GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Returns an instance of the database connection
        /// </summary>
        /// <returns>IMongoDatabase instance</returns>
        private IMongoDatabase GetDatabase()
        {
            if(string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException($"Please provide a valid value for { nameof(connectionString) } .");
            }

            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentNullException($"Please provide a valid value for { nameof(databaseName) } .");
            }

            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            IMongoClient client = new MongoClient(settings);

            return client.GetDatabase(databaseName);
        }
    }
}
