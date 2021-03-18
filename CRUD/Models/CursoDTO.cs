using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Models
{
    [Table("Curso")]
    public class CursoDTO
    {
        public int Id { get; set; }
        [Display(Name = "Código del curso")]
        public string CodigoCurso { get; set; }
        [Display(Name = "Nombre del curso")]
        public string NombreCurso { get; set; }
        [Display(Name = "Fecha de inicio")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime FechaInicio { get; set; }
        [Display(Name = "Fecha de finalización")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime FechaFin { get; set; }
    }
}
