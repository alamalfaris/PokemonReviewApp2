using PokemonReviewApp2.Data;
using PokemonReviewApp2.Interfaces;
using PokemonReviewApp2.Models;

namespace PokemonReviewApp2.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Owner> GetOwnersOfAPokemon(int pokeId)
        {
            return _context.PokemonOwners
                .Where(po => po.Pokemon.Id == pokeId)
                .Select(po => po.Owner)
                .ToList();
        }

        public ICollection<Pokemon> GetPokemonsByOwner(int ownerId)
        {
            return _context.PokemonOwners
                .Where(po => po.Owner.Id == ownerId)
                .Select(po => po.Pokemon)
                .ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }
    }
}
