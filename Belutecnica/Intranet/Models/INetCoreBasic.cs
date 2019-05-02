using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Models
{
    public class INetcoreBasic
    {
        [Display(Name = "Criado Em")]
        public DateTime dataCriacao { get; set; } = DateTime.UtcNow;
    }
}

