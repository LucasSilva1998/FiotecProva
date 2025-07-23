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
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("Perfis");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
           .ValueGeneratedOnAdd();

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(p => p.Usuarios)
                   .WithOne(u => u.Perfil)
                   .HasForeignKey(u => u.PerfilId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}