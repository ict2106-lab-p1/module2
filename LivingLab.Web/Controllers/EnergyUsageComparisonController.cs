using System.Diagnostics;

using LivingLab.Core.Entities.DTO.EnergyUsageDTOs;

using Microsoft.AspNetCore.Mvc;
using LivingLab.Core.Interfaces.Repositories;
using LivingLab.Core.Entities;
using LivingLab.Web.Models.ViewModels;
using LivingLab.Web.UIServices.EnergyUsage;
using System.Data;
using System.Text.RegularExpressions;

namespace LivingLab.Web.Controllers;
/// <remarks>
/// Author: Team P1-2
/// </remarks>
public class EnergyUsageComparisonController : Controller
{
    private readonly ILogger<EnergyUsageComparisonController> _logger;
    private readonly IEnergyUsageRepository _repository;
    private readonly IEnergyUsageComparisonService _comparisonService;
    public EnergyUsageComparisonController(ILogger<EnergyUsageComparisonController> logger, IEnergyUsageRepository repository, IEnergyUsageComparisonService comparisonService)
    {
        _logger = logger;
        _repository = repository;
        _comparisonService = comparisonService;
    }

    public IActionResult Index()
    {
        return View();
    }

    //need to implement the return type and implementation 
    public IActionResult GetLabEnergyUsageDetailTable(string listOfLabId, DateTime start, DateTime end)
    {
        return View();
    }

    public IActionResult GetLabEnergyUsageDetailGraph(string listOfLabId, DateTime start, DateTime end)
    {
        return View();
    }

    public IActionResult GetDeviceEnergy(string listOfDeviceType, DateTime start, DateTime end)
    {
        return View();
    }

