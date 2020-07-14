using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Users
    {
        public Users()
        {
            ProductosUsers = new HashSet<ProductosUsers>();
        }

        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }


        public virtual ICollection<ProductosUsers> ProductosUsers { get; set; }
    }
}
