using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace medic.Data.Model
{
    public class Consulta
    {
        public string OwnerID { get; set; }

        public String ConsultaID { get; set; }
        public DateTime Fecha { get; set; }
        public String Observacion { get; set; }

        [EnumDataType(typeof(Estado))]
        public Estado Estado { get; set; }


        public String MedicoID { get; set; }
        public Medico Medico { get; set; }

        public String PacienteID { get; set; }
        public Paciente Paciente { get; set; }


    }

    public enum Estado
    {
        Pendiente = 0,
        Abierta = 1,
        Cerrada = 2,
        Cancelada = 3,

    }
}
