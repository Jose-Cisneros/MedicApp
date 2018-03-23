using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace medic.Models.AppViewModels
{
    public class ConsultaViewModel
    {
        public DateTime Fecha { get; set; }

        public String Observacion { get; set; }

        public PacienteViewModel Paciente { get; set; }


    }
}
