using CatalogService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Repositories;

public interface ICatalogRepository
{
    Task<List<Product>> GetAll();
    Task<Product> GetById(int id);
    Task<Product> AddProduct(Product product);
    Task<bool> Delete(int id);
    Task<bool> Update(int id, Product product);

}