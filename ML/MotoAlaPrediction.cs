using Microsoft.ML.Data;

namespace MottuNET.ML
{
    public class MotoAlaPrediction
    {
        [ColumnName("PredictedLabel")]
        public string AlaPrevista { get; set; } = string.Empty;
    }
}
