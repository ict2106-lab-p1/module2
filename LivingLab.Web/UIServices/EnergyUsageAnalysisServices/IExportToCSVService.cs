using LivingLab.Core.Models;

namespace LivingLab.Web.UIServices.EnergyUsageAnalysisServices;

public interface IExportToCSVService
{
    public string Export(List<DeviceEnergyUsageModel> Content, string ColNames);
}
