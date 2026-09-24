using MongoDB.Driver;
using CatalogService.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Repositories;

public class CatalogRepository : ICatalogRepository
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

    public async Task<List<Product>> GetAll()
    {
        var filter = Builders<Product>.Filter.Empty;
        List<Product> products = await _collection.Find(filter).ToListAsync();
        return products;
    }

    public async Task<Product> GetById(int id)
    {
        var filter = Builders<Product>.Filter.Eq(product => product.Id, id);
        Product foundProduct = await _collection.Find(filter).FirstOrDefaultAsync();

        return foundProduct;
    }

    public async Task<Product> AddProduct(Product product)
    {
        await _collection.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> Delete(int id)
    {
        var filter = Builders<Product>.Filter.Eq(product => product.Id, id);
        DeleteResult result = await _collection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }

    public async Task<bool> Update(int id, Product product)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        ReplaceOneResult result = await _collection.ReplaceOneAsync(filter, product);

        return result.ModifiedCount > 0;
    }
    
}