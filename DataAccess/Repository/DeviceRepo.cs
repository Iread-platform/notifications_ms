using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            if (DeviceExists(device))
            {
                return null;
            }
            return (await _context.Devices.AddAsync(device)).Entity;
        }


        public bool DeviceExists(Device device)
        {
            return _context.Devices.Where(d => d.Token.Equals(device.Token)).Count() > 0;
        }
    }
}