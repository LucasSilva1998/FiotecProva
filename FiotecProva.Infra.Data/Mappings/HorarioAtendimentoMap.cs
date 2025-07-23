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
    public class HorarioAtendimentoMap : IEntityTypeConfiguration<HorarioAtendimento>
    {
        public void Configure(EntityTypeBuilder<HorarioAtendimento> builder)
        {
            builder.ToTable("HorariosAtendimento");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id)
           .ValueGeneratedOnAdd();

            builder.Property(h => h.HoraInicio)
                   .IsRequired();

            builder.Property(h => h.HoraFim)
                   .IsRequired();

            builder.HasOne(h => h.Medico)
                   .WithMany(m => m.HorariosAtendimento)
                   .HasForeignKey(h => h.MedicoId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}