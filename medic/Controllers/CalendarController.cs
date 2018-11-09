using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medic.Data.Context;
using medic.Data.Model;
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

            return Json(View(await medicContext.Where(g => g.MedicoID == userId || g.PacienteID == userId).ToListAsync()));

        }

        public async Task<JsonResult> GetCalendarDatasource()
        {
            var medicContext = _context.Consultas.Include(c => c.Medico).Include(c => c.Paciente);

            var userId = _userManager.GetUserId(HttpContext.User);
            var consults = await medicContext.Where(g => g.OwnerID == userId || g.PacienteID == userId).ToListAsync();
            var eventList = new List<EventSource>();
            foreach ( var c in consults)
            {
                eventList.Add(c.ToEventSource());
            }

            return Json(eventList);


        }

        public async Task<ActionResult> PartialConsults(String date)
        {
      
            var newDate = DateTime.Parse(date);
            var userId = _userManager.GetUserId(HttpContext.User);
            var list = await _context.Consultas.Where(g => (g.OwnerID == userId || g.PacienteID == userId) && g.Fecha.Date == newDate).ToListAsync();
            return PartialView(list);
        }
    }
}