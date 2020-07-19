using Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ClienteViewModel
    {

   
        public decimal BalanceCuentaAhorro { get; set; }
        public decimal DisponibleCuentaAhorro { get; set; }
        public List<ProductosUsers> Ahorros { get; set; }

        public decimal BalanceTarjeta { get; set; }
        public decimal DisponibleTarjeta { get; set; }
        public List<ProductosUsers> Tarjetas { get; set; }

        public decimal BalancePrestamo { get; set; }
        public List<ProductosUsers> Prestamos { get; set; }

    }
}
