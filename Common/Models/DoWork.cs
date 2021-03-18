using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [Table("DoWork")]
    public class DoWork
    {
        public bool EstaBorrado { get; set; }
        public string Evento { get; set; }
        public DateTime Fecha { get; set; }
    }
}
