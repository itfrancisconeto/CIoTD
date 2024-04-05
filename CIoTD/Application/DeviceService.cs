using CIoTD.Infrastructure;
using CIoTD.Domain;

namespace CIoTD.Application
{
    public class DeviceService
    {
        private readonly IRepository<Devices> _repository;

        public DeviceService(IRepository<Devices> repository)
        {
            _repository = repository;
        }

        public Task<List<Devices>> GetAll() => _repository.GetAll();
        public Task<Devices> GetById(string id) => _repository.GetById(id);
        public Task<Devices> GetByIdCommand(string id, string command) => _repository.GetByIdCommand(id, command);
        public Task<Devices> Create(Devices device) => _repository.Create(device);
        public Task<Devices> Update(string id, Devices device) => _repository.Update(id, device);
        public Task<Devices> Delete(string id) => _repository.Delete(id);
    }
}
