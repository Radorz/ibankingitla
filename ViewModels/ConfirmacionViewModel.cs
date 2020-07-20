using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ConfirmacionViewModel
    {

        public string beneficiario { get; set; }


        public string Corigen { get; set; }


        public string Cdestino { get; set; }


        public decimal Montopuro { get; set; }
        public string Monto { get; set; }



    }
}
