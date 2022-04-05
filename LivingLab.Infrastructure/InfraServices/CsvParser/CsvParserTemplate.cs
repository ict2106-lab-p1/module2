using System.Text;
using LivingLab.Core.CsvParser;
using LivingLab.Core.Entities.DTO.EnergyUsage;
using Microsoft.AspNetCore.Http;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace LivingLab.Infrastructure.InfraServices.CsvParser;

/// <remarks>
/// Author: Team P1-1
/// </remarks>
public abstract class CsvParserTemplate : IEnergyUsageLogCsvParser
{
    public IEnumerable<EnergyUsageCsvDTO> Parse(IFormFile file)
    {
        var filePath = SaveFile(file);
        var options = new CsvParserOptions(true, ',');
        var mapper = new EnergyUsageLogCsvMapper();
        var parser = new CsvParser<EnergyUsageCsvDTO>(options, mapper);
        var result = parser.ReadFromFile(filePath, Encoding.Default);

        return MapResult(result);    
    }
    
    public abstract string SaveFile(IFormFile file);
    public abstract IEnumerable<EnergyUsageCsvDTO> MapResult(ParallelQuery<CsvMappingResult<EnergyUsageCsvDTO>> result);
}
