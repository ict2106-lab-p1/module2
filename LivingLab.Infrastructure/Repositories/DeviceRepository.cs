using LivingLab.Core.Entities;
using LivingLab.Core.Entities.DTO.Device;
using LivingLab.Core.Interfaces.Repositories;
using LivingLab.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace LivingLab.Infrastructure.Repositories;

public class DeviceRepository : Repository<Device>, IDeviceRepository
{
    private readonly ApplicationDbContext _context;

    public DeviceRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<ViewDeviceTypeDTO>> GetViewDeviceType()
    {
        var deviceGroup = await _context.Devices.GroupBy(t => t.Type).Select(t => new { Key = t.Key, Count = t.Count() }).ToListAsync();
        List<ViewDeviceTypeDTO> deviceTypeDtos = new List<ViewDeviceTypeDTO>();
        foreach (var group in deviceGroup)
        {
            ViewDeviceTypeDTO deviceTypeDto = new ViewDeviceTypeDTO();
            deviceTypeDto.Type = group.Key;
            deviceTypeDto.Quantity = group.Count;
            deviceTypeDtos.Add(deviceTypeDto);
        }
        return deviceTypeDtos;
    }

    public async Task<List<Device>> GetAllDevicesByType(string deviceType)
    {
        List<Device> deviceList = await _context.Devices.Where(t => deviceType.Contains(t.Type)).ToListAsync();
        return deviceList;
    }

    public async Task<Device> GetDeviceDetails(int id)
    {
        // retrieve device db together with device type details using include to join entities
        Device device = (await _context.Devices.SingleOrDefaultAsync(d => d.Id == id))!;
        return device;
    }
    public async Task<Device> GetLastRow()
    {
        var device = await _context.Devices.OrderByDescending(d => d.Id).FirstOrDefaultAsync();
        return device;
    }

    public async Task<Device> AddDevice(Device addedDevice)
    {
        addedDevice.LabId = 1;
        // add to database
        addedDevice.LastUpdated = DateTime.Today;
        _context.Devices.Add(addedDevice);
        await _context.SaveChangesAsync();
        return addedDevice;
    }

    public async Task<Device> EditDeviceDetails(Device editedDevice)
    {
        // retrieve device db together with device type details using include to join entities
        Device currentDevice = (await _context.Devices.SingleOrDefaultAsync(d => d.Id == editedDevice.Id))!;
        currentDevice.SerialNo = editedDevice.SerialNo;
        currentDevice.Name = editedDevice.Name;
        currentDevice.Type = editedDevice.Type;
        currentDevice.Description = editedDevice.Description;
        currentDevice.Status = editedDevice.Status;
        currentDevice.Threshold = editedDevice.Threshold;
        currentDevice.LastUpdated = DateTime.Today;
        await _context.SaveChangesAsync();

        return editedDevice;
    }

    public async Task<Device> DeleteDevice(Device deleteDevice)
    {
        // retrieve device db together with device type details using include to join entities
        Device currentDevice = (await _context.Devices.SingleOrDefaultAsync(d => d.Id == deleteDevice.Id))!;
        _context.Devices.Remove(currentDevice);
        await _context.SaveChangesAsync();
        Console.WriteLine("Delete Succ");

        return deleteDevice;
    }
}