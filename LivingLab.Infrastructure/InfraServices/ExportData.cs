using System.Text;

using LivingLab.Core.Interfaces.Services;
using LivingLab.Core.Models;

namespace LivingLab.Infrastructure.InfraServices;

public class ExportData : IExportData
{
    public string ExportContentBuilder (List<DeviceEnergyUsageModel> Content, string ColNames)
    {
        // var builder = new StringBuilder();
        // builder.AppendLine(ColNames);
        // foreach (var item in Content)
        // {
        //     builder.AppendLine($"{item.DeviceSerialNo},{item.DeviceType},{item.TotalEnergyUsage},{item.EnergyUsagePerHour},{item.EnergyUsageCost}");
        // }
        // return builder.ToString();
        return "success";
    }
}