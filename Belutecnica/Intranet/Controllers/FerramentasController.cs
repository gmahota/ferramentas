using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Data;
using Intranet.Models;
using Intranet.Models.Stock;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Controllers
{
    [Authorize]
    public class FerramentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FerramentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public JsonResult Lista(DatatableAjaxModel param)
        {
            IEnumerable<CabecStock> cabecDoc = _context.CabecStock.Include(z => z.linhas)
                .Where(c => c.status == StockStatus.Aberto && c.tipodoc == "SF");

            var totalCustomers = _context.CabecStock.Count();

            var sortDirection = HttpContext.Request.Query["sSortDir_0"]; // asc or desc
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);

            if (!string.IsNullOrEmpty(param.sSearch))
                cabecDoc = cabecDoc.Where(z =>(z.funcionario != null && z.funcionario.ToLower().Contains(param.sSearch.ToLower())) ||
                    (z.nome != null && z.nome.ToLower().Contains(param.sSearch.ToLower())) ||
                    z.linhas.Any(l => l.artigo.ToLower().Contains(param.sSearch.ToLower()))
                    );

            switch (sortColumnIndex)
            {
                case 1:
                    cabecDoc = sortDirection == "asc" ? cabecDoc.OrderBy(z => z.data) : cabecDoc.OrderByDescending(z => z.data);
                    break;
                case 2:
                    cabecDoc = sortDirection == "asc" ? cabecDoc.OrderBy(z => z.funcionario) : cabecDoc.OrderByDescending(z => z.funcionario);
                    break;
                case 3:
                    cabecDoc = sortDirection == "asc" ? cabecDoc.OrderBy(z => z.notas) : cabecDoc.OrderByDescending(z => z.notas);
                    break;
                default:
                    cabecDoc = cabecDoc.OrderBy(z => z.id);
                    break;


            }
            var filteredCustomersCount = cabecDoc.Count();
            cabecDoc = cabecDoc.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCustomers,
                iTotalDisplayRecords = filteredCustomersCount,
                aaData = cabecDoc
            });
        }
    
        public JsonResult Linhas(DatatableAjaxModel param)
        {
            try{
                string funcionario = param.sSearch.ToLower();
            
            IEnumerable<ViewLinhasStock> linhas = _context.LinhasStock
                .Where(p=>p.status == StockStatus.Aberto).Join(
                    _context.CabecStock.Where(c=>c.funcionario.ToLower().Equals(funcionario) && c.tipodoc == "SF"),
                    ls => ls.CabecStockId,
                    cs => cs.id,
                    (ls,cs)=> new ViewLinhasStock {
                        id = ls.id,
                        nrDocExterno = cs.nrDocExterno,
                        CabecStockId = cs.id,
                        data = cs.data,
                        artigo = ls.artigo,
                        descricao = ls.descricao,
                        quantidade = ls.quantidade,
                        quantPendente= ls.quantPendente,
                        quantTrans = ls.quantTrans,
                        notas = ls.notas,
                        funcionario = cs.funcionario,
                        status = ls.status
                    }
            )
            .ToList<ViewLinhasStock>();
            
            var totalCustomers = linhas.Count();
            
            var sortDirection = HttpContext.Request.Query["sSortDir_0"]; // asc or desc
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);

            switch (sortColumnIndex)
            {
                case 1:
                    linhas = sortDirection == "asc" ? linhas.OrderBy(z => z.data) : linhas.OrderByDescending(z => z.data);
                    break;
                case 2:
                    linhas = sortDirection == "asc" ? linhas.OrderBy(z => z.artigo) : linhas.OrderByDescending(z => z.artigo);
                    break;
                case 3:
                    linhas = sortDirection == "asc" ? linhas.OrderBy(z => z.descricao) : linhas.OrderByDescending(z => z.descricao);
                    break;
                default:
                    linhas = linhas.OrderBy(z => z.data);
                    break;


            }
            var filteredCustomersCount = linhas.Count();
            linhas = linhas.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCustomers,
                iTotalDisplayRecords = filteredCustomersCount,
                aaData = linhas
            });
            }catch(Exception ex){

                return Json(new{
                    sEcho = "",
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<ViewLinhasStock> ()
                });
            }
            
        }

        public JsonResult Dashboard(DatatableAjaxModel param)
        {
            try{
                
            IEnumerable<ViewLinhasStock> linhas = _context.LinhasStock
                .Join(
                    _context.CabecStock.Where(c=> c.tipodoc == "SF" || c.tipodoc == "DF" ),
                    ls => ls.CabecStockId,
                    cs => cs.id,
                    (ls,cs)=> new ViewLinhasStock {
                        id = ls.id,
                        nrDocExterno = cs.nrDocExterno,
                        CabecStockId = cs.id,
                        data = cs.data,
                        artigo = ls.artigo,
                        descricao = ls.descricao,
                        quantidade = ls.quantidade,
                        quantPendente= ls.quantPendente,
                        quantTrans = ls.quantTrans,
                        notas = ls.notas,
                        funcionario = cs.funcionario,
                        status = ls.status,
                        documento = cs.tipodoc,
                        nome = cs.nome
                    }
            )
            .ToList<ViewLinhasStock>();
            
            var totalCustomers = linhas.Count();
            
            var sortDirection = HttpContext.Request.Query["sSortDir_0"]; // asc or desc
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);

            switch (sortColumnIndex)
            {
                case 1:
                    linhas = sortDirection == "asc" ? linhas.OrderBy(z => z.data) : linhas.OrderByDescending(z => z.data);
                    break;
                case 2:
                    linhas = sortDirection == "asc" ? linhas.OrderBy(z => z.artigo) : linhas.OrderByDescending(z => z.artigo);
                    break;
                case 3:
                    linhas = sortDirection == "asc" ? linhas.OrderBy(z => z.descricao) : linhas.OrderByDescending(z => z.descricao);
                    break;
                default:
                    linhas = linhas.OrderBy(z => z.data);
                    break;


            }
            var filteredCustomersCount = linhas.Count();
            linhas = linhas.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCustomers,
                iTotalDisplayRecords = filteredCustomersCount,
                aaData = linhas
            });
            }catch(Exception ex){

                return Json(new{
                    sEcho = "",
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<ViewLinhasStock> ()
                });
            }
            
        }

        public JsonResult Dashboard_Funcionario(DatatableAjaxModel param)
        {
            try
            {

                IEnumerable<Funcionarios> linhas = _context.CabecStock.Where(c => c.status == StockStatus.Aberto)
                            .Select(c=> new Funcionarios(){
                                nome = c.nome,
                                codigo = c.funcionario
                            }).AsEnumerable().Distinct();

                var totalCustomers = linhas.Count();

                var sortDirection = HttpContext.Request.Query["sSortDir_0"]; // asc or desc
                var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);

                switch (sortColumnIndex)
                {
                    case 1:
                        linhas = sortDirection == "asc" ? linhas.OrderBy(z => z.codigo) : linhas.OrderByDescending(z => z.codigo);
                        break;
                    case 2:
                        linhas = sortDirection == "asc" ? linhas.OrderBy(z => z.nome) : linhas.OrderByDescending(z => z.nome);
                        break;                    
                    default:
                        linhas = linhas.OrderBy(z => z.codigo);
                        break;


                }
                var filteredCustomersCount = linhas.Count();
                linhas = linhas.Skip(param.iDisplayStart).Take(param.iDisplayLength);
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalCustomers,
                    iTotalDisplayRecords = filteredCustomersCount,
                    aaData = linhas
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    sEcho = "",
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<ViewLinhasStock>()
                });
            }

        }

        // GET: Ferramentas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entrada()
        {
            CabecStock cab = new CabecStock();
            
            cab.tipodoc = "DF";

            var doc = _context.TipoDocumentoStock.First(m => m.tipo == cab.tipodoc);

            if(doc != null )
                cab.Documento = doc;

            return View(cab);
        }

        public ActionResult Saida()
        {
            CabecStock cab = new CabecStock
            {
                tipodoc = "SF"
            };

            var doc = _context.TipoDocumentoStock.First(m => m.tipo == cab.tipodoc);

            if (doc != null)
                cab.Documento = doc;

            return View(cab);
        }

        [HttpPost]
        public JsonResult GravarSaida([FromBody] CabecStock cabecDoc)
        {
            try
            {
                cabecDoc.data = DateTime.Now;
                _context.Add(cabecDoc);

                var result = _context.SaveChanges();
                
                return Json(new { success = true, message= "Foi Gerado o Documento com Sucesso" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public JsonResult GravarEntrada([FromBody] CabecStock cabecDoc)
        {
            try
            {
                cabecDoc.data = DateTime.Now;

                _context.Add(cabecDoc);

                var result = _context.SaveChanges();


                var ids = (from a in cabecDoc.linhas
                           select a.idLinhaOrigem).Distinct();

                
                foreach (var id in ids)
                {
                    var linhaOrigem = _context.LinhasStock.First(p => p.id == id);

                    var quantTrans = _context.LinhasStock.Where(p => p.idLinhaOrigem == id)
                            .Sum(p => p.quantidade);

                    linhaOrigem.quantTrans = quantTrans;
                    linhaOrigem.quantPendente = linhaOrigem.quantidade - quantTrans;

                    if (linhaOrigem.quantPendente == 0)
                    {
                        linhaOrigem.status = StockStatus.Fechado;
                    }

                    _context.Update(linhaOrigem);
                    _context.SaveChanges();


                }

                ids = (from a in cabecDoc.linhas
                        select a.idDocumentoOrigem).Distinct();

                foreach (var id in ids)
                {


                    var docAbertos = _context.LinhasStock.Where(p => p.status == StockStatus.Aberto && p.CabecStockId == id);

                    if (docAbertos.Count() == 0)
                    {
                        var cabecOrigem = _context.CabecStock.First(p => p.id == id);

                        cabecOrigem.status = StockStatus.Fechado;
                        _context.Update(cabecOrigem);
                        _context.SaveChanges();
                    }
                }




                return Json(new { success = true, message = "Foi Gerado o Documento com Sucesso" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // GET: Ferramentas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ferramentas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ferramentas/Create
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

        // GET: Ferramentas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ferramentas/Edit/5
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

        // GET: Ferramentas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ferramentas/Delete/5
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