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
        [HttpGet]

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
        [HttpPost]
        public async Task<IActionResult> PagoExpress(ExpressViewModel vm)
        {
                return RedirectToAction("ConfirmacionTransferencia", new { destino = vm.Cdestino, monto = vm.Monto, origen =vm.Corigen});
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmacionTransferencia(int destino, decimal monto, string origen)
        {
            
            var Cuenta = await _productsrepository.GetbyIdnew(destino);
            var users = await _adminrepository.GetbyIdNew(Cuenta.Idusuario.Trim());
            var confirmacion = new ConfirmacionViewModel();
            confirmacion.beneficiario = users.Nombre.Trim() + " " + users.Apellido.Trim();
            confirmacion.Corigen = origen.Substring(0, 9) + " | " + "Cuenta de Ahorro";
            confirmacion.Cdestino = destino + " | " + "Cuenta de Ahorro";
            confirmacion.Montopuro = monto;

            return View(confirmacion);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmacionTransferencia(ConfirmacionViewModel vm)
        {
            var Cuentad = await _productsrepository.GetbyIdnew(Convert.ToInt32(vm.Cdestino.Substring(0, 9)));
            var Cuentao = await _productsrepository.GetbyIdnew(Convert.ToInt32(vm.Corigen.Substring(0, 9)));

            var users = await _adminrepository.GetbyIdNew(Cuentad.Idusuario.Trim());
            Cuentao.Balance = Cuentao.Balance - vm.Montopuro;
            Cuentad.Balance = Cuentad.Balance + vm.Montopuro;
            await _productsrepository.Update(Cuentao);
            await _productsrepository.Update(Cuentad);

            var historial = new Transacciones();
            historial.Fecha = DateTime.Now;
            historial.Cuentaorigen = vm.Cdestino.Substring(0, 9);
            historial.Cuentadestino = vm.Corigen.Substring(0, 9);
            historial.Monto = vm.Montopuro;
            await _transrepository.Add(historial);


            return RedirectToAction("Index", "Cliente");
        }
        [HttpGet]
        public async Task<IActionResult> TarjetaCredito()
        {
            var users = await _userManager.FindByNameAsync(User.Identity.Name);

            var Cuentas = await _productsrepository.GetAllCuentas(users.Id.Trim());
            var tarjetas = await _productsrepository.GetAllTarjetas(users.Id.Trim());
            var vm = new PagosViewModel();

            var listc = new List<string>();
            var listd = new List<string>();


            foreach (var item in Cuentas)
            {

                listc.Add(item.Id.Trim() + " | " + "Cuenta de Ahorro" + " | " + " | " + item.Balance.ToString("C"));

            }
            foreach (var item in tarjetas)
            {

                listd.Add(item.Id.Trim() + " | " + "Tarjeta de Credito" + " | " + " |  Pagar: " + item.Balance.ToString("C"));

            }

            vm.Cuentas = listc;
            vm.Cuentasdestino = listd;
            return View(vm);
        
    }
        [HttpPost]
        public async Task<IActionResult> TarjetaCredito(PagosViewModel vm)

        {
            var cdestino = vm.Cdestino.Substring(0, 9);
            var corigen = vm.Corigen.Substring(0, 9);

            return RedirectToAction("ConfirmacionPago", new { destino = cdestino, monto = vm.Monto, origen = corigen });
        }
        [HttpGet]
        public async Task<IActionResult> PagoPrestamo()
        {
            var users = await _userManager.FindByNameAsync(User.Identity.Name);

            var Cuentas = await _productsrepository.GetAllCuentas(users.Id.Trim());
            var prestamo = await _productsrepository.GetAllPrestamos(users.Id.Trim());
            var vm = new PagosViewModel();

            var listc = new List<string>();
            var listd = new List<string>();


            foreach (var item in Cuentas)
            {

                listc.Add(item.Id.Trim() + " | " + "Cuenta de Ahorro" + " | " + " | " + item.Balance.ToString("C"));

            }
            foreach (var item in prestamo) 
            {
                if ((item.MontoPrestamo - item.Balance) > 0) { 

                listd.Add(item.Id.Trim() + " | " + "Prestamo" + " | " + " |  Pagar: " + (item.MontoPrestamo - item.Balance).ToString("C"));
                }
            }

            vm.Cuentas = listc;
            vm.Cuentasdestino = listd;
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> PagoPrestamo(PagosViewModel vm)

        {
            var cdestino = vm.Cdestino.Substring(0, 9);
            var corigen = vm.Corigen.Substring(0, 9);

            return RedirectToAction("ConfirmacionPago", new { destino = cdestino, monto = vm.Monto, origen = corigen });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmacionPago(int destino, decimal monto, string origen)
        {

            var Cuenta = await _productsrepository.GetbyIdnew(destino);
            var users = await _adminrepository.GetbyIdNew(Cuenta.Idusuario.Trim());
            var confirmacion = new ConfirmacionViewModel();
            confirmacion.beneficiario = users.Nombre.Trim() + " " + users.Apellido.Trim();
            confirmacion.Corigen = origen.Substring(0, 9) + " | " + "Cuenta de Ahorro";
            if (Cuenta.Idtipo == 3)
            {
                confirmacion.Cdestino = destino + " | " + "Tarjeta de Credito";

            }
            else {
                confirmacion.Cdestino = destino + " | " + "Prestamo";

            }
            confirmacion.Montopuro = monto;

            return View(confirmacion);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmacionPago(ConfirmacionViewModel vm)
        {
            var Cuentad = await _productsrepository.GetbyIdnew(Convert.ToInt32(vm.Cdestino.Substring(0, 9)));
            var Cuentao = await _productsrepository.GetbyIdnew(Convert.ToInt32(vm.Corigen.Substring(0, 9)));

            if (Cuentad.Idtipo == 3) { 

            if (Cuentad.Balance< vm.Montopuro)
            {
                Cuentao.Balance = Cuentao.Balance - Cuentad.Balance;
                Cuentad.Balance = Cuentad.Balance - Cuentad.Balance;


            }
            else {
                Cuentao.Balance = Cuentao.Balance - vm.Montopuro;
                Cuentad.Balance = Cuentad.Balance - vm.Montopuro;
            }
            }
            if(Cuentad.Idtipo == 2) {

                if ((Cuentad.MontoPrestamo - Cuentad.Balance) < vm.Montopuro)
                {
                    Cuentao.Balance = Cuentao.Balance - (Cuentad.MontoPrestamo - Cuentad.Balance);
                    Cuentad.Balance = (Cuentad.MontoPrestamo - Cuentad.Balance);


                }
                else
                {
                    Cuentao.Balance = Cuentao.Balance - vm.Montopuro;
                    Cuentad.Balance = Cuentad.Balance + vm.Montopuro;
                }


            }

            await _productsrepository.Update(Cuentao);
            await _productsrepository.Update(Cuentad);

            var historial = new Pagos();
            historial.Fecha = DateTime.Now;
            historial.ProductoOrigen = vm.Cdestino.Substring(0, 9);
            historial.ProductoDestino = vm.Corigen.Substring(0, 9);
            historial.Monto = vm.Montopuro;
            await _pagosrepository.Add(historial);


            return RedirectToAction("Index", "Cliente");
        }
        public async Task<IActionResult> PagoBeneficiario()
        {
            var users = await _userManager.FindByNameAsync(User.Identity.Name);

            var Cuentas = await _productsrepository.GetAllCuentas(users.Id.Trim());
            var cbene = await _beneficiariosrepository.GetAll();
            var beneficiarios = cbene.Where(a => a.Idcreador == users.Id.Trim()).ToList();
            var vm = new PagoBeneViewModel();
            
            var listc = new List<string>();
            var listd = new List<string>();


            foreach (var item in Cuentas)
            {

                listc.Add(item.Id.Trim() + " | " + "Cuenta de Ahorro" + " | " + " | " + item.Balance.ToString("C"));

            }
            foreach (var item in beneficiarios)
            {

                listd.Add(item.NoCuenta.Trim() + " | " + item.Nombre + " "+ item.Apellido);

            }

            vm.Cuentas = listc;
            vm.Cuentasdestino = listd;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> PagoBeneficiario(PagoBeneViewModel vm)
        {
            var cdestino = vm.Cdestino.Substring(0, 9);
            var corigen = vm.Corigen.Substring(0, 9);

            return RedirectToAction("ConfirmacionTransferencia", new { destino = cdestino, monto = vm.Monto, origen = corigen });
        }
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyAccount(int Cdestino)
        {

            var account = await _productsrepository.GetAllCuentaForVerify();
            var resul = account.FirstOrDefault(a => a.Id.Trim() == Cdestino.ToString().Trim());
            if (resul == null)
            {
                return Json($"Esta cuenta no existe.");
            }
           
            return Json(true);
        }
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyMoney(decimal Monto, string Corigen)
        {

            var CuentaOrigen = Corigen.Substring(0, 9);
            var cuenta = await _productsrepository.GetbyIdnew(Convert.ToInt32(CuentaOrigen));
            if (cuenta.Balance >= Monto){
                return Json(true);


            }
          
            return Json($"No tienes suficiente balance para este pago. Tu balance en cuenta {cuenta.Balance.ToString("C")}");
        }
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyPay(decimal Monto, string Cdestino, string Corigen)
        {

            var CuentaCdestino = Cdestino.Substring(0, 9);
            var CuentaCorigen = Corigen.Substring(0, 9);

         var cuentadestino = await _productsrepository.GetbyIdnew(Convert.ToInt32(CuentaCdestino));
            var cuentaorigen = await _productsrepository.GetbyIdnew(Convert.ToInt32(CuentaCorigen));

            if (cuentadestino.Idtipo == 3 && cuentadestino.Balance == 0)
            {

                return Json($"La tarjeta de credito tiene balance {cuentadestino.Balance} a pagar");

            }

            if (cuentaorigen.Balance < Monto)
            {

                return Json($"No tienes suficiente balance para este pago. Tu balance en cuenta {cuentaorigen.Balance.ToString("C")}");

            }


            return Json(true);
        }
    }
}
