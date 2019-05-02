using System;
using System.Collections.Generic;
using System.Text;
using Intranet.Models.Stock;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Intranet.Models;

namespace Intranet.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region GeneralSeed
            builder.Entity<TipoDocumentoStock>().HasData(
                new TipoDocumentoStock { tipo = "DF", descricao = "Devolução de Ferramentas", documento= "DF" },
                new TipoDocumentoStock { tipo = "SF", descricao = "Saida de Ferramentas", documento = "SF" },
                new TipoDocumentoStock { tipo = "DC", descricao = "Devolução de Consumiveis",documento = "DC" },
                new TipoDocumentoStock { tipo = "SC", descricao = "Saida de Consumiveis", documento = "SC" }
            );
            

            #endregion
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        
        public DbSet<Artigo> Artigos { get; set; }

        public DbSet<Funcionarios> Funcionarios { get; set; }

        public DbSet<Intranet.Models.Stock.TipoDocumentoStock> TipoDocumentoStock { get; set; }

        public DbSet<Intranet.Models.Stock.CabecStock> CabecStock { get; set; }

        public DbSet<Intranet.Models.Stock.LinhasStock> LinhasStock { get; set; }

        public DbSet<Intranet.Models.Stock.Projeto> Projeto { get; set; }

        public DbSet<Intranet.Models.Stock.Filial> Filial { get; set; }
    }
}
