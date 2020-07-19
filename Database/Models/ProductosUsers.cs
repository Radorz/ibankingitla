using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class ProductosUsers
    {
        public ProductosUsers()
        {
            TransaccionesCuentadestinoNavigation = new HashSet<Transacciones>();
            TransaccionesCuentaorigenNavigation = new HashSet<Transacciones>();
        }

        public string Id { get; set; }
        public string Idusuario { get; set; }
        public int Idtipo { get; set; }
        public string Tipo { get; set; }
        public decimal Balance { get; set; }
        public decimal LimiteTarjeta { get; set; }
        public decimal MontoPrestamo { get; set; }

        public virtual ICollection<Transacciones> TransaccionesCuentadestinoNavigation { get; set; }
        public virtual ICollection<Transacciones> TransaccionesCuentaorigenNavigation { get; set; }
    }
}
