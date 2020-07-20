using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using ViewModels;

namespace Ibanking_Itla.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _role;
        private readonly AdminRepository _adminrepository;


        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> role, AdminRepository adminrepository)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _role = role;
            this._adminrepository = adminrepository;

        }
        [HttpGet]
        public async Task <IActionResult> Login()
        {
            ViewBag.mostrar = "none";
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {

                    return RedirectToAction("Index", "Admin");

                }
                if (await _userManager.IsInRoleAsync(user, "Cliente"))
                {

                    return RedirectToAction("Index", "Cliente");

                }
                
            }
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Login(LoginViewModel vm)
        {
            ViewBag.mostrar = "none";

            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(vm.User, vm.Password, false, true);
                if (resultado.IsLockedOut)
                {

                    ModelState.AddModelError("Error", "Esta cuenta ha sido Inactivada");
                    return View(vm);
                }

                if (resultado.Succeeded)
                {
                   
                    var  user = await _userManager.FindByNameAsync(vm.User);
                    if(await _userManager.IsInRoleAsync(user, "Cliente"))
                    {

                      return RedirectToAction("Index", "Cliente");

                    }
                   if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {

                        return RedirectToAction("Index", "Admin");

                    }
                 }
                var userforlock = await _userManager.FindByNameAsync(vm.User);
                if (userforlock.AccessFailedCount == 3 && await _userManager.IsInRoleAsync(userforlock, "Cliente"))
                {

                    await _userManager.SetLockoutEndDateAsync(userforlock, DateTime.FromOADate(365 * 200));
                    await _userManager.ResetAccessFailedCountAsync(userforlock);
                    var state = await _adminrepository.GetbyIdNew(userforlock.Id);
                    state.Estado = "Inactivo";
                    await _adminrepository.Update(state);

                }
 
                 ModelState.AddModelError("UserOrPasswordInvalid", "El usuario o contraseña es invalido");
            }
            return View(vm);
        }
        public async Task<IActionResult> AccessDenied()
        {

                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    if (await _userManager.IsInRoleAsync(user, "Cliente"))
                    {

                        return RedirectToAction("Index", "Cliente");

                    }
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {

                        return RedirectToAction("Index", "Admin");

                    }
            return RedirectToAction("Login", "Account");

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
