using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace medic.Data.Model
{
    public class Paciente
    {


        public String PacienteID { get; set; }
        public String Nombre { get; set; }
        public int DNI { get; set; }
        public int Telefono { get; set; }
        public String Observacion { get; set; }
        public String Email { get; set; }

        public virtual ICollection<Consulta> Consultas { get; set; }
       


    }
}
