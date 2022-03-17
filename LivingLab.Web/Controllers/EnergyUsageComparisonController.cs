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
    private readonly ILogger<EnergyUsageAnalysisController> _logger;
    private readonly IEnergyUsageRepository _repository;
    private readonly IEnergyUsageComparisonService _comparisonService;
    public EnergyUsageComparisonController(ILogger<EnergyUsageAnalysisController> logger, IEnergyUsageRepository repository, IEnergyUsageComparisonService comparisonService)
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
            string[] array = { "Light", "Robot", "Microprocessor", "VR Device" };
            return array;
        }
        else
        {
            //need to get from db
            string[] array = { "SR6H", "LT4A", "SR5G", "SR7A" };
            return array;
        }
        
    }



    //need to add bench-mark for the lab graph
    [HttpPost]
    public JsonResult GetGraph(string type, string compareFactor)
    {
        //string compareType = type.Substring(1, type.Length - 2);
        string compareType = Regex.Replace(type, @"[^A-Z^a-z]+", String.Empty);
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



    //public GetLabEnergyUsageDetail(int labId)
    //{

    //    return true;
    //}

    //public GetDeviceEnergy()
    //{

    //}
}
