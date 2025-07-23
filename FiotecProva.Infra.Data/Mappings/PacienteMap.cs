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
    public class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacientes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.OwnsOne(p => p.Cpf, cpf =>
            {
                cpf.Property(c => c.Numero)
                   .HasColumnName("Cpf")
                   .IsRequired()
                   .HasMaxLength(14);
            });

            builder.Property(p => p.DataNascimento)
                   .IsRequired();

            builder.Property(p => p.Telefone)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.OwnsOne(p => p.Endereco, endereco =>
            {
                endereco.Property(e => e.Logradouro).HasColumnName("Logradouro").HasMaxLength(100);
                endereco.Property(e => e.Numero).HasColumnName("Numero").HasMaxLength(10);
                endereco.Property(e => e.Cep).HasColumnName("Cep").HasMaxLength(10);
                endereco.Property(e => e.Municipio).HasColumnName("Municipio").HasMaxLength(100);
                endereco.Property(e => e.Uf).HasColumnName("Uf").HasMaxLength(2);
            });

            builder.Property(p => p.CriadoEm)
                   .IsRequired();

            builder.HasOne(p => p.Usuario)
                    .WithOne(u => u.Paciente)
                    .HasForeignKey<Paciente>(p => p.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}