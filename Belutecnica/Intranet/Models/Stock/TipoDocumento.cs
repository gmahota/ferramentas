using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Models.Stock
{
    public class TipoDocumentoStock
    {
        [Key]
        [StringLength(5)]
        public string documento { get; set; }

        [StringLength(5)]
        public string tipo { get; set; }

        [StringLength(30)]
        public string descricao { get; set; }
    }

    public enum TipoDocumentoStockTypes
    {
        Entrada,
        Saida,
        Iventario
    }
}
