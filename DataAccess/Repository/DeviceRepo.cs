using System.Threading.Tasks;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{
    public class DeviceRepo : IDeviceRepo
    {
        private readonly AppDbContext _context;
        public DeviceRepo(AppDbContext context)
        {
            _context = context;

        }

        public async Task<Device> AddDevice(Device device)
        {
            return (await _context.Devices.AddAsync(device)).Entity;
        }


        public Task<bool> DeviceExists(Device device)
        {
            return new Task<bool>(() => true);
        }
    }
}