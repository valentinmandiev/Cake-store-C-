using MongoDB.Bson.Serialization.Attributes;

namespace CakeStore.Models.Models
{
    public class Cake
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid FactoryId { get; set; }
    }
}
