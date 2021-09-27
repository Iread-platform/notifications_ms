using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

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

    }
}