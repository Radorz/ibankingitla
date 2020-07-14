using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class NewAccountViewModel
    {
        public int Id { get; set; }
        [Remote(action: "VerifyUser", controller: "Admin")]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Usuario { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]

        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Este campo es requerido")]


        public string Apellido { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [EmailAddress(ErrorMessage = "No ingresaste un correo valido")]

        public string Correo { get; set; }

        [Display(Name = "Cedula")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(11, ErrorMessage = "solo ingresar 11 digitos faltantes")]

        public string Cedula{ get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido")]


        public string Contraseña { get; set; }

        [Display(Name = "Repetir Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [Compare("Contraseña", ErrorMessage = "Las contraseña no coinciden")]
        public string RepeatContraseña { get; set; }


        public decimal MontoInicial { get; set; }


        public List<string> Roles  { get; set; }
        public string RoleSelect { get; set; }

    }
}
