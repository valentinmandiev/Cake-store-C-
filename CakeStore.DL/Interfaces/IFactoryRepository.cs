using CakeStore.Models.Models;

namespace CakeStore.DL.Interfaces
{
    public interface IFactoryRepository
    {
        Task<bool> Exists(Guid id);
        Task<IEnumerable<Factory>> GetAll();
        Task<Factory?> GetById(Guid id);
        Task Add(Factory factory);
        Task<bool> Update(Factory factory);
        Task<bool> Delete(Guid id);
    }
}
