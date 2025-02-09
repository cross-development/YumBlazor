using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Data.Entities;
using YumBlazor.Repository.Abstractions;

namespace YumBlazor.Repository;

public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
{
    public async Task<Category> CreateAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);

        await dbContext.SaveChangesAsync();

        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        var categoryToUpdate = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

        if (categoryToUpdate is null)
        {
            return category;
        }

        categoryToUpdate.Name = category.Name;

        dbContext.Update(categoryToUpdate);

        await dbContext.SaveChangesAsync();

        return categoryToUpdate;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
        {
            return false;
        }

        dbContext.Categories.Remove(category);

        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Category> GetAsync(int id)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

        return category ?? new Category();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await dbContext.Categories.ToListAsync();
    }
}