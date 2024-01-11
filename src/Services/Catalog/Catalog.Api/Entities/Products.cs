using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Api.Entities
{
    public class Products
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Summary { get; set; }
        public string? ImageFile { get; set; }

        public Decimal Price { get; set; }
    }
}
