using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Models.Stock
{
    public class Projeto
    {
        [Key]
        public string codigo { get; set; }

        public string descricao { get; set; }
        
    }
}
