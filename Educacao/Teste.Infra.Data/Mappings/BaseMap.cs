using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Teste.Domain.Entities;

namespace Teste.Infra.Data.Mappings
{
    public class BaseMap<T> : IEntityTypeConfiguration<T>
               where T : BaseEntitie
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.Timestamp).IsConcurrencyToken(true).ValueGeneratedOnAddOrUpdate();
        }
    }
}
