using System;
using System.ComponentModel.DataAnnotations;

namespace Teste.Application.DTOs
{
    public abstract class BaseDTO
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Timestamp { get; set; }
    }
}
