using Intranet.Models.Geral;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Models.Stock
{
    public class Filial: INetcoreBasic,IBaseMorada
    {
        public Filial()
        {
            this.dataCriacao = DateTime.UtcNow;
            this.porDefeito = false;
        }

        [StringLength(38)]
        [Display(Name = "Branch Id")]
        [Key]
        public string filialId { get; set; }

        [StringLength(50)]
        [Display(Name = "Nome")]
        [Required]
        public string nome { get; set; }

        [StringLength(50)]
        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        [Display(Name = "Por Defeito ?")]
        public bool porDefeito { get; set; } = false;

        //IBaseAddress
        [Display(Name = "Morada 1")]
        [Required]
        [StringLength(50)]
        public string morada1 { get; set; }

        [Display(Name = "Morada 2")]
        [StringLength(50)]
        public string morada2 { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(30)]
        public string cidade { get; set; }

        [Display(Name = "Provincia")]
        [StringLength(30)]
        public string provincia { get; set; }

        [Display(Name = "Pais")]
        [StringLength(30)]
        public string pais { get; set; }
    }
}