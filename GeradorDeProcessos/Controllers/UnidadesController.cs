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
using GeradorDeProcessos.Repositorios;

namespace GeradorDeProcessos.Controllers
{
	public class UnidadesController : BaseController
	{
		private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();

		// GET: Unidades
		public ActionResult Index()
		{
			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
			{
				var unidades = db.Unidades.Include(u => u.Empreendimentos);
				//return RedirectToAction("Index", "Empreendimentos");
				return View(unidades);
			}
			else
			{
				return RedirectToAction("PermissaoNegada", "Usuarios", null);
			}
		}
		// GET: Unidades/Consulta/5
		public async Task<ActionResult> Consulta(int? id, int? page, string sortOrder, string currentFilter, string searchString)
		{
			ViewBag.CurrentSort = sortOrder;
			ViewBag.NumeroParam = String.IsNullOrEmpty(sortOrder) ? "Numero_Desc" : "";
			ViewBag.StatusParam = sortOrder == "Status" ? "Status_Desc" : "Status";
			List<Unidades> unidades;

			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
			}
			var tipoUsuario = RepositorioUsuarios.VerificaTipoUsuario();
			var idUsuario = RepositorioUsuarios.RecuperaIDUsuario();
			if (tipoUsuario == 0)
			{
				unidades = await db.Unidades.Where(u => u.IDEmpreendimento == id).ToListAsync();
			}
			else if (tipoUsuario == 1)
			{
				unidades = await db.Unidades.Where(u => u.IDEmpreendimento == id).ToListAsync();
			}
			else
			{
				unidades = await db.Unidades.Where(u => u.IDEmpreendimento == id && (u.UnidadeStatus == 0 || u.Analises.FirstOrDefault().Clientes.IDUsuario == idUsuario)).ToListAsync();
			}

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
				unidades = unidades.Where(u => u.Numero.ToUpper().Contains(searchString.ToUpper())).ToList();
			}

			switch (sortOrder)
			{
				case "Numero_Desc":
					unidades = unidades.OrderByDescending(u => u.Numero).ToList();
					break;
				case "Status":
					unidades = unidades.OrderBy(u => u.UnidadeStatus).ToList();
					break;
				case "Status_Desc":
					unidades = unidades.OrderByDescending(u => u.UnidadeStatus).ToList();
					break;
				default:
					unidades = unidades.OrderBy(u => u.Numero).ToList();
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
			Unidades unidade;

			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
			}
			var tipoUsuario = RepositorioUsuarios.VerificaTipoUsuario();
			var idUsuario = RepositorioUsuarios.RecuperaIDUsuario();
			if (tipoUsuario == 0)
			{
				unidade = await db.Unidades.FindAsync(id);
			}
			else if (tipoUsuario == 1)
			{
				unidade = await db.Unidades.FindAsync(id);
			}
			else
			{
				unidade = await db.Unidades.FindAsync(id);
				if (unidade.UnidadeStatus == 1 && unidade.Analises.FirstOrDefault().Clientes.IDUsuario != idUsuario)
				{
					RedirectToAction("PermissaoNegada", "Usuarios", null);
				}
			}
			if (unidade == null)
			{
				//return HttpNotFound();
				return RedirectToAction("Consulta");
			}

			ViewBag.UnidadeStatus = RepositorioListas.StatusUnidade();
			ViewBag.TipoUnidade = RepositorioListas.TipoUnidade();
			return View(unidade);
		}

		// GET: Unidades/Create
		public ActionResult Create(int id)
		{

			ViewBag.UnidadeStatus = RepositorioListas.StatusUnidade();
			ViewBag.Tipo = RepositorioListas.TipoUnidade();

			var tipoUsuario = RepositorioUsuarios.VerificaTipoUsuario();
			var empresa = RepositorioUsuarios.VerificaEmpresaUsuario();
			if (tipoUsuario == 0)
			{
				ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos, "IDEmpreendimento", "Nome");
			}
			else if (tipoUsuario == 1)
			{
				ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos.Where(e => e.IDEmpresa == empresa), "IDEmpreendimento", "Nome");
			}
			else
			{
				RedirectToAction("PermissaoNegada", "Usuarios", null);
			}
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
						unidade.UnidadeStatus = Convert.ToInt32(form["UnidadeStatus"]);
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
			ViewBag.UnidadeStatus = RepositorioListas.StatusUnidade();

			ViewBag.Tipo = RepositorioListas.TipoUnidade();

			Unidades unidade;

			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
			}
			var tipoUsuario = RepositorioUsuarios.VerificaTipoUsuario();
			var idUsuario = RepositorioUsuarios.RecuperaIDUsuario();
			if (tipoUsuario == 0)
			{
				unidade = await db.Unidades.FindAsync(id);
				ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos, "IDEmpreendimento", "Nome", unidade.IDEmpreendimento);
			}
			else if (tipoUsuario == 1)
			{
				unidade = await db.Unidades.FindAsync(id);
				ViewBag.IDEmpreendimento = new SelectList(db.Empreendimentos, "IDEmpreendimento", "Nome", unidade.IDEmpreendimento);
			}
			else
			{
				RedirectToAction("PermissaoNegada", "Usuarios", null);
				unidade = null;
			}

			if (unidade == null)
			{
				return HttpNotFound();
			}

			return View(unidade);
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
			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
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
			else
			{
				return RedirectToAction("PermissaoNegada", "Usuarios", null);
			}
		}

		// POST: Unidades/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
			{
				Unidades unidades = await db.Unidades.FindAsync(id);
				db.Unidades.Remove(unidades);
				await db.SaveChangesAsync();
				return RedirectToAction("Consulta");
			}
			else
			{
				return RedirectToAction("PermissaoNegada", "Usuarios", null);
			}
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
