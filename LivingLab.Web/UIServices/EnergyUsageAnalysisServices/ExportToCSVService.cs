using LivingLab.Web.Models.ViewModels;
using LivingLab.Core.Models;
using LivingLab.Core.Interfaces.Services;

namespace LivingLab.Web.UIServices.EnergyUsageAnalysis;

public class ExportToCSVService : IExportToCSVService
{
    private readonly IExportData _exportData;

    public ExportToCSVService (IExportData exportData) {
        _exportData = exportData;
    }
    public string Export(List<DeviceEnergyUsageModel> Content, string ColNames) {
        return _exportData.ExportContentBuilder(Content, ColNames);
    }
}