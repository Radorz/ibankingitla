using Database.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class EditViewModel
    {
        public string Id { get; set; }
   

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


        public decimal MontoAñadido { get; set; }
        public string tipouser { get; set; }

        public decimal BalanceCuentaAhorro{ get; set; }
        public decimal DisponibleCuentaAhorro { get; set; }
        public List<ProductosUsers> Ahorros { get; set; }



        public decimal BalanceTarjeta{ get; set; }
        public decimal DisponibleTarjeta { get; set; }
        public List<ProductosUsers> Tarjetas { get; set; }

        public decimal BalancePrestamo { get; set; }
        public List<ProductosUsers> Prestamos { get; set; }



    }
}
