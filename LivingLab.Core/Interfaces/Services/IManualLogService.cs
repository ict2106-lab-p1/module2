using Microsoft.AspNetCore.Http;

namespace LivingLab.Core.Interfaces.Services;

public interface IManualLogService
{
    void Process(IFormFile file);
    void Archieve(int deviceId, DateTime start, DateTime end);
    void Archieve(string deviceType, DateTime start, DateTime end);
}
