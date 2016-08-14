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
using PagedList;

namespace GeradorDeProcessos.Controllers
{
    public class EmpreendimentosController : Controller
    {
        private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();

        // GET: Empreendimentos
        public async Task<ActionResult> Index()
        {
            var empreendimentos = db.Empreendimentos.Include(e => e.Empresas);

            return View(await empreendimentos.ToListAsync());
        }

		// GET: Empreendimentos
		public async Task<ActionResult> Gerenciar(int id)
		{
			var empreendimento = await db.Empreendimentos.FindAsync(id);
			ViewBag.Empreendimento = empreendimento.Nome.ToString();
			return View(empreendimento);
		}
		// GET: Quantidade deunidades
		public string QtdUnidades(int id)
		{
			var unidades = db.Unidades.Where(u => u.IDEmpreendimento == id).ToArray();
			string totalUnidades = unidades.Length.ToString();
			return totalUnidades;
		}
		// GET: data do habite-se
		public string Habitese(int id)
		{
			var empreendimento = db.Empreendimentos.Find(id);
			string dataHabitese = empreendimento.DataEntrega.ToShortDateString();
			return dataHabitese;
		}

		// GET: Empreendimentos/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Empreendimentos empreendimento = await db.Empreendimentos.FindAsync(id);
			ViewBag.Empreendimento = empreendimento.Nome.ToString();
			var unidades = db.Unidades.Where(u => u.IDEmpreendimento == id).ToArray();
			ViewBag.TotalUnidades = unidades.Length.ToString();
			if (empreendimento == null)
            {
                return HttpNotFound();
            }
            return View(empreendimento);
        }

        // GET: Empreendimentos/Create
        public ActionResult Create()
        {
            ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome");
			
			return View();
        }

        // POST: Empreendimentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDEmpreendimento,Nome,DataEntrega,Tipo,IDEmpresa")] Empreendimentos empreendimentos)
        {
            if (ModelState.IsValid)
            {
                db.Empreendimentos.Add(empreendimentos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome", empreendimentos.IDEmpresa);
			return View(empreendimentos);
        }

        // GET: Empreendimentos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empreendimentos empreendimento = await db.Empreendimentos.FindAsync(id);
			ViewBag.Empreendimento = empreendimento.Nome.ToString();
			if (empreendimento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome", empreendimento.IDEmpresa);
			
			return View(empreendimento);
        }

        // POST: Empreendimentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDEmpreendimento,Nome,DataEntrega,Tipo,IDEmpresa")] Empreendimentos empreendimentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empreendimentos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome", empreendimentos.IDEmpresa);
			return View(empreendimentos);
        }

        // GET: Empreendimentos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empreendimentos empreendimento = await db.Empreendimentos.FindAsync(id);
			ViewBag.Empreendimento = empreendimento.Nome.ToString();
			if (empreendimento == null)
            {
                return HttpNotFound();
            }
            return View(empreendimento);
        }

        // POST: Empreendimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Empreendimentos empreendimentos = await db.Empreendimentos.FindAsync(id);
            db.Empreendimentos.Remove(empreendimentos);
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
