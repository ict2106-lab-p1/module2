using LivingLab.Core.Interfaces.Services;
using LivingLab.Core.Models;

namespace LivingLab.Web.UIServices.EnergyUsageAnalysisServices;

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