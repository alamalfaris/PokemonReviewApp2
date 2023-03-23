using PokemonReviewApp2.Data;
using PokemonReviewApp2.Interfaces;
using PokemonReviewApp2.Models;

namespace PokemonReviewApp2.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories
                .Where(c => c.Id == categoryId)
                .FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories
                .Where(pc => pc.CategoryId == categoryId)
                .Select(pc => pc.Pokemon)
                .ToList();
        }
    }
}
