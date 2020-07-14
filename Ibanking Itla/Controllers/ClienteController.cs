using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ibanking_Itla.Controllers
{
    [Authorize(Roles = "cliente")]
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Beneficiarios()
        {
            return View();
        }
        public IActionResult PagoExpress()
        {
            return View();
        }
        public IActionResult Confirmacion()
        {
            return View();
        }
        public IActionResult TarjetaCredito()
        {
            return View();
        }
        public IActionResult PagoPrestamo()
        {
            return View();
        }
        public IActionResult PagoBeneficiario()
        {
            return View();
        }
    }
}
