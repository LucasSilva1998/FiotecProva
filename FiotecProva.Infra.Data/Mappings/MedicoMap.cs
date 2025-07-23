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
    public class MedicoMap : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("Medicos");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.Nome)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(m => m.AnosExperiencia)
                   .IsRequired();

            builder.Property(m => m.DataNascimento)
                   .IsRequired();

            builder.Property(m => m.NumeroCRM)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(m => m.Especialidade)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.Property(m => m.CriadoEm)
                   .IsRequired();

            builder.HasOne(m => m.Usuario)
                    .WithOne(u => u.Medico)
                    .HasForeignKey<Medico>(m => m.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.Consultas)
                   .WithOne(c => c.Medico)
                   .HasForeignKey(c => c.MedicoId);

            builder.HasMany(m => m.HorariosAtendimento)
                   .WithOne()
                   .HasForeignKey("MedicoId")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}