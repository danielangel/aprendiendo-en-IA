using System.ComponentModel.DataAnnotations;

namespace FactoringAI.Model
{
    public class Cliente
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Ingresos { get; set; }
        public decimal Deudas { get; set; }
        public int PuntajeCredito { get; set; }
        public bool Aprobado { get; set; }
    }
}
