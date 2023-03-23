namespace PokemonReviewApp2.Models
{
    public class Reviewer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //one-to-many
        public ICollection<Review> Reviews { get; set; }
    }
}
