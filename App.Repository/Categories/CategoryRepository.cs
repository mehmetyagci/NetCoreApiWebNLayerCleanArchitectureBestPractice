using Microsoft.EntityFrameworkCore;

namespace App.Repository.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    public Task<Category?> GetCategoryWithProductsAsync(int id)
    {
        return context.Categories
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<Category> GetCategoryWithProducts()
    {
        return context.Categories
            .Include(x => x.Products);
    }
}