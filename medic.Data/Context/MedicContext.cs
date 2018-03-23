using Microsoft.EntityFrameworkCore;
using medic.Data.Model;


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

    }
}
