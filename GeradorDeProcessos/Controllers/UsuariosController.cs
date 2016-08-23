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
using PagedList;

namespace GeradorDeProcessos.Controllers
{
	public class UsuariosController : BaseController
	{
		private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();

		// GET: Usuarios
		public async Task<ActionResult> Index(int? page, string searchString, string currentFilter)
		{
			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
			{
				var usuarios = await db.Usuarios.Include(u => u.Empresas).ToListAsync();

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
					usuarios = usuarios.Where(u => u.Nome.ToUpper().Contains(searchString.ToUpper()) || u.Email.ToUpper().Contains(searchString.ToUpper())).ToList();
				}

				int pageSize = 10;
				int pageNumber = (page ?? 1);

				return View(usuarios.ToPagedList(pageNumber, pageSize));
			}
			else if (RepositorioUsuarios.VerificaTipoUsuario() == 1)
			{
				var empresa = RepositorioUsuarios.VerificaEmpresaUsuario();
				var usuarios = db.Usuarios.Where(u => u.IDEmpresa == empresa && u.TipoUsuario == 2);
				return View(await usuarios.ToListAsync());
			}
			else
			{
				return RedirectToAction("PermissaoNegada", "Usuarios", null);
			}


		}
		// GET : Permissao negada
		public ActionResult PermissaoNegada()
		{
			return View();
		}

		// Retorna o tipo do usuário
		public string TipoDeUsuario(int? id)
		{
			if (id != null)
			{
				var usuarios = db.Usuarios.Where(u => u.IDUsuario == id);
				var tipo = usuarios.First().TipoUsuario.Value;
				string TipoUsuario = "";

				switch (tipo)
				{
					case 0:
						TipoUsuario = "Administrador";
						break;
					case 1:
						TipoUsuario = "Gestor";
						break;
					case 2:
						TipoUsuario = "Usuário";
						break;

				}
				return TipoUsuario;
			}
			else
			{
				return null;
			}
		}

		// GET: Usuarios
		[HttpGet]
		public JsonResult AutenticacaoDeUsuario(string Email, string Senha)
		{
			if (RepositorioUsuarios.AutenticarUsuario(Email, Senha))
			{
				return Json(new
				{
					OK = true,
					Mensagem = "Usuário autenticado. Redirecionando..."
				},
					JsonRequestBehavior.AllowGet);
			}
			else
			{
				return Json(new
				{
					OK = false,
					Mensagem = "Usuário não encontrado. Tente novamente."
				},
					JsonRequestBehavior.AllowGet);
			}
		}

		// GET: Usuarios/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (RepositorioUsuarios.VerificaTipoUsuario() == 0 || RepositorioUsuarios.VerificaTipoUsuario() == 1)
			{
				if (id == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				Usuarios usuarios = await db.Usuarios.FindAsync(id);
				ViewBag.TipoUsuario = TipoDeUsuario(id);
				if (usuarios == null)
				{
					return HttpNotFound();
				}
				return View(usuarios);
			}
			else
			{
				return RedirectToAction("PermissaoNegada", "Usuarios", null);
			}

		}

		// GET: Usuarios/Create
		public ActionResult Create()
		{
			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
			{
				IList<SelectListItem> ListaTipos = new List<SelectListItem>();
				ListaTipos.Add(new SelectListItem() { Text = "Administrador", Value = "0" });
				ListaTipos.Add(new SelectListItem() { Text = "Gestor", Value = "1" });
				ListaTipos.Add(new SelectListItem() { Text = "Usuário", Value = "2" });

				ViewBag.TipoUsuario = ListaTipos.ToList();
				ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome");
				return View();
			}
			else if (RepositorioUsuarios.VerificaTipoUsuario() == 1)
			{
				IList<SelectListItem> ListaTipos = new List<SelectListItem>();
				ListaTipos.Add(new SelectListItem() { Text = "Usuário", Value = "2" });

				ViewBag.TipoUsuario = ListaTipos.ToList();
				var empresa = RepositorioUsuarios.VerificaEmpresaUsuario();
				ViewBag.IDEmpresa = new SelectList(db.Empresas.Where(u => u.IDEmpresa == empresa), "IDEmpresa", "Nome");
				return View();
			}
			else
			{
				return RedirectToAction("PermissaoNegada", "Usuarios", null);
			}
		}

