using LivingLab.Core.Models;
namespace LivingLab.Core.Interfaces.Services;

public interface IExportData
{
     public string ExportContentBuilder(List<DeviceEnergyUsageModel> Content, string ColNames);
}