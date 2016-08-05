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
	public class UnidadesController : Controller
	{
		private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();

		// GET: Unidades
		public ActionResult Index()
		{
			//var unidades = db.Unidades.Include(u => u.Empreendimentos);
			return RedirectToAction("Index", "Empreendimentos");
		}

		// GET: Unidades/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Unidades unidades = await db.Unidades.FindAsync(id);
			if (unidades == null)
			{
				return HttpNotFound();
			}
			if( unidades.Vendida == true)
			{
				ViewBag.Status = "Vendida";
			}
			else
			{
				ViewBag.Status = "Disponível";
			}
			return View(unidades);
		}

		public async Task<ActionResult> ListarUnidades(int? EmpreendimentoID)
		{
			if (EmpreendimentoID == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var unidadesEmpreendimento = db.Unidades.Where(u => u.IDEmpreendimento == EmpreendimentoID);
			if (EmpreendimentoID == null)
			{
				return HttpNotFound();
			}
			return View(await unidadesEmpreendimento.ToListAsync());
		}

		// GET: Unidades/Create
		public ActionResult Create(int? id)
		{
			//ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos, "IDEmpreendimento", "Nome", id);
			ViewBag.IDEmpreendimento = db.Empreendimentos.Find(id).Nome.ToString();
			return View();
		}

		// POST: Unidades/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		//public async Task<ActionResult> Create([Bind(Include = "IDUnidade,Numero,IDEmpreendimento")] Unidades unidades)
		public async Task<ActionResult> Create(FormCollection form, int id)
		{
			string[] novasUnidades = form["unidades"].Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			if (novasUnidades != null)
			{
				if (ModelState.IsValid)
				{
					foreach (string u in novasUnidades)
					{
						Unidades unidade = new Unidades();
						unidade.Numero = u;
						unidade.IDEmpreendimento = id;
						db.Unidades.Add(unidade);
					}


					await db.SaveChangesAsync();
					return RedirectToAction("ListarUnidades", "Unidades", new { empreendimentoID = id });
				}
			}

			ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos, "IDEmpreendimento", "Nome", Convert.ToInt32(form["IDEmpreendimento"]));
			return View();
		}


		// GET: Unidades/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Unidades unidades = await db.Unidades.FindAsync(id);
			if (unidades == null)
			{
				return HttpNotFound();
			}
			ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos, "IDEmpreendimento", "Nome", unidades.IDEmpreendimento);
			return View(unidades);
		}

		// POST: Unidades/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "IDUnidade,Numero,IDEmpreendimento")] Unidades unidades)
		{
			if (ModelState.IsValid)
			{
				db.Entry(unidades).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos, "IDEmpreendimento", "Nome", unidades.IDEmpreendimento);
			return View(unidades);
		}

		// GET: Unidades/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Unidades unidades = await db.Unidades.FindAsync(id);
			if (unidades == null)
			{
				return HttpNotFound();
			}
			return View(unidades);
		}

		// POST: Unidades/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Unidades unidades = await db.Unidades.FindAsync(id);
			db.Unidades.Remove(unidades);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		//GET Unidades/ListaConsulta/1
		public async Task<ActionResult> ListaConsulta(int? empreendimentoID)
		{
			var empreendimento = db.Empreendimentos.Find(empreendimentoID);
			ViewBag.NomeEmpreendimento = empreendimento.Nome.ToString();
			if (empreendimentoID == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var unidades = db.Unidades.Where(u => u.IDEmpreendimento == empreendimentoID);
			if (empreendimentoID == null)
			{
				return HttpNotFound();
			}
			return View(await unidades.ToListAsync());
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
