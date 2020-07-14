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
        private readonly AdminRepository _repository;




        public AdminController (UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> role, IMapper mapper, AdminRepository repository)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _role = role;
            this._mapper = mapper;
            this._repository = repository;

        }
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Management()
        {
            var vm = await _repository.GetAll();
            List< ManagementViewModel> usuarioentity = new List<ManagementViewModel>();
            foreach (var ua in vm){

                usuarioentity.Add(_mapper.Map<ManagementViewModel>(ua));
            }

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
                var user12 = await _repository.GetAll();
                var resul12 = user12.FirstOrDefault(a => a.Usuario.Trim() == usuarioentity.Usuario.Trim());

                if (resul.Succeeded)
                {
                   
                    var resulrol = await _userManager.AddToRoleAsync(user, vm.RoleSelect);
                    if (resulrol.Succeeded)
                    {
                        await _repository.Add(usuarioentity);
                        return RedirectToAction("Management");
                    }
                }
            }
             return View();
        }
        public IActionResult Edit()
        {
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyUser(string Usuario)
        {
       
            var user = await _repository.GetAll();
            var resul = user.FirstOrDefault(a => a.Usuario.Trim() == Usuario.Trim());
            if (resul != null)
            {
                return Json($"El Usuaurio {Usuario} ya esta en uso.");
            }

            return Json(true);
        }



    }
}