		// POST: Usuarios/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "IDUsuario,TipoUsuario,Nome,Email,Senha,IDEmpresa")] Usuarios usuarios)
		{
			if (ModelState.IsValid)
			{
				db.Usuarios.Add(usuarios);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			IList<SelectListItem> ListaTipos = new List<SelectListItem>();
			ListaTipos.Add(new SelectListItem() { Text = "Administrador", Value = "0" });
			ListaTipos.Add(new SelectListItem() { Text = "Gestor", Value = "1" });
			ListaTipos.Add(new SelectListItem() { Text = "Usuário", Value = "2" });

			ViewBag.TipoUsuario = ListaTipos.ToList();
			ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome", usuarios.IDEmpresa);
			return View(usuarios);
		}

		// GET: Usuarios/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
			{
				if (id == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				Usuarios usuarios = await db.Usuarios.FindAsync(id);
				if (usuarios == null)
				{
					return HttpNotFound();
				}
				IList<SelectListItem> ListaTipos = new List<SelectListItem>();
				ListaTipos.Add(new SelectListItem() { Text = "Administrador", Value = "0" });
				ListaTipos.Add(new SelectListItem() { Text = "Gestor", Value = "1" });
				ListaTipos.Add(new SelectListItem() { Text = "Usuário", Value = "2" });

				ViewBag.TipoUsuario = ListaTipos.ToList();
				ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome", usuarios.IDEmpresa);
				return View(usuarios);
			}
			else if (RepositorioUsuarios.VerificaTipoUsuario() == 1)
			{
				if (id == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				Usuarios usuarios = await db.Usuarios.FindAsync(id);
				if (usuarios == null)
				{
					return HttpNotFound();
				}
				IList<SelectListItem> ListaTipos = new List<SelectListItem>();
				ListaTipos.Add(new SelectListItem() { Text = "Usuário", Value = "2" });

				ViewBag.TipoUsuario = ListaTipos.ToList();
				var empresa = RepositorioUsuarios.VerificaEmpresaUsuario();
				ViewBag.IDEmpresa = new SelectList(db.Empresas.Where(u => u.IDEmpresa == empresa), "IDEmpresa", "Nome");

				return View(usuarios);
			}
			else
			{
				return RedirectToAction("PermissaoNegada", "Usuarios", null);
			}

		}

		// POST: Usuarios/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "IDUsuario,TipoUsuario,Nome,Email,Senha,IDEmpresa")] Usuarios usuarios)
		{
			if (ModelState.IsValid)
			{
				db.Entry(usuarios).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			IList<SelectListItem> ListaTipos = new List<SelectListItem>();
			ListaTipos.Add(new SelectListItem() { Text = "Administrador", Value = "0" });
			ListaTipos.Add(new SelectListItem() { Text = "Gestor", Value = "1" });
			ListaTipos.Add(new SelectListItem() { Text = "Usuário", Value = "2" });

			ViewBag.TipoUsuario = ListaTipos.ToList();
			ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome", usuarios.IDEmpresa);
			return View(usuarios);
		}

		// GET: Usuarios/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
			{
				if (id == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				Usuarios usuarios = await db.Usuarios.FindAsync(id);
				ViewBag.TipoUsuario = TipoDeUsuario(id);
				if (usuarios == null)
				{
					return HttpNotFound();
				}
				return View(usuarios);
			}
			else
			{
				return RedirectToAction("PermissaoNegada", "Usuarios", null);
			}

		}

		// POST: Usuarios/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Usuarios usuarios = await db.Usuarios.FindAsync(id);
			db.Usuarios.Remove(usuarios);
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
