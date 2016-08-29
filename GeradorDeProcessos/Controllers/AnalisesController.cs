using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GeradorDeProcessos.Models;
using GeradorDeProcessos.Repositorios;

namespace GeradorDeProcessos.Controllers
{
    public class AnalisesController : BaseController
    {
        private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();

        // GET: Analises
        public async Task<ActionResult> Index()
        {
            var analises = db.Analises.Include(a => a.Clientes).Include(a => a.Unidades);
            return View(await analises.ToListAsync());
        }

        // GET: Analises/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home", null);
            }
            Analises analises = await db.Analises.FindAsync(id);
            if (analises == null)
            {
                return HttpNotFound();
            }
            return View(analises);
        }

        // GET: Analises/Create
        public ActionResult Create(int? id)
        {
			if (id == null)
			{
				return RedirectToAction("Index","Home",null);
			}

			ViewBag.TipoAnalise = RepositorioListas.TipoAnalise();
			ViewBag.IDCliente = new SelectList(db.Clientes, "IDCliente", "Nome");
			ViewBag.IDunidade = new SelectList(db.Unidades, "IDUnidade", "Numero", id);
            return View();
        }

        // POST: Analises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDAnalise,DataEntrega,ValorFinanciamento,ValorTotal,SaldoDevedor,Observacao,TipoAnalise,IDCliente,IDUnidade")] Analises analises)
        {
            if (ModelState.IsValid)
            {
                db.Analises.Add(analises);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

			ViewBag.TipoAnalise = RepositorioListas.TipoAnalise();
            ViewBag.IDCliente = new SelectList(db.Clientes, "IDCliente", "Nome", analises.IDCliente);
            ViewBag.IDUnidade = new SelectList(db.Unidades, "IDUnidade", "Numero", analises.IDUnidade);
            return View(analises);
        }

        // GET: Analises/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
			}
			Analises analises = await db.Analises.FindAsync(id);
            if (analises == null)
            {
                return HttpNotFound();
            }

			ViewBag.TipoAnalise = RepositorioListas.TipoAnalise();
			ViewBag.IDCliente = new SelectList(db.Clientes, "IDCliente", "Nome", analises.IDCliente);
            ViewBag.IDUnidade = new SelectList(db.Unidades, "IDUnidade", "Numero", analises.IDUnidade);
            return View(analises);
        }

        // POST: Analises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDAnalise,DataEntrega,ValorFinanciamento,ValorTotal,SaldoDevedor,Observacao,TipoAnalise,IDCliente,IDUnidade")] Analises analises)
        {
            if (ModelState.IsValid)
            {
                db.Entry(analises).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

			ViewBag.TipoAnalise = RepositorioListas.TipoAnalise();
			ViewBag.IDCliente = new SelectList(db.Clientes, "IDCliente", "Nome", analises.IDCliente);
            ViewBag.IDUnidade = new SelectList(db.Unidades, "IDUnidade", "Numero", analises.IDUnidade);
            return View(analises);
        }

        // GET: Analises/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
			}
			Analises analises = await db.Analises.FindAsync(id);
            if (analises == null)
            {
                return HttpNotFound();
            }

			ViewBag.TipoAnalise = RepositorioListas.TipoAnalise();
			return View(analises);
        }

        // POST: Analises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Analises analises = await db.Analises.FindAsync(id);
            db.Analises.Remove(analises);
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
