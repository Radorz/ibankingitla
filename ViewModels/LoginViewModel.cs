using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "El Usuario es requerido")]
        public string User { get; set; }
        [Required(ErrorMessage = "La Contraseña es requerida")]
        public string Password { get; set; }


    }
}
