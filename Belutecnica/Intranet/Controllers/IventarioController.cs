﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intranet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Controllers
{
    
    public class IventarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IventarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult ListaArtigos(string tipo)
        {
            try
            {
                var strSql =string.Format(
                    @"
                        select artigo,descricao,CodBarras as codbarrasartigo, 'A' as armazem ,stkActual 
                          
                        from PRIBELUAGRO.dbo.Artigo a with(nolock)
                        where a.TipoArtigo = '{0}'
                    ",tipo)
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
                var strSql = @"select Codigo,Nome,CDU_CodigoBarras from PRIBELUAGRO.dbo.Funcionarios";

                var listaFuncionarios = _context.Funcionarios.FromSql(strSql);

                return Json(listaFuncionarios);
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
                var strSql = string.Format( @"select Centro as codigo, descricao from PRIBELUAGRO.dbo.planocentros
                    where TipoConta = 'M' and Inactivo = 0 and Ano = YEAR(GetDate()) and centro like '{0}%'",areaNegocio);

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
                var strSql = @"select Centro as codigo, descricao from PRIBELUAGRO.dbo.planocentros
                    where TipoConta = 'R' and Inactivo = 0 and Ano = YEAR(GetDate())";

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