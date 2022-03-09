using LivingLab.Core.Models;
namespace LivingLab.Core.Interfaces.Services;

public interface IExportData
{
     byte[] ExportContentBuilder(List<DeviceEnergyUsageModel> Content, string ColNames);
}