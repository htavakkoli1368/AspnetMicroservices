using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public interface IProductRepositories
    {
        Task<IEnumerable<Products>> GetProducts();
        Task<Products> GetProduct(string id);
        Task<IEnumerable<Products>> GetProductByName(string name);
        Task<IEnumerable<Products>> GetProductByCategory(string categoryName);
        Task CreateProduct(Products product);
        Task<bool> UpdateProduct(Products product);
        Task<bool> DeleteProduct(string id);

    }
}
