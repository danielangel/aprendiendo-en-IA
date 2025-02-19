using Microsoft.ML.Data;

namespace FactoringAI.Model
{
    public class ClientePrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Aprobado { get; set; }
    }
}
