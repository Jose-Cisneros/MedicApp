using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace medic.Data.Model
{
     public class Medico
    {
       
        public String MedicoID { get; set; }
        public String Nombre { get; set; }
        public int DNI { get; set; }
        public int Matricula { get; set; }

        public virtual ICollection<Consulta> Consultas { get; set; }


    }
}
