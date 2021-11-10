using System;
using System.ComponentModel.DataAnnotations;

namespace Teste.Domain.Entities
{
    public abstract class BaseEntitie
    {
        [Key]
        public int Id { get; set; }

        [Timestamp]
        public DateTime Timestamp { get; set; }
    }
}