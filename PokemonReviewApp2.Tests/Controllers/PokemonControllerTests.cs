using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp2.Controllers;
using PokemonReviewApp2.Dtos;
using PokemonReviewApp2.Interfaces;
using PokemonReviewApp2.Models;

namespace PokemonReviewApp2.Tests.Controllers
{
    public class PokemonControllerTests : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonControllerTests()
        {
            _pokemonRepository = A.Fake<IPokemonRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void PokemonController_GetPokemons_ReturnOK()
        {
            //Arrange
            var pokemons = A.Fake<ICollection<Pokemon>>();
            var pokemonList = A.Fake<List<PokemonDto>>();

            A.CallTo(() => _mapper.Map<List<PokemonDto>>(pokemons)).Returns(pokemonList);

            var controller = new PokemonController(_pokemonRepository, _mapper);

            //Act
            var result = controller.GetPokemons();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void PokemonController_GetPokemon_ReturnOK()
        {
            //Arrange
            int pokeId = 1;
            var pokemon = A.Fake<Pokemon>();
            var pokemonDto = A.Fake<PokemonDto>();

            A.CallTo(() => _pokemonRepository.PokemonExists(pokeId)).Returns(true);
            A.CallTo(() => _mapper.Map<PokemonDto>(pokemon)).Returns(pokemonDto);

            var controller = new PokemonController(_pokemonRepository, _mapper);

            //Act
            var result = controller.GetPokemon(pokeId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void PokemonController_GetPokemon_ReturnNotFound()
        {
            //Arrange
            int pokeId = 1;
            var pokemon = A.Fake<Pokemon>();
            var pokemonDto = A.Fake<PokemonDto>();

            A.CallTo(() => _pokemonRepository.PokemonExists(pokeId)).Returns(false);
            A.CallTo(() => _mapper.Map<PokemonDto>(pokemon)).Returns(pokemonDto);

            var controller = new PokemonController(_pokemonRepository, _mapper);

            //Act
            var result = controller.GetPokemon(pokeId);

            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public void PokemonController_GetPokemonRating_ReturnOK()
        {
            //Arrange
            int pokeId = 1;

            A.CallTo(() => _pokemonRepository.PokemonExists(pokeId)).Returns(true);

            var controller = new PokemonController(_pokemonRepository, _mapper);

            //Act
            var result = controller.GetPokemonRating(pokeId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void PokemonController_GetPokemonRating_ReturnNotFound()
        {
            //Arrange
            int pokeId = 1;

            A.CallTo(() => _pokemonRepository.PokemonExists(pokeId)).Returns(false);

            var controller = new PokemonController(_pokemonRepository, _mapper);

            //Act
            var result = controller.GetPokemonRating(pokeId);

            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public void PokemonController_CreatePokemon_ReturnOK()
        {
            // Arrange
            // Fake paramater CreatePokemon method
            int ownerId = 1;
            int catId = 1;
            var pokemonCreate = A.Fake<PokemonDto>();

            // Fake variable inside CreatePokemon method
            var pokemon = A.Fake<Pokemon>();
            var pokemonMap = A.Fake<Pokemon>();

            A.CallTo(() => _pokemonRepository.GetPokemonTrimToUpper(pokemonCreate)).Returns(null);
            A.CallTo(() => _mapper.Map<Pokemon>(pokemonCreate)).Returns(pokemonMap);
            A.CallTo(() => _pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap)).Returns(true);

            var controller = new PokemonController(_pokemonRepository, _mapper);

            // Act
            var result = controller.CreatePokemon(ownerId, catId, pokemonCreate);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
