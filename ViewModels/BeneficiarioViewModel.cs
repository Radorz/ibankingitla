using Database.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BeneficiariosViewModel
    {
        public string UserOnline { get; set; }

        public List<Beneficiarios> Beneficiarios{ get; set; }


        [Remote(action: "VerifyAccount", controller: "Cliente")]
        [Display(Name = "newbeneficiario")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int newbeneficiario { get; set; }
        public int deletebeneficiario { get; set; }



    }
}
