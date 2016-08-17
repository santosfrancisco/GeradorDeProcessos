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

namespace GeradorDeProcessos.Controllers
{
	public class ClientesController : Controller
	{
		private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();

		// GET: Clientes
		public async Task<ActionResult> Index()
		{
			var clientes = db.Clientes.Include(c => c.Usuarios);
			return View(await clientes.ToListAsync());
		}

		// GET: Clientes/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Clientes clientes = await db.Clientes.FindAsync(id);
			if (clientes == null)
			{
				return HttpNotFound();
			}
			return View(clientes);
		}

		// GET: Clientes/Create
		public ActionResult Create()
		{
			IList<SelectListItem> tipo = new List<SelectListItem>();
			tipo.Add(new SelectListItem() { Text = "Física", Value = "0" });
			tipo.Add(new SelectListItem() { Text = "Jurídica", Value = "1" });

			ViewBag.TipoPessoa = tipo.ToList();
			ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nome");
			return View();
		}

		// POST: Clientes/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "IDCliente,TipoPessoa,CpfCnpj,Nome,Sexo,Profissao,DataNascimento,Renda,EstadoCivil,RegimeCasamento,Conjuge_Cpf,Conjuge_Nome,IDUsuario")] Clientes clientes)
		{
			if (ModelState.IsValid)
			{
				db.Clientes.Add(clientes);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nome", clientes.IDUsuario);
			return View(clientes);
		}

		// GET: Clientes/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Clientes clientes = await db.Clientes.FindAsync(id);
			if (clientes == null)
			{
				return HttpNotFound();
			}
			List<SelectListItem> tipo = new List<SelectListItem>();
			tipo.Add(new SelectListItem() { Text = "Física", Value = "0" });
			tipo.Add(new SelectListItem() { Text = "Jurídica", Value = "1" });
			ViewBag.TipoPessoa = tipo.ToList();
			ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nome", clientes.IDUsuario);
			return View(clientes);
		}

		// POST: Clientes/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "IDCliente,TipoPessoa,CpfCnpj,Nome,Sexo,Profissao,DataNascimento,Renda,EstadoCivil,RegimeCasamento,Conjuge_Cpf,Conjuge_Nome,IDUsuario")] Clientes clientes)
		{
			if (ModelState.IsValid)
			{
				db.Entry(clientes).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			List<SelectListItem> tipo = new List<SelectListItem>();
			tipo.Add(new SelectListItem() { Text = "Física", Value = "0" });
			tipo.Add(new SelectListItem() { Text = "Jurídica", Value = "1" });
			ViewBag.TipoPessoa = tipo.ToList();
			ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nome", clientes.IDUsuario);
			return View(clientes);
		}

		// GET: Clientes/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Clientes clientes = await db.Clientes.FindAsync(id);
			if (clientes == null)
			{
				return HttpNotFound();
			}
			return View(clientes);
		}

		// POST: Clientes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Clientes clientes = await db.Clientes.FindAsync(id);
			db.Clientes.Remove(clientes);
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
