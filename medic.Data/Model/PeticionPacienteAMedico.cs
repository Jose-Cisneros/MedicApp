using System;
using System.Collections.Generic;
using System.Text;

namespace medic.Data.Model
{
   public class PeticionPacienteAMedico
    {

        public String PeticionPacienteAMedicoID { get; set; }

        public String MedicoID { get; set; }
        public Medico Medico { get; set; }

        public String PacienteID { get; set; }
        public Paciente Paciente { get; set; }

        public DateTime Fecha { get; set; }


        public Boolean visto { get; set; }

    }
}
