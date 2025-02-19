using FactoringAI.Data;
using FactoringAI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;

namespace FactoringAI.Services
{
    public class ClienteService :Controller
    {
        private  FactoringContext _context;

        public ClienteService(FactoringContext context)
        {
            _context = context;
        }

        public async Task<bool> EvaluarClienteAsync(Cliente cliente)
        {
            var clienteData = new ClienteData
            {
                Ingresos = (float)cliente.Ingresos,
                Deudas = (float)cliente.Deudas,
                PuntajeCredito = cliente.PuntajeCredito
            };
            cargarDatos();
            return ClienteModel.Predict(clienteData);
        }
        public void cargarDatos()
        {
            var mlContext = new MLContext();

            var datos = new[]
       {
            new ClienteData { Ingresos = 50000, Deudas = 10000, PuntajeCredito = 750, Aprobado = true },
            new ClienteData { Ingresos = 30000, Deudas = 20000, PuntajeCredito = 600, Aprobado = false },
            new ClienteData { Ingresos = 80000, Deudas = 15000, PuntajeCredito = 800, Aprobado = true },
            new ClienteData { Ingresos = 40000, Deudas = 25000, PuntajeCredito = 500, Aprobado = false }
        };

            // Cargar los datos en un IDataView
            var dataView = mlContext.Data.LoadFromEnumerable(datos);

            // Definir el pipeline de entrenamiento
            var pipeline = mlContext.Transforms.Concatenate("Features", nameof(ClienteData.Ingresos), nameof(ClienteData.Deudas), nameof(ClienteData.PuntajeCredito))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: nameof(ClienteData.Aprobado), featureColumnName: "Features"));

            // Entrenar el modelo
            var model = pipeline.Fit(dataView);

            // Guardar el modelo en un archivo .zip
            string rutaModelo = "c:\\Temp\\modelo.zip";
            mlContext.Model.Save(model, dataView.Schema, rutaModelo);

            Console.WriteLine($"Modelo guardado en {rutaModelo}");
        }

 

        public async Task AgregarClienteAsync(Cliente cliente)
        {
            try
            {
                 await  _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(nameof(cliente), ex.Message.ToString());
            }
        
        }
    }
}
