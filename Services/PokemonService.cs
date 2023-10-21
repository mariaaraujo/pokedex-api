using Microsoft.Extensions.Options;
using MongoDB.Driver;
using pokedex_api.Models;

namespace pokedex_api.Services
{
    public class PokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;

        public PokemonService(IOptions<PokedexDatabaseSettings> pokedexServices)
        {
            var mongoClient = new MongoClient(pokedexServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(pokedexServices.Value.DatabaseName);

            _pokemonCollection = mongoDatabase.GetCollection<Pokemon>(pokedexServices.Value.PokedexCollectionName);
        }
        public async Task<List<Pokemon>> GetAsync() =>
            await _pokemonCollection.Find(_ => true).ToListAsync();

        public async Task<Pokemon?> GetAsync(string id) =>
        await _pokemonCollection.Find(x => x._id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Pokemon newPokemon) =>
            await _pokemonCollection.InsertOneAsync(newPokemon);

        public async Task UpdateAsync(string id, Pokemon updatePokemon) =>
            await _pokemonCollection.ReplaceOneAsync(x => x._id == id, updatePokemon);

        public async Task RemoveAsync(string id) =>
            await _pokemonCollection.DeleteOneAsync(x => x._id == id);
    }
}
