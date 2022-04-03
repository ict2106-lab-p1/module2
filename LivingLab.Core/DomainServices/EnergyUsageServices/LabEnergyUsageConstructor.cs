using System.Text;
using LivingLab.Core.Interfaces.Services.EnergyUsageInterfaces;
using LivingLab.Core.Entities.DTO.EnergyUsageDTOs;
using LivingLab.Core.Entities;

namespace LivingLab.Core.DomainServices.EnergyUsageServices;
/// <remarks>
/// Author: Team P1-2
/// </remarks>
public class LabEnergyUsageConstructor: ConstructEnergyUsageTemplates<string>
{
    public override List<string> GetIdentifier(List<EnergyUsageLog> logs)
    {
        var uniqueLab = new List<string>();
        foreach (var item in logs)
        {
            if (!uniqueLab.Contains(item.Lab.LabLocation))
            {
                uniqueLab.Add(item.Lab.LabLocation);    
            }
        }
        return uniqueLab;
    }

    protected List<LabEnergyUsageDTO> MergeIntoCollection(List<string> identifier, 
        List<double> intensity, List<double> cost, List<double> totalEU)
    {
        var labDTO = new List<LabEnergyUsageDTO>();
        for (int i = 0; i < identifier.Count; i++)
        {
            labDTO.Add(
                new LabEnergyUsageDTO{
                    LabLocation = identifier[i],
                    TotalEnergyUsage = totalEU[i],
                    EnergyUsageIntensity = intensity[i],
                    EnergyUsageCost = cost[i]
                }
            );
        }
        return labDTO;

    }

    protected List<double> GetTotalEU(List<EnergyUsageLog> logs, List<string> identifierList)
    {
        return base.BasicGetTotalEU(logs,identifierList);
    }

    protected List<double> GetEUCost(List<double> energyList)
    {
        return BasicGetEUCost(energyList);
    }

    protected List<double> GetIntensity(List<double> totalEU, List<int> area)
    {
        return BasicGetIntensity(totalEU, area);
    }
}
