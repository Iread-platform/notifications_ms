using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

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
            Device addedDevice = (await _context.Devices.AddAsync(device)).Entity;
            await _context.SaveChangesAsync();
            return addedDevice;
        }

        public async Task<List<Device>> GetAllDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        public async Task<List<Device>> GetUsersDevices(List<int> users)
        {
            return await _context.Devices.Where(device => users.Contains(device.UserId)).ToListAsync();
        }

        public bool DeviceExists(Device device)
        {
            return _context.Devices.Where(d => d.Token.Equals(device.Token)).Count() > 0;
        }
    }
}