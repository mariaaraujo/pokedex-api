using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pokedex_api.Models;
using pokedex_api.Services;

namespace pokedex_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonService _pokemonService;

        public PokemonController(PokemonService pokemonService) => _pokemonService = pokemonService;

        [HttpGet]
        public async Task<List<Pokemon>> Get() => await _pokemonService.GetAsync();

        
        [HttpGet("{_id:length(24)}")]
        public async Task<ActionResult<Pokemon>> Get(string _id)
        {
            var pokemon = await _pokemonService.GetAsync(_id);

            if (pokemon is null)
                return NotFound();

            return pokemon;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pokemon newPokemon)
        {
            await _pokemonService.CreateAsync(newPokemon);

            return CreatedAtAction(nameof(Get), new { id = newPokemon._id }, newPokemon);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string _id, Pokemon updatePokemon)
        {
            var pokemon = await _pokemonService.GetAsync(_id);

            if (pokemon is null)
                return NotFound();

            updatePokemon._id = pokemon._id;

            await _pokemonService.UpdateAsync(_id, updatePokemon);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string _id)
        {
            var pokemon = await _pokemonService.GetAsync(_id);

            if (pokemon is null)
                return NotFound();

            await _pokemonService.RemoveAsync(_id);

            return NoContent();
        }
    }
}
