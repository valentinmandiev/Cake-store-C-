using CakeStore.Models.Models;

namespace CakeStore.DL.Interfaces
{
    public interface IIdentityRepository
    {
        Task<User> FindByUserName(string userName);
        Task Add(User user);
    }
}
