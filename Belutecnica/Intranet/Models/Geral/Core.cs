using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Models.Geral
{
    public interface IBaseMorada
    {
        //IBaseAddress
        [Display(Name = "Morada 1")]
        [Required]
        [StringLength(50)]
        string morada1 { get; set; }

        [Display(Name = "Morada 2")]
        [StringLength(50)]
        string morada2 { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(30)]
        string cidade { get; set; }

        [Display(Name = "Provincia")]
        [StringLength(30)]
        string provincia { get; set; }

        [Display(Name = "Pais")]
        [StringLength(30)]
        string pais { get; set; }
    }
}
