using System.Threading.Tasks;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{

    public interface IDeviceRepo
    {
        public Task<Device> AddDevice(Device device);
        public Task<bool> DeviceExists(Device device);

    }
}