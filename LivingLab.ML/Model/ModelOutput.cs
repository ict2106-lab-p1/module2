using Microsoft.ML.Data;

namespace LivingLab.ML.Model;

public class ModelOutput
{
    [ColumnName("Score")]
    public float EnergyUsage;
}
