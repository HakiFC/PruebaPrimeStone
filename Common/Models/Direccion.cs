using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    [Table("Direccion")]
    public class Direccion : Entidad
    {
        [Column("Direccion")]
        public string StringDireccion { get; set; }
        public TipoDireccion TipoDireccion { get; set; }
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

    }
}