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
    [Authorize(Roles = "admin")]

    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _role;
        private readonly IMapper _mapper;
        private readonly AdminRepository _adminrepository;
        private readonly ProductosRepository _productsrepository;
        private readonly PagosRepository _pagosrepository;
        private readonly TransferenciasRepository _transrepository;



        public AdminController (UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> role, IMapper mapper, AdminRepository adminrepository, ProductosRepository productsrepository, PagosRepository pagosrepository,
            TransferenciasRepository transrepository)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _role = role;
            this._mapper = mapper;
            this._adminrepository = adminrepository;
            this._productsrepository = productsrepository;
            this._pagosrepository = pagosrepository;
            this._transrepository = transrepository;




        }
        public async Task<IActionResult> Index()
        {
            var vm = new AdminViewModel();
           vm.ProductosAsignados= await _productsrepository.GetCOUNT();
            vm.ClientesActivos = await _adminrepository.GetCOUNTactive();
            vm.ClientesInactivos = await _adminrepository.GetCOUNTinactive();
            vm.Pagoshoy = await _pagosrepository.GetCOUNTtoday();
            vm.PagosTotales = await _pagosrepository.GetCOUNTall();
            vm.Transaccioneshoy = await _transrepository.GetCOUNTtoday();
            vm.Transaccionestotal = await _transrepository.GetCOUNTall();
            return View(vm);
        }
        
        public async Task<IActionResult> Management()
        {
            var vm = await _adminrepository.GetAll();
             ManagementViewModel usuarioentity = new ManagementViewModel();
            foreach(var user in vm)
            {


            }
                usuarioentity.list =vm;
            

            return View(usuarioentity);
        }
        public async Task <IActionResult> Create()
        {

            var vm = new NewAccountViewModel
            {
                Roles = _role.Roles.Select(s => s.Name).ToList()
            };
            return View(vm);
        } 
        [HttpPost]
        public async Task <IActionResult>Create(NewAccountViewModel vm )
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = vm.Usuario, Email = vm.Correo };
                var resul = await _userManager.CreateAsync(user, vm.Contraseña);
                var usuarioentity = _mapper.Map<Users>(vm);
                usuarioentity.Tipo = vm.RoleSelect;
                usuarioentity.Estado = "Activo";
                usuarioentity.Id = user.Id;
                
                 

                if (resul.Succeeded)
                {
                   
                    var resulrol = await _userManager.AddToRoleAsync(user, vm.RoleSelect);
                    if (resulrol.Succeeded)
                    {
                        
                        await _adminrepository.Add(usuarioentity);
                        if (vm.RoleSelect.Equals("cliente")){

                            var productentity = new ProductosUsers();
                            productentity.Id = DateTime.Now.ToString("HHyfffmm");
                            productentity.Idusuario = user.Id;
                            productentity.Idtipo = 1;
                            productentity.tipo = "Principal";

                            await _productsrepository.Add(productentity);


                        }
                        return RedirectToAction("Management");
                    }
                }
            }
             return View();
        }
        public async Task<IActionResult> Edit(string id)
        {
           var user= await _adminrepository.GetbyIdNew(id);
            var vm = _mapper.Map<EditViewModel>(user);
            vm.tipouser = user.Tipo.Trim();
            vm.RepeatContraseña = user.Contraseña.Trim();
            vm.Ahorros = await _productsrepository.GetAllCuentas(id);
            vm.Tarjetas = await _productsrepository.GetAllTarjetas(id);
            vm.Prestamos = await _productsrepository.GetAllPrestamos(id);

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
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {

            await _adminrepository.Deletenew(id);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> statechange(string id)

        {
            var user = await _userManager.FindByIdAsync(id);
            if (await _userManager.IsLockedOutAsync(user))
            {
                await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.UtcNow));


            }
            await _userManager.SetLockoutEndDateAsync(user, DateTime.FromOADate(365*200));

            await _adminrepository.statechange(id);

            return RedirectToAction("Management");
        }
        [HttpPost]
        public async Task<IActionResult> addahorros(string id)

        {
            var productentity = new ProductosUsers();
            productentity.Id = DateTime.Now.ToString("HHyfffmm");
            productentity.Idusuario = id;
            productentity.Idtipo = 1;
            productentity.tipo = "Secundaria";
            productentity.Balance = 0;
            productentity.LimiteTarjeta = 0;
            productentity.MontoPrestamo = 0;

            await _productsrepository.Add(productentity);

            return RedirectToAction("Edit", new { id = id });
        }
        [HttpPost]
        public async Task<IActionResult> AddTarjeta(string id, decimal LimiteNewTarjeta)

        {
            var productentity = new ProductosUsers();
            productentity.Id = DateTime.Now.ToString("HHyfffmm");
            productentity.Idusuario = id;
            productentity.Idtipo = 3;
            productentity.tipo = "Credito";
            productentity.Balance = 0;
            productentity.LimiteTarjeta = LimiteNewTarjeta;
            productentity.MontoPrestamo = 0;

            await _productsrepository.Add(productentity);

            return RedirectToAction("Edit", new { id = id });
        }
        [HttpPost]
        public async Task<IActionResult> AddPrestamo(string id, decimal MontonewPrestamo)

        {
            var productentity = new ProductosUsers();
            productentity.Id = DateTime.Now.ToString("HHyfffmm");
            productentity.Idusuario = id;
            productentity.Idtipo = 2;
            productentity.tipo = "Deuda";
            productentity.Balance = 0;
            productentity.LimiteTarjeta = 0;
            productentity.MontoPrestamo = MontonewPrestamo;

            await _productsrepository.Add(productentity);

            return RedirectToAction("Edit", new { id = id });
        }
        [HttpPost]
        public  async Task<IActionResult> deleteProduct(string id)

        {

            var delete = await _productsrepository.DeleteNew(id);
            //await _productsrepository.DeleteNew(id);

            return RedirectToAction("Edit", new { id = delete.Idusuario });
            
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyUser(string Usuario)
        {
       
            var user = await _adminrepository.GetAll();
            var resul = user.FirstOrDefault(a => a.Usuario.Trim() == Usuario.Trim());
            if (resul != null)
            {
                return Json($"El Usuaurio {Usuario} ya esta en uso.");
            }

            return Json(true);
        }



    }
}
