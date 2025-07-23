using FiotecProva.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Infra.Data.Mappings
{
    public class ConsultaMap : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("Consultas");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
           .ValueGeneratedOnAdd();

            builder.Property(c => c.DataHoraConsulta)
                   .IsRequired();

            builder.Property(c => c.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(c => c.JustificativaCancelamento)
                   .HasMaxLength(500)
                   .IsUnicode(true)
                   .IsRequired(false);

            builder.Property(c => c.CriadoEm)
                   .IsRequired();

            builder.HasOne(c => c.Medico)
                    .WithMany(m => m.Consultas)
                    .HasForeignKey(c => c.MedicoId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Paciente)
                    .WithMany(p => p.Consultas)
                    .HasForeignKey(c => c.PacienteId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}