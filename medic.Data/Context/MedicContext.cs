using Microsoft.EntityFrameworkCore;
using medic.Data.Model;
using System;
using System.Collections.Generic;

namespace medic.Data.Context
{
    public class MedicContext : DbContext
    {
        public MedicContext(DbContextOptions<MedicContext> options)
           : base(options)
        { }


        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

        public static implicit operator MedicContext(List<Medico> v)
        {
            throw new NotImplementedException();
        }

        public object Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }

        public object Where(Func<object, bool> p, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
