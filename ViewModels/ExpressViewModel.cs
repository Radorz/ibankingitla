using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ExpressViewModel
    {

        public List<string> Cuentas { get; set; }

        [Display(Name = "Cuenta Origen")]
        [Required(ErrorMessage = "Es necesario una cuenta de donde Transferir")]
        public string Corigen { get; set; }

        [Remote(action: "VerifyAccount", controller: "Cliente")]
        [Display(Name = "Cuenta Destino")]
        [Required(ErrorMessage = "La Cuenta a depositar es requerida")]
        public string Cdestino { get; set; }

        [Remote(action: "VerifyMoney", controller: "Pagos",AdditionalFields = nameof(Corigen))]
        [Display(Name = "Monto a Transferir")]
        [Range(50.0, 999999999.99,ErrorMessage = "Se debe ingresar un monto a Mayor a $50.00")]
        [Required(ErrorMessage = "Se debe ingresar el monto a transferir")]
        public decimal Monto { get; set; }


    }
}
