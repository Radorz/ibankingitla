using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using ViewModels;

namespace Ibanking_Itla.Controllers
{
    [Authorize(Roles = "cliente")]
    public class PagosController : Controller
    {
        private readonly AdminRepository _adminrepository;
        private readonly ProductosRepository _productsrepository;
        private readonly BeneficiariosRepository _beneficiariosrepository;
        private readonly PagosRepository _pagosrepository;
        private readonly TransferenciasRepository _transrepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _role;
        private readonly IMapper _mapper;


        public PagosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> role, IMapper mapper,  ProductosRepository productsrepository, PagosRepository pagosrepository,
            TransferenciasRepository transrepository, AdminRepository adminrepository, BeneficiariosRepository beneficiariosrepository)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _role = role;
            this._mapper = mapper;
            this._productsrepository = productsrepository;
            this._pagosrepository = pagosrepository;
            this._transrepository = transrepository;
            this._adminrepository = adminrepository;
            this._beneficiariosrepository = beneficiariosrepository;
}
       
        public async Task<IActionResult> PagoExpress()
        {

            var users = await _userManager.FindByNameAsync(User.Identity.Name);

            var Cuentas = await _productsrepository.GetAllCuentas(users.Id.Trim());
            var vm = new ExpressViewModel();
            var list = new List<string>();

            foreach ( var item in Cuentas)
            {

                list.Add(item.Id.Trim() + " | " + "Cuenta de Ahorro" + " | " + " | " + item.Balance.ToString("C"));

            }

            vm.Cuentas = list;
            return View(vm);
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
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyMoney(decimal Monto,string Corigen)
        {

            var CuentaOrigen = Corigen.Substring(0, 9);
            var cuenta = await _productsrepository.GetbyIdnew(Convert.ToInt32(CuentaOrigen));
            if (cuenta.Balance >= Monto){
                return Json(true);


            }
          
            return Json($"No tienes suficiente balance para este pago. Tu balance en cuenta {cuenta.Balance.ToString("C")}");
        }
    }
}
