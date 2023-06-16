using MongoDB.Bson.Serialization.Attributes;

namespace CakeStore.Models.Models
{
    public class Factory
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EIK { get; set; }
        public string Address { get; set; }
    }
}
