using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AdminViewModel
    {

        public int Transaccioneshoy { get; set; }
        public int Transaccionestotal { get; set; }
        public int Pagoshoy { get; set; }
        public int PagosTotales { get; set; }
        public int ClientesActivos { get; set; }
        public int ClientesInactivos{ get; set; }
        public int ProductosAsignados { get; set; }



    }
}
