using AutoMapper;
using CakeStore.Models.Models;
using CakeStore.Models.Request.Cake;
using CakeStore.Models.Request.Factory;
using CakeStore.Models.Requests.Cake;
using CakeStore.Models.Requests.Factory;
using CakeStore.Models.Requests.Identity;
using CakeStore.Models.Responses.Identity;

namespace CakeStore.Web.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Requests
            CreateMap<AddCakeRequest, Cake>();
            CreateMap<UpdateCakeRequest, Cake>();

            CreateMap<AddFactoryRequest, Factory>();
            CreateMap<UpdateFactoryRequest, Factory>();

            CreateMap<AddUserRequest, User>();
            #endregion

            #region Responses
            CreateMap<User, GetUserByUserNameResponse>();

            CreateMap<Cake, CakeResponse>();

            CreateMap<Factory, FactoryResponse>();
            #endregion
        }
    }
}
