using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Transacciones
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cuentaorigen { get; set; }
        public string Cuentadestino { get; set; }
        public decimal? Monto { get; set; }

        public virtual ProductosUsers CuentadestinoNavigation { get; set; }
        public virtual ProductosUsers CuentaorigenNavigation { get; set; }
    }
}
