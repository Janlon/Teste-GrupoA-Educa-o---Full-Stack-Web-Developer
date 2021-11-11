using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teste.Domain.Entities;

namespace Teste.Infra.Data.Mappings
{
    public class AlunoMap : BaseMap<Aluno>
    {
        public override void Configure(EntityTypeBuilder<Aluno> modelBuilder)
        {
            modelBuilder.ToTable("Alunos");

            modelBuilder.Property(t => t.Nome).IsRequired().HasMaxLength(300);
            modelBuilder.Property(t => t.CPF).IsRequired().HasMaxLength(14);
            modelBuilder.Property(t => t.Email).IsRequired().HasMaxLength(256);
            modelBuilder.Property(t => t.RA).IsRequired().HasMaxLength(14);

            modelBuilder.HasIndex(c => c.CPF).IsUnique(true).HasFilter(null);
            modelBuilder.HasIndex(c => c.Email).IsUnique(true).HasFilter(null);
            modelBuilder.HasIndex(c => c.RA).IsUnique(true).HasFilter(null);

            base.Configure(modelBuilder);
        }
    }
}
