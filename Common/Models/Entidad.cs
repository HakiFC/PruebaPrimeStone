using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public abstract class Entidad
    {
        [Key]
        public int Id { get; set; }
        public bool EstaBorrado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaBorrado { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
