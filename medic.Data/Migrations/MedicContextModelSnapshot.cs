﻿// <auto-generated />
using medic.Data.Context;
using medic.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace medic.Data.Migrations
{
    [DbContext(typeof(MedicContext))]
    partial class MedicContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("medic.Data.Model.Consulta", b =>
                {
                    b.Property<string>("ConsultaID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Estado");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("MedicoID");

                    b.Property<string>("Observacion");

                    b.Property<string>("OwnerID");

                    b.Property<string>("PacienteID");

                    b.HasKey("ConsultaID");

                    b.HasIndex("MedicoID");

                    b.HasIndex("PacienteID");

                    b.ToTable("Consultas");
                });

            modelBuilder.Entity("medic.Data.Model.Medico", b =>
                {
                    b.Property<string>("MedicoID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DNI");

                    b.Property<string>("Direccion");

                    b.Property<string>("Especialidad");

                    b.Property<int>("Matricula");

                    b.Property<string>("Nombre");

                    b.Property<long>("Telefono");

                    b.HasKey("MedicoID");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("medic.Data.Model.Paciente", b =>
                {
                    b.Property<string>("PacienteID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DNI");

                    b.Property<string>("Nombre");

                    b.Property<string>("Observacion");

                    b.Property<int>("Telefono");

                    b.HasKey("PacienteID");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("medic.Data.Model.PeticionPacienteAMedico", b =>
                {
                    b.Property<string>("PeticionPacienteAMedicoID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("MedicoID");

                    b.Property<string>("MedicoNombre");

                    b.Property<string>("PacienteID");

                    b.Property<string>("PacienteNombre");

                    b.Property<bool>("visto");

                    b.HasKey("PeticionPacienteAMedicoID");

                    b.HasIndex("MedicoID");

                    b.HasIndex("PacienteID");

                    b.ToTable("PeticionPacienteAMedicos");
                });

            modelBuilder.Entity("medic.Data.Model.Consulta", b =>
                {
                    b.HasOne("medic.Data.Model.Medico", "Medico")
                        .WithMany("Consultas")
                        .HasForeignKey("MedicoID");

                    b.HasOne("medic.Data.Model.Paciente", "Paciente")
                        .WithMany("Consultas")
                        .HasForeignKey("PacienteID");
                });

            modelBuilder.Entity("medic.Data.Model.PeticionPacienteAMedico", b =>
                {
                    b.HasOne("medic.Data.Model.Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoID");

                    b.HasOne("medic.Data.Model.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteID");
                });
#pragma warning restore 612, 618
        }
    }
}