    public IActionResult GetLabLocationOrDeviceType(string type)
    {
        return View();
    }
    // end of skeleton code


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpGet]
    public string[] GetType(string compareType)
    {
        if (compareType == "DeviceType")
        {
            //need to get from db

            //List<string> names = _comparisonService.GetAllLabLocation();


            string[] array = { "Light", "Robot", "Microprocessor", "VR Device" };
            return array;
            //List<LabEnergyUsageDTO> names = _comparisonService.GetAllLabLocation();

            //return names;
        }
        else
        {
            //need to get from db
            //List<strinList<string> names = _comparisonService.GetAllLabLocation();


            //List<LabEnergyUsageDTO> names = _comparisonService.GetAllLabLocation();

            //return names; 
            string[] array = { "SR5A", "SR7H", "LT3A", "SR6G" };
            return array;
        }

    }


    //need to add benchmark for the lab graph
    [HttpPost]
    public JsonResult GetGraph(string startDate, string endDate, string compareFactor)
    {
        string start = Regex.Replace(startDate, @"[^^a-zA-Z0-9 -]+", String.Empty);
        string end = Regex.Replace(endDate, @"[^^a-zA-Z0-9 -]+", String.Empty);

        string compareType = "Lab";
        string[] compareFactorArray = compareFactor.Split(",");

        List<object> iData = new List<object>();
        //Creating sample data  
        DataTable dt = new DataTable();
        dt.Columns.Add(compareType, System.Type.GetType("System.String"));
        dt.Columns.Add("Energy Usage", System.Type.GetType("System.Int32"));
        dt.Columns.Add("Energy Intensity", System.Type.GetType("System.Int32"));
        dt.Columns.Add("Benchmark", System.Type.GetType("System.Int32"));//for energy usage

        DataRow dr = dt.NewRow();

        for (int i = 0; i < compareFactorArray.Length; i++)
        {
            compareFactorArray[i] = Regex.Replace(compareFactorArray[i], @"[^^a-zA-Z0-9 -]+", String.Empty);
            if (i == 0)
            {
                dr[compareType] = compareFactorArray[i];
                dr["Energy Usage"] = 213; //need to get from db
                dr["Energy Intensity"] = 123; //need to get from db
                dr["Benchmark"] = 120;//need to get from db
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr[compareType] = compareFactorArray[i];
                dr["Energy Usage"] = 200; //need to get from db
                dr["Energy Intensity"] = 456; //need to get from db
                dr["Benchmark"] = 120;//need to get from db
                dt.Rows.Add(dr);
            }
        }

        foreach (DataColumn dc in dt.Columns)
        {
            List<object> x = new List<object>();
            x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
            iData.Add(x);
        }

        return Json(iData);
    }


    [HttpPost]
    public List<Dictionary<string, object>> GetLabTable(string startDate, string endDate, string compareFactor)
    {
        string start = Regex.Replace(startDate, @"[^^a-zA-Z0-9 -]+", String.Empty);
        string end = Regex.Replace(endDate, @"[^^a-zA-Z0-9 -]+", String.Empty);

        string compareType = "Lab";
        string[] compareFactorArray = compareFactor.Split(",");

        List<string> listNumber = new List<string>();

        List<Dictionary<string, object>> iData = new List<Dictionary<string, object>>();
        //Creating sample data  
        DataTable dt = new DataTable();
        dt.Columns.Add("labLocation", System.Type.GetType("System.String"));
        dt.Columns.Add("energyUsage", System.Type.GetType("System.Int32"));
        dt.Columns.Add("energyUsageCost", System.Type.GetType("System.Int32"));
        dt.Columns.Add("averageEnergyUsage", System.Type.GetType("System.Int32"));
        dt.Columns.Add("energyIntensity", System.Type.GetType("System.Int32"));

        DataRow dr = dt.NewRow();

        for (int i = 0; i < compareFactorArray.Length; i++)
        {
            compareFactorArray[i] = Regex.Replace(compareFactorArray[i], @"[^^a-zA-Z0-9 -]+", String.Empty);
            if (i == 0)
            {
                dr["labLocation"] = compareFactorArray[i];
                dr["energyUsage"] = 213; //need to get from db
                dr["energyUsageCost"] = 123; //need to get from db
                dr["averageEnergyUsage"] = 120;//need to get from db
                dr["energyIntensity"] = 120;//need to get from db
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["labLocation"] = compareFactorArray[i];
                dr["energyUsage"] = 200; //need to get from db
                dr["energyUsageCost"] = 456; //need to get from db
                dr["averageEnergyUsage"] = 120;//need to get from db
                dr["energyIntensity"] = 120;//need to get from db
                dt.Rows.Add(dr);
            }
        }

        foreach (DataRow dr1 in dt.Rows)
        {
            Dictionary<string, object> drow = new Dictionary<string, object>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                drow.Add(dt.Columns[i].ColumnName, dr1[i]);
            }
            iData.Add(drow);
        }



        return iData;
    }

    [HttpPost]
    public List<Dictionary<string, object>> GetDeviceTable(string startDate, string endDate, string compareFactor)
    {

        string start = Regex.Replace(startDate, @"[^^a-zA-Z0-9 -]+", String.Empty);
        string end = Regex.Replace(endDate, @"[^^a-zA-Z0-9 -]+", String.Empty);

        string compareType = "Device Type";
        string[] compareFactorArray = compareFactor.Split(",");

        List<string> listNumber = new List<string>();

        List<Dictionary<string, object>> iData = new List<Dictionary<string, object>>();
        //Creating sample data  
        DataTable dt = new DataTable();
        dt.Columns.Add("deviceType", System.Type.GetType("System.String"));
        dt.Columns.Add("energyUsage", System.Type.GetType("System.Int32"));
        dt.Columns.Add("energyUsageCost", System.Type.GetType("System.Int32"));
        dt.Columns.Add("averageEnergyUsage", System.Type.GetType("System.Int32"));

        DataRow dr = dt.NewRow();

        for (int i = 0; i < compareFactorArray.Length; i++)
        {
            compareFactorArray[i] = Regex.Replace(compareFactorArray[i], @"[^^a-zA-Z0-9 -]+", String.Empty);
            if (i == 0)
            {
                dr["deviceType"] = compareFactorArray[i];
                dr["energyUsage"] = 213; //need to get from db
                dr["energyUsageCost"] = 123; //need to get from db
                dr["averageEnergyUsage"] = 120;//need to get from db
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["deviceType"] = compareFactorArray[i];
                dr["energyUsage"] = 200; //need to get from db
                dr["energyUsageCost"] = 456; //need to get from db
                dr["averageEnergyUsage"] = 120;//need to get from db
                dt.Rows.Add(dr);
            }
        }

        foreach (DataRow dr1 in dt.Rows)
        {
            Dictionary<string, object> drow = new Dictionary<string, object>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                drow.Add(dt.Columns[i].ColumnName, dr1[i]);
            }
            iData.Add(drow);
        }



        return iData;
    }
}
