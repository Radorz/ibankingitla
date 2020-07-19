using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Pagos
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string ProductoOrigen { get; set; }
        public string ProductoDestino { get; set; }
        public decimal? Monto { get; set; }
        public int? TipoProducto { get; set; }
    }
}
