using Microsoft.ML.Data;

namespace MottuNET.ML
{
    public class MotoAlaData
    {
        [LoadColumn(0)]
        public string Problema { get; set; } = string.Empty;

        [LoadColumn(1)]
        public string Status { get; set; } = string.Empty;

        [LoadColumn(2)]
        public string Ala { get; set; } = string.Empty; 
    }
}
