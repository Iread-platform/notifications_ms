using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{

    public interface IDeviceRepo
    {
        public Task<Device> AddDevice(Device device);
        public Task<List<Device>> GetAllDevices();
        public Task<List<Device>> GetUsersDevices(List<int> users);
        public bool DeviceExists(Device device);

    }
}