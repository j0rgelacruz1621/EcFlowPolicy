using System;
using System.Collections.Generic;

namespace CPCB
{
    // Estructura para guardar los datos del plan
    public class PlanSeguro
    {
        public string Nombre { get; set; }
        public decimal PrecioUSD { get; set; }
        public decimal CobDanosCosas { get; set; }
        public decimal CobDanosPersonas { get; set; }
        public decimal ExcesoLimite { get; set; }
        public decimal CobInvalidez { get; set; }
        public decimal CobGastosMedicos { get; set; }
        public decimal CobMuerte { get; set; }
        public decimal IndemnizacionSemanal { get; set; }
        public decimal GruaDanosMecanicos { get; set; }
        public decimal GruaSiniestro { get; set; }
        public decimal AsistenciaLegal { get; set; }
        public decimal CobDefensaPenal { get; set; }
        // Sobrescribir ToString() para mostrar el nombre
        public override string ToString()
        {
            return Nombre;
        }
    }
}