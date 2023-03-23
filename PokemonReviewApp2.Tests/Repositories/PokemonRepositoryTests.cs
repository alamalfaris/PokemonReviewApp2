using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp2.Data;
using PokemonReviewApp2.Models;
using PokemonReviewApp2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp2.Tests.Repositories
{
    public class PokemonRepositoryTests
    {
        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.PokemonOwners.CountAsync() <= 0)
            {
                var pokemonOwners = new List<PokemonOwner>()
                {
                    new PokemonOwner()
                    {
                        Pokemon = new Pokemon()
                        {
                            Name = "Pikachu",
                            BirthDate = new DateTime(1903,1,1),
                            PokemonCategories = new List<PokemonCategory>()
                            {
                                new PokemonCategory { Category = new Category() { Name = "Electric"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Pikachu",Text = "Pickahu is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Pikachu", Text = "Pickachu is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Jack",
                            LastName = "London",
                            Gym = "Brocks Gym",
                            Country = new Country()
                            {
                                Name = "Kanto"
                            }
                        }
                    },
                    new PokemonOwner()
                    {
                        Pokemon = new Pokemon()
                        {
                            Name = "Squirtle",
                            BirthDate = new DateTime(1903,1,1),
                            PokemonCategories = new List<PokemonCategory>()
                            {
                                new PokemonCategory { Category = new Category() { Name = "Water"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Squirtle", Text = "squirtle is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title= "Squirtle",Text = "Squirtle is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title= "Squirtle", Text = "squirtle, squirtle, squirtle", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Harry",
                            LastName = "Potter",
                            Gym = "Mistys Gym",
                            Country = new Country()
                            {
                                Name = "Saffron City"
                            }
                        }
                    },
                    new PokemonOwner()
                    {
                        Pokemon = new Pokemon()
                        {
                            Name = "Venasuar",
                            BirthDate = new DateTime(1903,1,1),
                            PokemonCategories = new List<PokemonCategory>()
                            {
                                new PokemonCategory { Category = new Category() { Name = "Leaf"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Veasaur",Text = "Venasuar is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Veasaur",Text = "Venasuar is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Veasaur",Text = "Venasuar, Venasuar, Venasuar", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Ash",
                            LastName = "Ketchum",
                            Gym = "Ashs Gym",
                            Country = new Country()
                            {
                                Name = "Millet Town"
                            }
                        }
                    }
                };

                await databaseContext.PokemonOwners.AddRangeAsync(pokemonOwners);
                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        [Fact]
        public async void PokemonRepository_GetPokemon_ByName_ReturnPokemon()
        {
            // Arrange
            var name = "Pikachu";
            var dbContext = await GetDatabaseContext();
            var repository = new PokemonRepository(dbContext);

            // Act
            var result = repository.GetPokemon(name);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Pokemon>();
        }

        [Fact]
        public async void PokemonRepository_GetPokemon_ByName_ReturnNull()
        {
            // Arrange
            var name = "Odeng";
            var dbContext = await GetDatabaseContext();
            var repository = new PokemonRepository(dbContext);

            // Act
            var result = repository.GetPokemon(name);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void PokemonRepository_GetPokemon_ByPokeId_ReturnPokemon()
        {
            // Arrange
            var pokeId = 2;
            var dbContext = await GetDatabaseContext();
            var repository = new PokemonRepository(dbContext);

            // Act
            var result = repository.GetPokemon(pokeId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Pokemon>();
        }

        [Fact]
        public async void PokemonRepository_GetPokemon_ByPokeId_ReturnNull()
        {
            // Arrange
            var pokeId = 100;
            var dbContext = await GetDatabaseContext();
            var repository = new PokemonRepository(dbContext);

            // Act
            var result = repository.GetPokemon(pokeId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void PokemonRepository_GetPokemons_ReturnListPokemon()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new PokemonRepository(dbContext);

            // Act
            var result = repository.GetPokemons();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Pokemon>>();
        }

        [Fact]
        public async void PokemonRepository_PokemonExists_ReturnTrue()
        {
            // Arrange
            var pokeId = 2;
            var dbContext = await GetDatabaseContext();
            var repository = new PokemonRepository(dbContext);

            // Act
            var result = repository.PokemonExists(pokeId);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void PokemonRepository_PokemonExists_ReturnFalse()
        {
            // Arrange
            var pokeId = 100;
            var dbContext = await GetDatabaseContext();
            var repository = new PokemonRepository(dbContext);

            // Act
            var result = repository.PokemonExists(pokeId);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void PokemonRepository_GetPokemonRating_ReturnDecimalBetweenOneAndTen()
        {
            // Arrange
            var pokeId = 2;
            var dbContext = await GetDatabaseContext();
            var repository = new PokemonRepository(dbContext);

            // Act
            var result = repository.GetPokemonRating(pokeId);

            // Assert
            result.Should().NotBe(0);
            result.Should().BeInRange(1, 10);
        }

        [Fact]
        public async void PokemonRepository_GetPokemonRating_ReturnDecimalZero()
        {
            // Arrange
            var pokeId = 100;
            var dbContext = await GetDatabaseContext();
            var repository = new PokemonRepository(dbContext);

            // Act
            var result = repository.GetPokemonRating(pokeId);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public async void PokemonRepository_CreatePokemon_ReturnTrue()
        {
            // Arrange
            var ownerId = 2;
            var categoryId = 2;
            var pokemon = new Pokemon()
            {
                Name = "Raichu",
                BirthDate = DateTime.Now
            };
            var dbContext = await GetDatabaseContext();
            var repository = new PokemonRepository(dbContext);

            // Act
            var result = repository.CreatePokemon(ownerId, categoryId, pokemon);

            // Assert
            result.Should().BeTrue();
        }
    }
}
