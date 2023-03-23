using PokemonReviewApp2.Models;

namespace PokemonReviewApp2.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsOfAPokemon(int pokeId);
        bool ReviewExists(int id);
    }
}
