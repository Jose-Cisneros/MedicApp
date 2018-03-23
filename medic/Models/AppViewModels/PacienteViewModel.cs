using medic.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace medic.Models.AppViewModels
{
    public class PacienteViewModel
    {

        [Required]
        public String Nombre { get; set; }

        [Required]
        public int DNI { get; set; }

        [Phone]
        public int Telefono { get; set; }

        public String Observacion { get; set; }

        public PacienteViewModel(Paciente paciente)
        {
            Nombre = paciente.Nombre;
            DNI = paciente.DNI;
            Telefono = paciente.Telefono;
            Observacion = paciente.Observacion;
        }




    }
}
