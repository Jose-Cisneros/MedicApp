using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using medic.Data.Context;
using medic.Data.Model;
using Microsoft.AspNetCore.Identity;
using medic.Models;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace medic.Controllers
{
    public class MedicosController : Controller
    {
        private readonly MedicContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public MedicosController(MedicContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;


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
        public async Task<IActionResult> Edit(String id, [Bind("MedicoID,Nombre,DNI,Matricula,Especialidad,Telefono,Direccion")] Medico medico)
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
                return RedirectToAction("Index","Users");
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

        public async Task<IActionResult> GetNotificaciones()
        {
            List<PeticionPacienteAMedico> listaNotif = new List<PeticionPacienteAMedico>();
            var peticiones = _context.PeticionPacienteAMedicos;
            var doctorId = _userManager.GetUserId(HttpContext.User);

            var notificaciones = await peticiones.Where(g => g.MedicoID == doctorId && g.visto == false ).ToListAsync();

            foreach (PeticionPacienteAMedico peticionPacienteAMedico in notificaciones )
            {
                var ppam = new PeticionPacienteAMedico();
                ppam.PacienteID = peticionPacienteAMedico.PacienteID;
                listaNotif.Add(ppam);

                peticionPacienteAMedico.visto = true;
                _context.Update(peticionPacienteAMedico);
                await _context.SaveChangesAsync();
            }

            

            return View(listaNotif);


        }

        public ActionResult GetAllDoctors()
        {
            return View();
        }

        public async Task<int> NotificationsQuantity()
        {

            var peticiones = _context.PeticionPacienteAMedicos;
            var doctorId = _userManager.GetUserId(HttpContext.User);
            var notificaciones = await peticiones.Where(g => g.MedicoID == doctorId && g.visto == false).ToListAsync();

           return notificaciones.Count;
        }

        private bool MedicoExists(string id)
        {
            return _context.Medicos.Any(e => e.MedicoID == id);
        }
        
        public async Task ConsultaConfirmationAsync(string pacienteId,string fecha)
        {
            
            var medicoId =  _userManager.GetUserId(HttpContext.User);
            var medicoNombre = _context.Medicos.Where(m => m.MedicoID == medicoId).First().Nombre;


            string body = string.Empty;

            using (StreamReader reader = new StreamReader("./wwwroot/Templates/emailConfirmation.html"))
            {
                body = reader.ReadToEnd();
                body = body.Replace("{UserName}", medicoNombre);
                body = body.Replace("{fecha}", fecha);


            }


            var asunto = "Confirmacion turno";
            var mensaje = "Se ha confirmado tu turno";

            var pacientes = _userManager.Users;
            var paciente = await pacientes.Where(p => p.Id == pacienteId).FirstAsync();

            var emailDestino = paciente.Email;
            var builder = new StringBuilder();

           
            var client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.live.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("juampi_csl@hotmail.com", "chumpoa1");
            MailMessage mm = new MailMessage("juampi_csl@hotmail.com", emailDestino, asunto, body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mm.Body = body;
            mm.IsBodyHtml = true;
            client.Send(mm);
   
        }
    }
}
