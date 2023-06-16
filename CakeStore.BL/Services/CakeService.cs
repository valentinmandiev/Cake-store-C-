using AutoMapper;
using CakeStore.BL.Interfaces;
using CakeStore.DL.Interfaces;
using CakeStore.Models.Models;
using CakeStore.Models.Request.Cake;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CakeStore.BL.Services
{
    public class CakeService : ICakeService
    {
        private readonly ICakeRepository _cakeRepository;
        private readonly IFactoryRepository _factoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CakeService> _logger;

        public CakeService(
            ICakeRepository cakeRepository,
            IMapper mapper,
            IFactoryRepository factoryRepository)
        {
            _cakeRepository = cakeRepository;
            _mapper = mapper;
            _factoryRepository = factoryRepository;
        }

        public CakeService(
            ICakeRepository cakeRepository,
            IMapper mapper,
            ILogger<CakeService> logger,
            IFactoryRepository factoryRepository)
        {
            _cakeRepository = cakeRepository;
            _mapper = mapper;
            _logger = logger;
            _factoryRepository = factoryRepository;
        }

        public async Task<IEnumerable<Cake>> GetAll()
        {
            try
            {
                return await _cakeRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(GetAll),
                    nameof(CakeService),
                    ex.Message);

                throw;
            }
        }

        public async Task<Cake?> GetById(Guid id)
        {
            try
            {
                var result = await _cakeRepository.GetById(id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(GetById),
                    nameof(CakeService),
                    ex.Message);

                throw;
            }
        }

        public async Task<Cake?> Add(AddCakeRequest addCakeRequest)
        {
            try
            {
                var factoryExists = await _factoryRepository.Exists(addCakeRequest.FactoryId);
                if (!factoryExists) 
                {
                    return null;   
                }

                var cake = _mapper.Map<Cake>(addCakeRequest);
                cake.Id = Guid.NewGuid();

                await _cakeRepository.Add(cake);

                return cake;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(Add),
                    nameof(CakeService),
                    ex.Message);

                throw;
            }
        }

        public async Task<bool> Update(UpdateCakeRequest updateCakeRequest)
        {
            try
            {
                var cake = _mapper.Map<Cake>(updateCakeRequest);

                return await _cakeRepository.Update(cake);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(Update),
                    nameof(CakeService),
                    ex.Message);

                throw;
            }
        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                return await _cakeRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(Delete),
                    nameof(CakeService),
                    ex.Message);

                throw;
            }
        }
    }
}
