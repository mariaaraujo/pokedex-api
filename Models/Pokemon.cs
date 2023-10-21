using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace pokedex_api.Models
{
    public class Pokemon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        public string name { get; set; } = null!;
        public string description { get; set; } = null!;
        public string[] type { get; set; } = null!;
    }
}
