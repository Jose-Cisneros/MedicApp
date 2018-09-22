using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medic.Data.Context;
using medic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace medic.Controllers
{
    public class CalendarController : Controller

    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly MedicContext _context;

        public CalendarController(MedicContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetConsults()
        {

            var medicContext = _context.Consultas.Include(c => c.Medico).Include(c => c.Paciente);

            var userId = _userManager.GetUserId(HttpContext.User);

            return Json(View(await medicContext.Where(g => g.OwnerID == userId).ToListAsync()));

        }
    }
}