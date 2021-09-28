using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Repository;
using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.Web.Service
{

    public class DeviceService
    {
        private readonly IPublicRepo _publicRepo;

        public DeviceService(IPublicRepo publicRepo)
        {
            _publicRepo = publicRepo;
        }

        public async Task<Device> AddDevice(Device device)
        {
            return await _publicRepo.DeviceRepo.AddDevice(device);
        }

        public async Task<List<Device>> GetAllDevices()
        {
            return await _publicRepo.DeviceRepo.GetAllDevices();
        }
        public async Task<Device> GetDevice(int id)
        {
            return await _publicRepo.DeviceRepo.GetDevice(id);
        }
    }
}