using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BServices.Entidades
{
    public class CabecStock
    {

        public string numDoc { get; set; }
        public string serie { get; set; }
        public string tipoDoc { get; set; }
        public string entradaSaida { get; set; }
        public string funcionario { get; set; }
        public string armazem { get; set; }
        public string localizacao { get; set; }
        
        public string nrDocExterno { get; set; }
        public string projecto { get; set; }
    
        public DateTime data { get; set; }

        public string notas { get; set; }

        public List<LinhasStock> linhas { get; set; }

    }

    public class LinhasStock
    {
        public string artigo { get; set; }
        public string descricao { get; set; }
        public string codbarrasCabec { get; set; }
        public string quantidade { get; set; }
        public string notas { get; set; }
    }
}
