using MongoDB.Driver;
using CatalogService.Models;
namespace CatalogService.Repositories;

public class CatalogRepository
{
    private IMongoCollection<Product> _collection;

    public CatalogRepository(IConfiguration config)
    {
        string? connectionString = config["Mongo:ConnectionString"];
        string databaseName = config["Mongo:DatabaseName"];

        MongoClient mongoClient = new MongoClient(connectionString);
        IMongoDatabase? mongoDatabase = mongoClient.GetDatabase(databaseName);
        _collection = mongoDatabase.GetCollection<Product>("Products");
    }
}