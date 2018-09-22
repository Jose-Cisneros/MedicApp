using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using medic.Data.Context;
using medic.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using medic.Models;

namespace medic.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly MedicContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ConsultasController(MedicContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }

        [Authorize]
        // GET: Consultas
        public async Task<IActionResult> Index()
        {
            var medicContext = _context.Consultas.Include(c => c.Medico).Include(c => c.Paciente);

            var userId = _userManager.GetUserId(HttpContext.User);

            return View(await medicContext.Where( g => g.OwnerID == userId).ToListAsync());
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .SingleOrDefaultAsync(m => m.ConsultaID == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consultas/Create
        public IActionResult Create()
        {
            
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "Nombre" );
            return View();
        }

        // POST: Consultas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultaID,Fecha,Observacion,Estado,PacienteID")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                consulta.OwnerID = userId;
                consulta.MedicoID = userId;
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "PacienteID", consulta.PacienteID);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas.SingleOrDefaultAsync(m => m.ConsultaID == id);
            if (consulta == null)
            {
                return NotFound();
            }
            ViewData["MedicoID"] = new SelectList(_context.Medicos, "MedicoID", "MedicoID", consulta.MedicoID);
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "PacienteID", consulta.PacienteID);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(String id, [Bind("ConsultaID,Fecha,Observacion,Estado,MedicoID,PacienteID")] Consulta consulta)
        {
            if (id != consulta.ConsultaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.ConsultaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoID"] = new SelectList(_context.Medicos, "MedicoID", "MedicoID", consulta.MedicoID);
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "PacienteID", consulta.PacienteID);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .SingleOrDefaultAsync(m => m.ConsultaID == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(String id)
        {
            var consulta = await _context.Consultas.SingleOrDefaultAsync(m => m.ConsultaID == id);
            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(String id)
        {
            return _context.Consultas.Any(e => e.ConsultaID == id);
        }
    }
}
