using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intranet.Data;
using Intranet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Controllers
{
    
    public class IventarioController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INetcoreService _netcore;

        public IventarioController(ApplicationDbContext context, INetcoreService netcore)
        {
            _context = context;
            _netcore = netcore;
        }

        [HttpGet]
        public JsonResult ListaArtigos(string tipo)
        {
            try
            {

                var strSql =string.Format(
                    @"
                        select artigo,descricao,CodBarras as codbarrasartigo, 'A' as armazem ,stkActual 
                          
                        from {0}.dbo.Artigo a with(nolock)
                        where a.TipoArtigo = '{1}'
                    ",_netcore._Primavera.database,tipo)
                    ;

                var listaArtigos = _context.Artigos.FromSql(strSql);

                return Json(listaArtigos);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet]
        public JsonResult ListaFuncionarios()
        {
            try
            {
                var strSql =
                    string.Format(
                    @"select f.Codigo,f.Nome,isnull(f.CDU_CodigoBarras,'') as CDU_CodigoBarras , 
                    isnull(fc.Ccusto,'') as Ccusto 
                        from {0}.dbo.Funcionarios f with(nolock)
                        left outer join {0}.dbo.funcccusto fc with(nolock) on fc.funcionario = f.codigo 
                            and ano = year(getdate()) and mesfiscal = month(getdate()) 
                            and fc.principal = 1", _netcore._Primavera.database)
                    ;

                var listaFuncionarios = _context.Funcionarios.FromSql(strSql);

                return Json(listaFuncionarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public JsonResult ListaProjetos()
        {
            try
            {
                var strSql =
                    string.Format(
                    "select [ID],[Codigo],[Descricao] from {0}.dbo.COP_Obras", 
                        _netcore._Primavera.database)
                    ;

                var listaProjeto = _context.Projeto.FromSql(strSql);

                return Json(listaProjeto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public JsonResult ListaProjetos_CentroCusto(string areaNegocio)
        {
            try
            {
                var strSql =
                    string.Format("select [ID],[Codigo],[Descricao] from {0}.dbo.COP_Obras",
                        _netcore._Primavera.database)
                    ;

                var listaProjeto = _context.Projeto.FromSql(strSql);

                return Json(listaProjeto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public JsonResult ListaNegocio_CentroCusto()
        {
            try
            {
                var strSql = string.Format(@"select Centro as codigo, descricao from PRIBELUAGRO.dbo.planocentros
                    where TipoConta = 'M' and Inactivo = 0 and Ano = YEAR(GetDate())",
                        _netcore._Primavera.database)
                    ;

                var listaProjeto = _context.Projeto.FromSql(strSql);

                return Json(listaProjeto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Iventario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Iventario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Iventario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Iventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Iventario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Iventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Iventario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Iventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}