using LivingLab.Core.Entities;
using LivingLab.Core.Entities.Identity;
using LivingLab.Core.Repositories.EnergyUsage;
using LivingLab.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace LivingLab.Infrastructure.Repositories.EnergyUsage;

/// <remarks>
/// Author: Team P1-1
/// </remarks>
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
            log.Device = await _context.Devices.FirstOrDefaultAsync(d => d.SerialNo == log.Device.SerialNo);
            log.Lab = await _context.Labs.FirstOrDefaultAsync(l => l.LabLocation == log.Lab.LabLocation);
            await _context.AddAsync(log);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<EnergyUsageLog>> GetDeviceEnergyUsageByDateTime(DateTime start, DateTime end)
    {
        var logsForDateRange = await IncludeReferences(
                _context.EnergyUsageLogs
                .Where(log => log.LoggedDate >= start && log.LoggedDate <= end)
            )
            .ToListAsync();
        return logsForDateRange;
    }

    public async Task<List<EnergyUsageLog>> GetDeviceEnergyUsageByDeviceTypeAndDate(string deviceType, DateTime start, DateTime end)
    {
        var logsForTypeInDateRange = await IncludeReferences(
                _context.EnergyUsageLogs
                .Where(log => log.LoggedDate >= start && log.LoggedDate <= end)
                .Where(log => log.Device!.Type == deviceType)
            )
            .ToListAsync();
        return logsForTypeInDateRange;
    }

    public async Task<List<EnergyUsageLog>> GetLabEnergyUsageByLabNameAndDate(string labName, DateTime start, DateTime end)
    {
        var logsForTypeInDateRange = await IncludeReferences(
                _context.EnergyUsageLogs
                .Where(log => log.LoggedDate >= start && log.LoggedDate <= end)
                .Where(log => log.Lab.LabLocation == labName)
            )
            .ToListAsync();
        return logsForTypeInDateRange;
    }

    public async Task<List<EnergyUsageLog>> GetDeviceEnergyUsageByLabAndDate(int labId, DateTime? start, DateTime? end)
    {
        var now = DateTime.Now;
        
        start ??= now.AddDays(-30).Date + new TimeSpan(0, 0, 0);;
        end ??= now.Date + new TimeSpan(23, 59, 59);
        
        var logsForLabInDateRange = await IncludeReferences(
                _context.EnergyUsageLogs
                    .Where(log => log.LoggedDate >= start && log.LoggedDate <= end)
                    .Where(log => log.Lab!.LabId == labId)
            )
            .ToListAsync();
        return logsForLabInDateRange;
    }
    
    // JOEY: start
    public async Task<List<EnergyUsageLog>> GetLabEnergyUsageByLocationAndDate(string labLocation, DateTime? start, DateTime? end)
    {
        var now = DateTime.Now;
        
        start ??= now.AddDays(-30).Date + new TimeSpan(0, 0, 0);;
        end ??= now.Date + new TimeSpan(23, 59, 59);
        
        Console.WriteLine("START: " + start);
        Console.WriteLine("END: " + end);
        
        var logsForLabInDateRange = await IncludeReferences(
                _context.EnergyUsageLogs
                    .Where(log => log.LoggedDate >= start && log.LoggedDate <= end)
                    .Where(log => log.Lab!.LabLocation == labLocation)
                )
            .ToListAsync();
        return logsForLabInDateRange;
    }
    
    public async Task<List<EnergyUsageLog>> GetLabEnergyUsageByDate(DateTime? start, DateTime? end)
    {
        var now = DateTime.Now;
        
        start ??= now.AddDays(-30).Date + new TimeSpan(0, 0, 0);;
        end ??= now.Date + new TimeSpan(23, 59, 59);
        
        var logsForLabInDateRange = await IncludeReferences(
                _context.EnergyUsageLogs
                    .Where(log => log.LoggedDate >= start && log.LoggedDate <= end)
            )
            .ToListAsync();
        return logsForLabInDateRange;
    }

    // JOEY: end

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
                .Where(log => log.Lab!.LabId == id)
            )
            .ToListAsync();
        return logsForLab;
    }
    
    protected override IQueryable<EnergyUsageLog> IncludeReferences(IQueryable<EnergyUsageLog> logQuery)
    {
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
