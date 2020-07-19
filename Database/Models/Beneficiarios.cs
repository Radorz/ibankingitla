using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Beneficiarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NoCuenta { get; set; }
        public string Idcreador { get; set; }

        public virtual Users IdcreadorNavigation { get; set; }
    }
}
