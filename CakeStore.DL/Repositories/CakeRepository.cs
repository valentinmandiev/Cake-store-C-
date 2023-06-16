using CakeStore.DL.Interfaces;
using CakeStore.Models.Configuration;
using CakeStore.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CakeStore.DL.Repositories
{
    public class CakeRepository : ICakeRepository
    {
        private readonly IMongoCollection<Cake> _cakes;

        public CakeRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            var client = new MongoClient(
                mongoConfig.CurrentValue.ConnectionString);
            var database =
                client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
            var collectionSettings = new MongoCollectionSettings
            {
                GuidRepresentation = GuidRepresentation.Standard
            };

            _cakes = database
                .GetCollection<Cake>(nameof(Cake), collectionSettings);
        }

        public async Task<IEnumerable<Cake>> GetAll()
        {
            return await _cakes.Find(author => true).ToListAsync();
        }

        public async Task<Cake?> GetById(Guid id)
        {
            var result = await _cakes.Find(x => x.Id == id).FirstOrDefaultAsync();

            return result;
        }

        public async Task Add(Cake cake)
        {
            await _cakes.InsertOneAsync(cake);
        }

        public async Task<bool> Update(Cake cake)
        {
            var filter =
                Builders<Cake>.Filter.Eq(s => s.Id, cake.Id);

            var update = Builders<Cake>
                .Update.Set(s =>
                    s.Name, cake.Name);

            var result = await _cakes.UpdateOneAsync(filter, update);

            return result is not null;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _cakes.DeleteOneAsync(x => x.Id == id);

            return result is not null;
        }
    }
}
