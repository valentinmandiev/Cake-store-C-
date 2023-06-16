using CakeStore.DL.Interfaces;
using CakeStore.Models.Configuration;
using CakeStore.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CakeStore.DL.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IMongoCollection<User> _users;

        public IdentityRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            var client = new MongoClient(
                mongoConfig.CurrentValue.ConnectionString);
            var database =
                client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
            var collectionSettings = new MongoCollectionSettings
            {
                GuidRepresentation = GuidRepresentation.Standard
            };

            _users = database
                .GetCollection<User>(nameof(User), collectionSettings);
        }

        public async Task<User> FindByUserName(string userName)
        {
            var result = await _users.Find(x => x.Username == userName).FirstOrDefaultAsync();

            return result;
        }

        public async Task Add(User user)
        {
            await _users.InsertOneAsync(user);
        }
    }
}
