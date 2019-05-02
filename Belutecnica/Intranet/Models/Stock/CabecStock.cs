using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Models.Stock
{
    public class CabecStock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int numDoc { get; set; }

        [StringLength(10)]
        public string serie { get; set; }

        [StringLength(5)]
        public string tipodoc { get; set; }

        [StringLength(5)]
        public string entradaSaida { get; set; }

        [StringLength(20)]
        public string funcionario { get; set; }

        [StringLength(50)]
        public string nome { get; set; }

        [StringLength(10)]
        public string armazem { get; set; }

        [StringLength(10)]
        public string localizacao { get; set; }

        [StringLength(10)]
        public string nrDocExterno { get; set; }

        [StringLength(15)]
        public string areaNegocio { get; set; }

        [StringLength(15)]
        public string projecto { get; set; }

        public string notas { get; set; }

        public DateTime data { get; set; }
        
        public bool integradoErp { get; set; }

        public StockStatus status { get; set; }

        public HashSet<LinhasStock> linhas { get; set; } = new HashSet<LinhasStock>();

        [ForeignKey("tipodoc")]
        public virtual TipoDocumentoStock Documento { get; set; }
        
    }

    public enum StockStatus
    {
        Aberto,
        Fechado,
        Cancelado
    }

    //public class Cabec
    //{
    //    public string tipodoc { get; set; }

    //    public string funcionario { get; set; }

    //    public string nome { get; set; }

    //    public string projecto { get; set; }

    //    public DateTime data { get; set; }

    //    //public string notas { get; set; }
        
    //    public List<Linhas> linhas { get; set; }
    //}

    //public class Linhas
    //{

    //    public string artigo { get; set; }
    //    public string descricao { get; set; }
    //    public string codbarrasCabec { get; set; }
    //    public double quantidade { get; set; }
    //    public string notas { get; set; }
        
    //}
}
