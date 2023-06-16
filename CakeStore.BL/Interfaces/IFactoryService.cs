using CakeStore.Models.Models;
using CakeStore.Models.Request.Factory;

namespace CakeStore.BL.Interfaces
{
    public interface IFactoryService
    {
        Task<IEnumerable<Factory>> GetAll();
        Task<Factory?> GetById(Guid id);
        Task<Factory?> Add(AddFactoryRequest factory);
        Task<bool> Update(UpdateFactoryRequest factory);
        Task<bool> Delete(Guid id);
    }
}
