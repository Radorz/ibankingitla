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
    public class ClienteController : Controller
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


        public ClienteController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
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
        public async Task<IActionResult> Index()
        {
            var vm = new ClienteViewModel();
            var user = User.Identity.Name;
            var id = await _userManager.FindByNameAsync(user);

            vm.Ahorros = await _productsrepository.GetAllCuentas(id.Id);
            vm.Tarjetas = await _productsrepository.GetAllTarjetas(id.Id);
            vm.Prestamos = await _productsrepository.GetAllPrestamos(id.Id);

            foreach (var item in vm.Ahorros)
            {
                vm.BalanceCuentaAhorro = vm.BalanceCuentaAhorro + item.Balance;

            }
            foreach (var item in vm.Tarjetas)
            {
                vm.DisponibleTarjeta = vm.BalanceTarjeta + item.Balance;
                vm.BalanceTarjeta = vm.BalanceTarjeta + (item.LimiteTarjeta - item.Balance);

            }

            foreach (var item in vm.Prestamos)
            {
                vm.BalancePrestamo = vm.BalancePrestamo + (item.MontoPrestamo - item.Balance);

            }
            return View(vm);
        }
        public async Task<IActionResult> Beneficiarios()
        {
            var usercreador = await _userManager.FindByNameAsync(User.Identity.Name);
            var beneficiarios = await _beneficiariosrepository.GetAll();
            BeneficiariosViewModel vm = new BeneficiariosViewModel();
            vm.Beneficiarios =  beneficiarios.Where(A => A.Idcreador.Trim() == usercreador.Id.Trim()).ToList();
            return View(vm);
        }
        public async Task<IActionResult> PagoExpress()
        {
            var users = await _userManager.FindByNameAsync(User.Identity.Name);

            var Cuentas = await _productsrepository.GetAllCuentas(users.Id.Trim());



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

        [HttpPost]
             public async Task<IActionResult> Addbenefi(BeneficiariosViewModel vm)
        {
            var usercreador = await _userManager.FindByNameAsync(User.Identity.Name);
            var account = await _productsrepository.GetbyIdnew(vm.newbeneficiario);
            var dueño = await _adminrepository.GetbyIdNew(account.Idusuario.Trim());
            var beneficiario = new Beneficiarios();
            beneficiario.Nombre = dueño.Nombre.Trim();
            beneficiario.Apellido = dueño.Apellido.Trim();
            beneficiario.NoCuenta = account.Id.Trim();
            beneficiario.Idcreador = usercreador.Id.Trim();


            await _beneficiariosrepository.Add(beneficiario);

            return RedirectToAction("Beneficiarios");
        }
        

        [HttpPost]
             public async Task<IActionResult> deleteBenefi(int deletebeneficiario)
        {
            


            await _beneficiariosrepository.Delete(deletebeneficiario);

            return RedirectToAction("Beneficiarios");
        }
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyAccount(int newbeneficiario)
        {

            var account = await _productsrepository.GetAllCuentaForVerify();
            var resul = account.FirstOrDefault(a => a.Id.Trim() == newbeneficiario.ToString().Trim());
            if (resul == null)
            {
                return Json($"Esta cuenta no existe.");
            }
            var usercreador = await _userManager.FindByNameAsync(User.Identity.Name);
            var beneficiarios = await _beneficiariosrepository.GetAll();
            var Beneficiarios = beneficiarios.FirstOrDefault(A =>( A.Idcreador.Trim() == usercreador.Id.Trim()&& A.NoCuenta== newbeneficiario.ToString().Trim()));
           
            if (beneficiarios!= null)
            {
                return Json($"Ya tienes esta cuenta agregada.");


            }
            return Json(true);
        }
    }
}
