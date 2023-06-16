using MongoDB.Bson.Serialization.Attributes;

namespace CakeStore.Models.Models
{
    public class User
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
