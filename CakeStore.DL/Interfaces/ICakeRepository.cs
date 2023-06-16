using CakeStore.Models.Models;

namespace CakeStore.DL.Interfaces
{
    public interface ICakeRepository
    {
        Task<IEnumerable<Cake>> GetAll();
        Task<Cake?> GetById(Guid id);
        Task Add(Cake cake);
        Task<bool> Update(Cake cake);
        Task<bool> Delete(Guid id);
    }
}
