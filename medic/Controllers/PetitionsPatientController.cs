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
    public class PetitionsPatientController : Controller
    {
        private readonly MedicContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PetitionsPatientController(MedicContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }

         public async Task<IActionResult> CreatePetition(String idMedico)

        {
            var medicos = _context.Medicos;
            var medicos2 = await medicos.Where(g => g.MedicoID == idMedico).ToListAsync();
            var medico = medicos2.First();

            PeticionPacienteAMedico peticionPacienteAMedico = new PeticionPacienteAMedico();
            peticionPacienteAMedico.MedicoID = idMedico;
            peticionPacienteAMedico.MedicoNombre = medico.Nombre;


            return View(peticionPacienteAMedico);
        }

    
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePetition(String MedicoID,DateTime Fecha)
        {
            PeticionPacienteAMedico peticionPacienteAMedico = new PeticionPacienteAMedico();

            if (ModelState.IsValid)
            {
                peticionPacienteAMedico.PacienteID = _userManager.GetUserId(HttpContext.User);
                peticionPacienteAMedico.MedicoID = MedicoID;
                peticionPacienteAMedico.Fecha = Fecha;
                peticionPacienteAMedico.visto = false;
                peticionPacienteAMedico.PeticionPacienteAMedicoID = Guid.NewGuid().ToString();
                

                _context.Add(peticionPacienteAMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(peticionPacienteAMedico);
        }
        
    }
    
}