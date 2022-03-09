using LivingLab.Web.Models.ViewModels;
using LivingLab.Core.Models;

namespace LivingLab.Web.UIServices.EnergyUsageAnalysis;

public interface IExportToCSVService
{
    public byte[] Export(List<DeviceEnergyUsageModel> Content, string ColNames);
}