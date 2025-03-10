using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Data.Entities;
using YumBlazor.Repository.Abstractions;

namespace YumBlazor.Repository;

public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
{
    public async Task<Product> CreateAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);

        await dbContext.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var productToUpdate = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

        if (productToUpdate is null)
        {
            return product;
        }

        productToUpdate.Name = product.Name;

        dbContext.Update(productToUpdate);

        await dbContext.SaveChangesAsync();

        return productToUpdate;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
        {
            return false;
        }

        dbContext.Products.Remove(product);

        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Product> GetAsync(int id)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        return product ?? new Product();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await dbContext.Products.ToListAsync();
    }
}