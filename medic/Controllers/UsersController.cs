using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace medic.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public UsersController( UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            String tipo = user.Type.ToString();
            switch (tipo)
            {
                case "admin":
                    return RedirectToAction("Admin");
                    
                case "medico":
                   return RedirectToAction("Medico");
                    
                case "paciente":
                    return RedirectToAction("Paciente");
                   
                default:
                    return StatusCode(400);

            }
            
        }

        public async Task<IActionResult> Medico()
        {
            var user = await _userManager.GetUserAsync(User);
            String tipo = user.Type.ToString();

            if (tipo != "medico") return StatusCode(400);
            return View();
        }

        public async Task<IActionResult> Paciente()
        {

            var user = await _userManager.GetUserAsync(User);
            String tipo = user.Type.ToString();

            if (tipo != "paciente") return StatusCode(400);
            return View();
        }

        public async Task<IActionResult> Admin()
        {
            var user = await _userManager.GetUserAsync(User);
            String tipo = user.Type.ToString();

            if (tipo != "admin") return StatusCode(400);
            return View();
        }
    }
}