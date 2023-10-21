namespace pokedex_api.Models
{
    public class PokedexDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PokedexCollectionName { get; set; } = null!;
    }
}
