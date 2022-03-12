using LivingLab.Core.Entities;
using LivingLab.Core.Entities.Identity;
using LivingLab.Core.Interfaces.Repositories;
using LivingLab.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace LivingLab.Infrastructure.Repositories;

public class EnergyUsageRepository : Repository<EnergyUsageLog>, IEnergyUsageRepository
{
    private readonly ApplicationDbContext _context;

    public EnergyUsageRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task BulkInsertAsync(ICollection<EnergyUsageLog> logs)
    {
        foreach (var log in logs)
        {
            // TODO: Change to repo method
            log.Device = await _context.Devices.FirstOrDefaultAsync(d => d.DeviceSerialNumber == log.Device.DeviceSerialNumber);
            log.Lab = await _context.Labs.FirstOrDefaultAsync(l => l.Id == 1);
            await _context.AddAsync(log);
        }

        await _context.SaveChangesAsync();
    }

    public async Task BulkInsertAsyncByUser(ICollection<EnergyUsageLog> logs, ApplicationUser loggedBy)
    {
        logs.ToList().ForEach(log => log.LoggedBy = loggedBy);
        await BulkInsertAsync(logs);
    }

    public async Task<List<EnergyUsageLog>> GetUsageByDeviceId(int id)
    {
        var logsForDevice = await IncludeReferences(
                _context.EnergyUsageLogs
                .Where(log => log.Device!.Id == id)
            )
            .ToListAsync();
        return logsForDevice;
    }

    public async Task<List<EnergyUsageLog>> GetUsageByDeviceType(string deviceType)
    {
        var logsForType = await IncludeReferences(
                _context.EnergyUsageLogs
                .Where(log => log.Device!.Type == deviceType)
            )
            .ToListAsync();
        return logsForType;
    }

    public async Task<List<EnergyUsageLog>> GetUsageByLabId(int id)
    {
        var logsForLab = await IncludeReferences(
                _context.EnergyUsageLogs
                .Where(log => log.Lab!.Id == id)
            )
            .ToListAsync();
        return logsForLab;
    }

    public Task<List<EnergyUsageLog>> GetUsageByUser(ApplicationUser? user)
    {
        if(user == null) {
            return IncludeReferences(
                    _context.EnergyUsageLogs
                    .Where(log => log.LoggedBy != null)
                )
                .ToListAsync();
        } else {
            return IncludeReferences(
                    _context.EnergyUsageLogs
                    .Where(log => log.LoggedBy != null && log.LoggedBy.Equals(user))
                )
                .ToListAsync();
        }
    }
    protected override IQueryable<EnergyUsageLog> IncludeReferences(IQueryable<EnergyUsageLog> logQuery) {
        return base.IncludeReferences(logQuery)
            .Include(log => log.Device)
            .Include(log => log.Lab);
    }

    protected override async Task IncludeReferencesForFindAsync(EnergyUsageLog log)
    {
        await base.IncludeReferencesForFindAsync(log);
        var deviceLoadTask = _context.Entry(log).Reference(l => l.Device).LoadAsync();
        var labLoadTask = _context.Entry(log).Reference(l => l.Lab).LoadAsync();
        await Task.WhenAll(deviceLoadTask, labLoadTask);
    }
}
