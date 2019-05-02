using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Models.Stock
{
    public class Artigo
    {
        [Key]
        [StringLength(48)]
        public string artigo { get; set; }

        [StringLength(50)]
        public string descricao { get; set; }

        [StringLength(30)]
        public string codbarrasartigo { get; set; }

        [StringLength(10)]
        public string armazem { get; set; }

        public double stkActual { get; set; }
    }
}
