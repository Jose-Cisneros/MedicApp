using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using medic.Data.Context;
using medic.Data.Model;

namespace medic.Controllers
{
    public class MedicosController : Controller
    {
        private readonly MedicContext _context;

        public MedicosController(MedicContext context)
        {
            _context = context;
        }

        // GET: Medicos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicos.ToListAsync());
        }
        public async Task<IActionResult> GetForSpecialidad(String especialidad)
        {
            var esp = especialidad;
            var medicos =  _context.Medicos;
            var especialistas = await medicos.Where(g => g.Especialidad == especialidad).ToListAsync();
         return   View(especialistas);


        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .SingleOrDefaultAsync(m => m.MedicoID == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicoID,Nombre,DNI,Matricula,Especialidad")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.SingleOrDefaultAsync(m => m.MedicoID == id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(String id, [Bind("MedicoID,Nombre,DNI,Matricula,Especialidad")] Medico medico)
        {
            if (id != medico.MedicoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.MedicoID))
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
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .SingleOrDefaultAsync(m => m.MedicoID == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(String id)
        {
            var medico = await _context.Medicos.SingleOrDefaultAsync(m => m.MedicoID == id);
            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult GetAllDoctors()
        {
            return View();
        }

        private bool MedicoExists(string id)
        {
            return _context.Medicos.Any(e => e.MedicoID == id);
        }
    }
}
