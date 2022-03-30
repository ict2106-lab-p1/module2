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
    private readonly IEnergyUsageRepository _repository;

    private readonly IDeviceRepository _deviceRepository;

    private readonly ILabRepository _abRepository;

    private readonly IEnergyUsageCalculationService _calculator = new EnergyUsageCalculationService();

    private double cost = 0.2544;

    public EnergyUsageComparisonService(IEnergyUsageRepository repository, IDeviceRepository deviceRepository, ILabRepository labRepository)
    {
        _repository = repository;
        _deviceRepository = deviceRepository;
        _abRepository = labRepository;
    }

    //public List<LabEnergyUsageDTO> GetEnergyUsageByLabID(int labIds, DateTime start, DateTime end)
    public List<EnergyComparisonLabTableDTO> GetEnergyUsageByLabNameTable(string labName, DateTime start, DateTime end)
    {
        //throw new NotImplementedException();
        List<EnergyUsageLog> result = _repository.GetLabEnergyUsageByLabNameAndDate(labName, start, end).Result;
        int LabEU = 0;
        int LabUsageTime = 0;
        int LabEUPerHour = 0;
        double LabEUCost = 0;
        double energyIntensity = 0;
        int LabArea = 0;


        List<EnergyComparisonLabTableDTO> LabEUList = new List<EnergyComparisonLabTableDTO>();


        foreach (var item in result)
        {
            LabEU += (int)item.EnergyUsage;
            LabUsageTime += item.Interval.Minutes;
            //not sure how to get the lab area
            //LabArea = item.
        }

        LabEUPerHour = _calculator.CalculateEnergyUsagePerHour(LabEU, LabUsageTime);
        LabEUCost = _calculator.CalculateEnergyUsageCost(cost, LabEU, LabUsageTime);
        energyIntensity = _calculator.CalculateEnergyIntensity(LabArea, LabEU);

        LabEUList.Add(new EnergyComparisonLabTableDTO
        {
            LabLocation = labName,
            TotalEnergyUsage = LabEU,
            EnergyUsageCost = LabEUCost,
            EnergyUsagePerHour = LabEUPerHour, 
            EnergyUsageIntensity = energyIntensity
        });

        return LabEUList;
    }

    public List<EnergyComparisonGraphDTO> GetEnergyUsageByLabNameGraph(string labName, DateTime start, DateTime end)
    {
        //throw new NotImplementedException();
        List<EnergyUsageLog> result = _repository.GetLabEnergyUsageByLabNameAndDate(labName, start, end).Result;
        int LabEU = 0;
        double energyIntensity = 0;
        int LabArea = 0;


        List<EnergyComparisonGraphDTO> LabEUList = new List<EnergyComparisonGraphDTO>();


        foreach (var item in result)
        {
            LabEU += (int)item.EnergyUsage;
            //not sure how to get the lab area
            //LabArea = item.
        }

        energyIntensity = _calculator.CalculateEnergyIntensity(LabArea, LabEU);

        LabEUList.Add(new EnergyComparisonGraphDTO
        {
            LabLocation = labName,
            TotalEnergyUsage = LabEU,
            EnergyUsageIntensity = energyIntensity
        });

        return LabEUList;
    }

    public double GetEnergyUsageByLabNameBenchmark(string[] labNames, DateTime start, DateTime end)
    {
        //throw new NotImplementedException();
        double benchmark = 0;
        foreach(var i in labNames)
        {
            List<EnergyUsageLog> result = _repository.GetLabEnergyUsageByLabNameAndDate(i, start, end).Result;
        
            int LabEU = 0;

            foreach (var item in result)
            {
                LabEU += (int)item.EnergyUsage;
            }

            benchmark += LabEU;

        }

        benchmark = benchmark / labNames.Length;
        return benchmark;
    }

    //public List<DeviceEnergyUsageDTO> GetEnergyUsageByDeviceType(List<string> deviceTYpe, DateTime start, DateTime end)
    public List<EnergyComparisonDeviceTableDTO> GetEnergyUsageByDeviceType(string deviceType, DateTime start, DateTime end)
    {
        //throw new NotImplementedException();
        List<EnergyUsageLog> result = _repository.GetDeviceEnergyUsageByDeviceTypeAndDate(deviceType,start, end).Result;
        int DeviceEU = 0;
        int DeviceUsageTime = 0;
        int DeviceEUPerHour = 0;
        double DeviceEUCost = 0;


        List<EnergyComparisonDeviceTableDTO> DeviceEUList = new List<EnergyComparisonDeviceTableDTO>();

  
        foreach (var item in result)
        {
            DeviceEU += (int)item.EnergyUsage;
            DeviceUsageTime += item.Interval.Minutes;
        }
        
        DeviceEUPerHour = _calculator.CalculateEnergyUsagePerHour(DeviceEU, DeviceUsageTime);
        DeviceEUCost = _calculator.CalculateEnergyUsageCost(cost, DeviceEU, DeviceUsageTime);
       
        DeviceEUList.Add(new EnergyComparisonDeviceTableDTO
        {
            DeviceType = deviceType,
            TotalEnergyUsage = DeviceEU,
            EnergyUsageCost = DeviceEUCost,
            EnergyUsagePerHour = DeviceEUPerHour
        });

        return DeviceEUList;
    }

    public List<LabEnergyUsageDTO> GetEnergyUsageEnergyIntensitySelectedLab (int labId, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public List<string> GetAllDeviceType()
    {
        List<Device> result = _deviceRepository.GetAllDeviceType().Result;
        List<string> deviceType = new List<string>();

        foreach (var item in result)
        {
            deviceType.Add(item.Type);
        }

        return deviceType;

    }
    public List<string> GetAllLabLocation()
    {
        List<Lab> result = _abRepository.GetAllLabLocation().Result;
        List<string> labNames = new List<string>();
        //List<LabEnergyUsageDTO> labList = new List<LabEnergyUsageDTO>();

        foreach (var item in result)
        {
            labNames.Add(item.LabLocation);
        }

        //for (int i = 0; i < labNames.Count; i++)
        //{
        //    labList.Add(new LabEnergyUsageDTO
        //    {
        //        LabLocation = labNames[i]
        //    });

        //}

        //return labList;

        return labNames;

        //throw new NotImplementedException();
    }
}
