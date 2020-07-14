using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class TiposProductos
    {
        public TiposProductos()
        {
            Pagos = new HashSet<Pagos>();
            ProductosUsers = new HashSet<ProductosUsers>();
        }

        public int Id { get; set; }
        public string NombreProducto { get; set; }

        public virtual ICollection<Pagos> Pagos { get; set; }
        public virtual ICollection<ProductosUsers> ProductosUsers { get; set; }
    }
}
