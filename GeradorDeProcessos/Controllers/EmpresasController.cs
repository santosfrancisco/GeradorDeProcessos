﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GeradorDeProcessos.Models;
using PagedList;
using GeradorDeProcessos.Repositorios;

namespace GeradorDeProcessos.Controllers
{
    public class EmpresasController : BaseController
    {
        private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();

        // GET: Empresas
        public async Task<ActionResult> Index(int? page)
        {
			var empresas = await db.Empresas.ToListAsync();

			int pageSize = 5;
			int pageNumber = (page ?? 1);

			return View(empresas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            return View();
        }

		// GET: Empresas/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			var tipoUsuario = RepositorioUsuarios.VerificaTipoUsuario();
			var empresaUsuario = RepositorioUsuarios.VerificaEmpresaUsuario();
			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
			}
			Empresas empresa = await db.Empresas.FindAsync(id);
			if (tipoUsuario != 0 && empresa.IDEmpresa != empresaUsuario)
			{
				return RedirectToAction("PermissaoNegada", "Usuarios", null);
			}
			if (empresa == null)
			{
				return HttpNotFound();
			}
			return View(empresa);
		}

		// POST: Empresas/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDEmpresa,Nome,Responsavel,Responsavel_Email,Responsavel_Telefone")] Empresas empresas)
        {
            if (ModelState.IsValid)
            {
                db.Empresas.Add(empresas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(empresas);
        }

        // GET: Empresas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home", null);
            }
            Empresas empresas = await db.Empresas.FindAsync(id);
			ViewBag.Empreendimento = empresas.Nome.ToString();
			if (empresas == null)
            {
                return HttpNotFound();
            }
            return View(empresas);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDEmpresa,Nome,Responsavel,Responsavel_Email,Responsavel_Telefone")] Empresas empresas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(empresas);
        }

        // GET: Empresas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
			if (id == null)
            {
                return RedirectToAction("Index", "Home", null);
            }
            Empresas empresas = await db.Empresas.FindAsync(id);
			ViewBag.Empreendimento = empresas.Nome.ToString();
            if (empresas == null)
            {
                return HttpNotFound();
            }
            return View(empresas);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Empresas empresas = await db.Empresas.FindAsync(id);
            db.Empresas.Remove(empresas);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
