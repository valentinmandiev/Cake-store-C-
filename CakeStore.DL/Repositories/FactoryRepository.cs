using CakeStore.DL.Interfaces;
using CakeStore.Models.Configuration;
using CakeStore.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CakeStore.DL.Repositories
{
    public class FactoryRepository : IFactoryRepository
    {
        private readonly IMongoCollection<Factory> _factories;

        public FactoryRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            var client = new MongoClient(
                mongoConfig.CurrentValue.ConnectionString);
            var database =
                client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
            var collectionSettings = new MongoCollectionSettings
            {
                GuidRepresentation = GuidRepresentation.Standard
            };

            _factories = database
                .GetCollection<Factory>(nameof(Factory), collectionSettings);
        }

        public async Task<bool> Exists(Guid id)
        {
            var result = await _factories.Find(x => x.Id == id).ToListAsync();

            if (result.Count == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Factory>> GetAll()
        {
            var result = await _factories.Find(author => true).ToListAsync();

            return result;
        }

        public async Task<Factory?> GetById(Guid id)
        {
            var result = await _factories.Find(x => x.Id == id).FirstOrDefaultAsync();

            return result;
        }

        public async Task Add(Factory factory)
        {
            await _factories.InsertOneAsync(factory);
        }

        public async Task<bool> Update(Factory factory)
        {
            var filter =
                Builders<Factory>.Filter.Eq(s => s.Id, factory.Id);

            var update = Builders<Factory>
                .Update.Set(s =>
                    s.Name, factory.Name);

            var result = await _factories.UpdateOneAsync(filter, update);

            return result is not null;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _factories.DeleteOneAsync(x => x.Id == id);

            return result is not null;
        }
    }
}
