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
using PagedList.Mvc;

namespace GeradorDeProcessos.Controllers
{
	public class UnidadesController : BaseController
	{
		private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();

		// GET: Unidades
		public ActionResult Index()
		{
			var unidades = db.Unidades.Include(u => u.Empreendimentos);
			//return RedirectToAction("Index", "Empreendimentos");
			return View(unidades);
		}
		// GET: Unidades/Consulta/5
		public ActionResult Consulta(int? id, int? page, string sortOrder, string currentFilter, string searchString)
		{
			ViewBag.CurrentSort = sortOrder;
			ViewBag.NumeroParam = String.IsNullOrEmpty(sortOrder) ? "Numero_Desc" : "";
			ViewBag.StatusParam = sortOrder == "Status" ? "Status_Desc" : "Status";

			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
			}

			var unidades = db.Unidades.Where(u => u.IDEmpreendimento == id);

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			if (!String.IsNullOrEmpty(searchString))
			{
				unidades = unidades.Where(u => u.Numero.ToUpper().Contains(searchString.ToUpper()));
			}

			switch (sortOrder)
			{
				case "Numero_Desc":
					unidades = unidades.OrderByDescending(u => u.Numero);
					break;
				case "Status":
					unidades = unidades.OrderBy(u => u.UnidadeStatus);
					break;
				case "Status_Desc":
					unidades = unidades.OrderByDescending(u => u.UnidadeStatus);
					break;
				default:
					unidades = unidades.OrderBy(u => u.Numero);
					break;
			}
			ViewBag.IdEmpreendimento = id;
			int pageSize = 10;
			int pageNumber = (page ?? 1);
			return View(unidades.ToPagedList(pageNumber, pageSize));
		}

		// GET: Unidades/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
			}
			Unidades unidades = await db.Unidades.FindAsync(id);
			if (unidades == null)
			{
				//return HttpNotFound();
				return RedirectToAction("Consulta");
			}
			//ViewBag.Status = unidades.UnidadeStatus.ToString();
			IList<SelectListItem> status = new List<SelectListItem>();
			status.Add(new SelectListItem() { Text = "Disponível", Value = "Disponível" });
			status.Add(new SelectListItem() { Text = "Vendida", Value = "Vendida" });

			ViewBag.UnidadeStatus = status.ToList();
			return View(unidades);
		}

		public async Task<ActionResult> ListarUnidades(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
			}

			var unidadesEmpreendimento = db.Unidades.Where(u => u.IDEmpreendimento == id);
			if (id == null)
			{
				return HttpNotFound();
			}
			await unidadesEmpreendimento.ToListAsync();
			return RedirectToAction("Consulta");
		}

		// GET: Unidades/Create
		public ActionResult Create(int? id)
		{
			IList<SelectListItem> status = new List<SelectListItem>();
			status.Add(new SelectListItem() { Text = "Disponível", Value = "Disponível" });
			status.Add(new SelectListItem() { Text = "Vendida", Value = "Vendida" });
			ViewBag.UnidadeStatus = status.ToList();

			IList<SelectListItem> tipo = new List<SelectListItem>();
			tipo.Add(new SelectListItem() { Text = "Residencial", Value = "Residencial" });
			tipo.Add(new SelectListItem() { Text = "Comercial", Value = "Comercial" });
			ViewBag.Tipo = tipo.ToList();

			ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos, "IDEmpreendimento", "Nome");
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
						unidade.Tipo = form["Tipo"].ToString();
						unidade.UnidadeStatus = form["UnidadeStatus"].ToString();
						unidade.UnidadeObservacao = form["UnidadeObservacao"].ToString();
						db.Unidades.Add(unidade);
					}


					await db.SaveChangesAsync();
					return RedirectToAction("Consulta", "Unidades", new { id = id });
				}
			}

			ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos, "IDEmpreendimento", "Nome", Convert.ToInt32(form["IDEmpreendimento"]));
			return View();
		}


		// GET: Unidades/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			IList<SelectListItem> status = new List<SelectListItem>();
			status.Add(new SelectListItem() { Text = "Disponível", Value = "Disponível" });
			status.Add(new SelectListItem() { Text = "Vendida", Value = "Vendida" });
			ViewBag.UnidadeStatus = status.ToList();

			IList<SelectListItem> tipo = new List<SelectListItem>();
			tipo.Add(new SelectListItem() { Text = "Residencial", Value = "Residencial" });
			tipo.Add(new SelectListItem() { Text = "Comercial", Value = "Comercial" });
			ViewBag.Tipo = tipo.ToList();
			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
			}
			Unidades unidades = await db.Unidades.FindAsync(id);

			ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos, "IDEmpreendimento", "Nome", unidades.IDEmpreendimento);
			if (unidades == null)
			{
				return HttpNotFound();
			}

			return View(unidades);
		}

		// POST: Unidades/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "IDEmpreendimento,IDUnidade,Numero,Tipo,UnidadeStatus,UnidadeObservacao")] Unidades unidades)
		{
			if (ModelState.IsValid)
			{
				db.Entry(unidades).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Consulta");
			}
			ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos, "IDEmpreendimento", "Nome", unidades.IDEmpreendimento);
			return View(unidades);
		}

		// GET: Unidades/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
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
			return RedirectToAction("Consulta");
		}

		//GET Unidades/ListaConsulta/1
		public async Task<ActionResult> ListaConsulta(int? empreendimentoID)
		{
			var empreendimento = db.Empreendimentos.Find(empreendimentoID);
			ViewBag.NomeEmpreendimento = empreendimento.Nome.ToString();
			if (empreendimentoID == null)
			{
				return RedirectToAction("Index", "Home", null);
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
