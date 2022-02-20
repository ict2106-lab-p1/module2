using LivingLab.DataGenerator;

try
{
    EnergyUsageGenerator.GenerateEnergyUsageCsv();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}








