using YumBlazor.Data;
using YumBlazor.Data.Entities;
using YumBlazor.Repository.Abstractions;

namespace YumBlazor.Repository;

public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
{
    public Category Create(Category category)
    {
        dbContext.Categories.Add(category);
        dbContext.SaveChanges();

        return category;
    }

    public Category Update(Category category)
    {
        var categoryToUpdate = dbContext.Categories.FirstOrDefault(c => c.Id == category.Id);

        if (categoryToUpdate is null)
        {
            return category;
        }

        categoryToUpdate.Name = category.Name;
        dbContext.SaveChanges();

        return categoryToUpdate;
    }

    public bool Delete(int id)
    {
        var category = dbContext.Categories.FirstOrDefault(c => c.Id == id);

        if (category is null)
        {
            return false;
        }

        dbContext.Categories.Remove(category);

        return dbContext.SaveChanges() > 0;
    }

    public Category Get(int id)
    {
        var category = dbContext.Categories.FirstOrDefault(c => c.Id == id);

        return category ?? new Category();
    }

    public IEnumerable<Category> GetAll()
    {
        return dbContext.Categories.ToList();
    }
}