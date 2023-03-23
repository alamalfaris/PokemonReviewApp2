using PokemonReviewApp2.Dtos;
using PokemonReviewApp2.Models;

namespace PokemonReviewApp2.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int pokeId);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRating(int pokeId);
        bool PokemonExists(int pokeId);
        bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate);
    }
}
