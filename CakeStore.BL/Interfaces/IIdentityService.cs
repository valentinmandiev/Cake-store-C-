using CakeStore.Models.Requests.Identity;
using CakeStore.Models.Responses.Identity;

namespace CakeStore.BL.Interfaces
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);
        Task<GetUserByUserNameResponse> FindByUserName(string userName);
        Task Add(AddUserRequest addUserRequest);
    }
}