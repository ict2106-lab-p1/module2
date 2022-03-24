using LivingLab.Core.Interfaces.Services.EnergyUsageInterfaces;
using LivingLab.Core.Entities.DTO.EnergyUsageDTOs;
using LivingLab.Core.Entities;
using LivingLab.Core.Interfaces.Repositories;

namespace LivingLab.Core.DomainServices.EnergyUsageServices;
/// <remarks>
/// Author: Team P1-2
/// </remarks>
public class EnergyUsageComparisonService : IEnergyUsageComparisonService
{
    //hy, not sure what is your DTO, the DTO have to create in LivingLab.Core.Entities.DTO.EnergyUsageDTOs
    //private readonly IEnergyUsageRepository _repository;

    public List<DeviceEnergyUsageDTO> GetEnergyUsageByLabID(List<int> labIds, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }
    public List<LabEnergyUsageDTO> GetEnergyUsageByDeviceType(List<string> deviceTYpe, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }
    public List<LabEnergyUsageDTO> GetEnergyUsageEnergyIntensitySelectedLab (int labId, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }
    public List<string> GetAllDeviceType()
    {
        throw new NotImplementedException();
    }
    public List<string> GetAllLabLocation()
    {
        //List<EnergyUsageLog> result = _repository.GetAllLabLocation().Result;
        //List<string> labNames = new List<string>();
        //List<LabEnergyUsageDTO> labList = new List<LabEnergyUsageDTO>();

        //foreach (var item in result)
        //{
        //    labNames.Add(item.Lab.LabLocation);
        //}

        //for (int i = 0; i < labNames.Count; i++)
        //{
        //    labList.Add(new LabEnergyUsageDTO
        //    {
        //        LabLocation = labNames[i]
        //    });

        //}

        //return labList;

        throw new NotImplementedException();
    }
}
