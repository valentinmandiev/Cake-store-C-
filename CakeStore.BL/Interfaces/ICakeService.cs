using CakeStore.Models.Models;
using CakeStore.Models.Request.Cake;

namespace CakeStore.BL.Interfaces
{
    public interface ICakeService
    {
        Task<IEnumerable<Cake>> GetAll();
        Task<Cake?> GetById(Guid id);
        Task<Cake?> Add(AddCakeRequest cake);
        Task<bool> Update(UpdateCakeRequest cake);
        Task<bool> Delete(Guid id);
    }
}
