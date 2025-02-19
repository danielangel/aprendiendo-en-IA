using Microsoft.ML.Data;

namespace FactoringAI.Model
{
    public class ClienteData
    {
        [LoadColumn(0)] public float Ingresos { get; set; }
        [LoadColumn(1)] public float Deudas { get; set; }
        [LoadColumn(2)] public float PuntajeCredito { get; set; }
        [LoadColumn(3)] public bool Aprobado { get; set; }
    }
}
