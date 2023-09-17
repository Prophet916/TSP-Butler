using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Butler.config
{
    public class DataBaseAccess
    {
        private IMongoCollection<BlackPyramidLocation> _blackPyramidLocations;

        public DataBaseAccess(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _blackPyramidLocations = database.GetCollection<BlackPyramidLocation>("blackpyramidlocations");
        }

        public void InsertBlackPyramidLocation(BlackPyramidLocation location)
        {
            Console.WriteLine("DBAccess class insert locations started");
            // Insert the location into MongoDB collection
            _blackPyramidLocations.InsertOne(location);
        }

        public List<BlackPyramidLocation> GetAllBlackPyramidLocations()
        {
            Console.WriteLine("DBAccess class retrieve all locations started");
            // Retrieve all Black Pyramid locations from MongoDB collection
            return _blackPyramidLocations.Find(_ => true).ToList();
        }

        public bool TestConnection()
        {
            try
            {
                // This will attempt to execute a basic operation, ensuring the connection works
                _blackPyramidLocations.CountDocuments(_ => true);
                return true; // Connection succeeded
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                Console.WriteLine($"MongoDB connection failed: {ex.Message}");
                return false; // Connection failed
            }
        }
    }

    public class BlackPyramidLocation
    {
        public ObjectId Id { get; set; }
        public string SolarSystem { get; set; }
        public string Planet { get; set; }
    }
}
