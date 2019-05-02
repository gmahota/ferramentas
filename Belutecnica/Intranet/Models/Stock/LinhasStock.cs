using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Models.Stock
{
    public class LinhasStock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(48)]
        public string artigo { get; set; }

        [StringLength(50)]
        public string descricao { get; set; }

        [StringLength(15)]
        public string areaNegocio { get; set; }

        [StringLength(15)]
        public string projecto { get; set; }

        public string notas { get; set; }

        [StringLength(30)]
        public string codbarrasCabec { get; set; }
        
        public double quantidade { get; set; }

        public double quantTrans { get; set; }

        public double quantPendente { get; set; }
        
        public int idLinhaOrigem { get; set; }

        public int idDocumentoOrigem { get; set; }

        public StockStatus status { get; set; }

        public int CabecStockId { get; set; }
        
    }

    public class ViewLinhasStock{
        public bool sel {get;set;} = false;
        public int id { get; set; }

        public DateTime data {get;set;}

        public string documento {get;set;}

        public string funcionario{get;set;}

        public string nome{get;set;}

        public string nrDocExterno { get; set; }
        
        public string artigo { get; set; }
        public string descricao { get; set; }
        public string codbarrasCabec { get; set; }
        public double quantidade { get; set; }
        public double quantPendente { get; set; }
        public double quantTrans { get; set; }
        public string notas { get; set; }

        public string areaNegocio { get; set; }

        public string projecto { get; set; }

        public StockStatus status { get; set; }

        public int CabecStockId { get; set; }
    }

}
