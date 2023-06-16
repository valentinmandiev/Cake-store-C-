using AutoMapper;
using CakeStore.BL.Interfaces;
using CakeStore.DL.Interfaces;
using CakeStore.Models.Models;
using CakeStore.Models.Request.Factory;
using Microsoft.Extensions.Logging;

namespace CakeStore.BL.Services
{
    public class FactoryService : IFactoryService
    {
        private readonly IFactoryRepository _factoryRepository;
        private readonly ILogger<FactoryService> _logger;
        private readonly IMapper _mapper;

        public FactoryService(
            IFactoryRepository cakeRepository,
            ILogger<FactoryService> logger,
            IMapper mapper)
        {
            _factoryRepository = cakeRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Factory>> GetAll()
        {
            return await _factoryRepository.GetAll();
        }

        public async Task<Factory?> GetById(Guid id)
        {
            try
            {
                var result = await _factoryRepository.GetById(id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(GetById),
                    nameof(FactoryService),
                    ex.Message);

                throw;
            }
        }

        public async Task<Factory?> Add(AddFactoryRequest addFactoryRequest)
        {
            try
            {
                var factory = _mapper.Map<Factory>(addFactoryRequest);

                await _factoryRepository.Add(factory);

                return factory;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(Add),
                    nameof(FactoryService),
                    ex.Message);

                throw;
            }
        }

        public async Task<bool> Update(UpdateFactoryRequest updateFactoryRequest)
        {
            try
            {
                var factory = _mapper.Map<Factory>(updateFactoryRequest);

                return await _factoryRepository.Update(factory);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(Update),
                    nameof(FactoryService),
                    ex.Message);

                throw;
            }
        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                return await _factoryRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(Delete),
                    nameof(FactoryService),
                    ex.Message);

                throw;
            }
        }
    }
}
