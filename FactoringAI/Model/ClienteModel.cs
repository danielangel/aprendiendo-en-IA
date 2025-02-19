using Microsoft.ML;

namespace FactoringAI.Model
{
    public class ClienteModel
    {
        private static MLContext mlContext = new MLContext();
        private static ITransformer model;

        public static void TrainModel(string dataPath)
        {
            var data = mlContext.Data.LoadFromTextFile<ClienteData>(dataPath, hasHeader: true, separatorChar: ',');
            var pipeline = mlContext.Transforms.Concatenate("Features", "Ingresos", "Deudas", "PuntajeCredito")
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Aprobado", featureColumnName: "Features"));

            model = pipeline.Fit(data);
        }

        public static bool Predict(ClienteData cliente)
        {
            CargaClienteModel();
            try
            {
                if (cliente == null)
                {
                    throw new ArgumentNullException(nameof(cliente), "El modelo no puede ser nulo.");
                }

                var predEngine = mlContext.Model.CreatePredictionEngine<ClienteData, ClientePrediction>(model);
                var prediction = predEngine.Predict(cliente);
                return prediction.Aprobado;

            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(nameof(cliente), ex.Message.ToString());
            }

        }

        public static void CargaClienteModel()
        {
            if (!System.IO.File.Exists("C:\\Temp\\modelo.zip"))
            {
                throw new System.IO.FileNotFoundException($"El archivo del modelo en la ruta '{"C:\\Temp\\modelo.zip"}' no existe.");
            }

            // Cargar el modelo desde el archivo .zip
            model = mlContext.Model.Load("C:\\Temp\\modelo.zip", out var modelSchema);
            
        }   

    }

}
