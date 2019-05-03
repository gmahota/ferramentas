using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Models.Stock
{
    public class Funcionarios
    {
        [Key]
        public string codigo { get; set; }

        public string nome { get; set; }

        public string cdu_CodigoBarras { get; set; }

        public string ccusto { get; set; }

    }

    public class View_Funcionario_Ferramentas
    {
        

    }
}
