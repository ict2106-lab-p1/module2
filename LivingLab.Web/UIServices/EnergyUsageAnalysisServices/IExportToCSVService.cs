using LivingLab.Web.Models.ViewModels;
using LivingLab.Core.Models;

namespace LivingLab.Web.UIServices.EnergyUsageAnalysis;

public interface IExportToCSVService
{
    public string Export(List<DeviceEnergyUsageModel> Content, string ColNames);
}