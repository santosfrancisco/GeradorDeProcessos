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
	public class ClientesController : BaseController
	{
		private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();

		// GET: Clientes
		public async Task<ActionResult> Index(int? page, string searchString, string currentFilter)
		{
			List<Clientes> clientes;

			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
			{
				clientes = await db.Clientes.Include(c => c.Usuarios).ToListAsync();
			}
			else if (RepositorioUsuarios.VerificaTipoUsuario() == 1)
			{
				var empresa = RepositorioUsuarios.VerificaEmpresaUsuario();
				clientes = await db.Clientes.Where(c => c.Usuarios.IDEmpresa == empresa).ToListAsync();
			}
			else
			{
				var IDUsuario = RepositorioUsuarios.RecuperaIDUsuario();
				clientes = await db.Clientes.Where(c => c.Usuarios.IDUsuario == IDUsuario).ToListAsync();
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
				clientes = clientes.Where(c => c.Nome.ToUpper().Contains(searchString.ToUpper()) || c.CpfCnpj.Contains(searchString)).ToList();
			}

			int pageSize = 5;
			int pageNumber = (page ?? 1);

			return View(clientes.ToPagedList(pageNumber, pageSize));
		}

		// GET: Clientes/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
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
			// lista de tipos de pessoa
			ViewBag.TipoPessoa = RepositorioListas.TipoPessoa();
			// lista sexos
			ViewBag.Sexo = RepositorioListas.Sexo();
			// lista de estados civis
			ViewBag.EstadoCivil = RepositorioListas.EstadoCivil();
			// lista de regimes de casamento
			ViewBag.RegimeCasamento = RepositorioListas.RegimeCasamento();

			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
			{
				ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nome");
			}
			else if (RepositorioUsuarios.VerificaTipoUsuario() == 1)
			{
				var empresa = RepositorioUsuarios.VerificaEmpresaUsuario();
				ViewBag.IDUsuario = new SelectList(db.Usuarios.Where(u => u.IDEmpresa == empresa), "IDUsuario", "Nome");
			}
			else
			{
				var IDUsuario = RepositorioUsuarios.RecuperaIDUsuario();
				ViewBag.IDUsuario = new SelectList(db.Usuarios.Where(u => u.IDUsuario == IDUsuario), "IDUsuario", "Nome");
			}

			return View();
		}

		// POST: Clientes/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "IDCliente,TipoPessoa,CpfCnpj,Nome,Sexo,Profissao,DataNascimento,Renda,EstadoCivil,RegimeCasamento,Conjuge_Cpf,Conjuge_Nome,IDUsuario")] Clientes clientes)
		{
			// lista de tipos de pessoa
			ViewBag.TipoPessoa = RepositorioListas.TipoPessoa();
			// lista sexos
			ViewBag.Sexo = RepositorioListas.Sexo();
			// lista de estados civis
			ViewBag.EstadoCivil = RepositorioListas.EstadoCivil();
			// lista de regimes de casamento
			ViewBag.RegimeCasamento = RepositorioListas.RegimeCasamento();
			if (ModelState.IsValid)
			{
				db.Clientes.Add(clientes);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
			{
				ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nome");
			}
			else if (RepositorioUsuarios.VerificaTipoUsuario() == 1)
			{
				var empresa = RepositorioUsuarios.VerificaEmpresaUsuario();
				ViewBag.IDUsuario = new SelectList(db.Usuarios.Where(u => u.IDEmpresa == empresa), "IDUsuario", "Nome");
			}
			else
			{
				var IDUsuario = RepositorioUsuarios.RecuperaIDUsuario();
				ViewBag.IDUsuario = new SelectList(db.Usuarios.Where(u => u.IDUsuario == IDUsuario), "IDUsuario", "Nome");
			}
			return View(clientes);
		}

		// GET: Clientes/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Index", "Home", null);
			}
			Clientes clientes = await db.Clientes.FindAsync(id);
			if (clientes == null)
			{
				return HttpNotFound();
			}
			// lista de tipos de pessoa
			ViewBag.TipoPessoa = RepositorioListas.TipoPessoa();
			// lista sexos
			ViewBag.Sexo = RepositorioListas.Sexo();
			// lista de estados civis
			ViewBag.EstadoCivil = RepositorioListas.EstadoCivil();
			// lista de regimes de casamento
			ViewBag.RegimeCasamento = RepositorioListas.RegimeCasamento();

			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
			{
				ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nome");
			}
			else if (RepositorioUsuarios.VerificaTipoUsuario() == 1)
			{
				var empresa = RepositorioUsuarios.VerificaEmpresaUsuario();
				ViewBag.IDUsuario = new SelectList(db.Usuarios.Where(u => u.IDEmpresa == empresa), "IDUsuario", "Nome");
			}
			else
			{
				var IDUsuario = RepositorioUsuarios.RecuperaIDUsuario();
				ViewBag.IDUsuario = new SelectList(db.Usuarios.Where(u => u.IDUsuario == IDUsuario), "IDUsuario", "Nome");
			}

			return View(clientes);
		}

		// POST: Clientes/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "IDCliente,TipoPessoa,CpfCnpj,Nome,Sexo,Profissao,DataNascimento,Renda,EstadoCivil,RegimeCasamento,Conjuge_Cpf,Conjuge_Nome,IDUsuario")] Clientes clientes)
		{
			// lista de tipos de pessoa
			ViewBag.TipoPessoa = RepositorioListas.TipoPessoa();
			// lista sexos
			ViewBag.Sexo = RepositorioListas.Sexo();
			// lista de estados civis
			ViewBag.EstadoCivil = RepositorioListas.EstadoCivil();
			// lista de regimes de casamento
			ViewBag.RegimeCasamento = RepositorioListas.RegimeCasamento();

			if (ModelState.IsValid)
			{
				db.Entry(clientes).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(clientes);
		}

		// GET: Clientes/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (RepositorioUsuarios.VerificaTipoUsuario() == 0)
			{
				// lista de tipos de pessoa
				ViewBag.TipoPessoa = RepositorioListas.TipoPessoa();
				// lista sexos
				ViewBag.Sexo = RepositorioListas.Sexo();
				// lista de estados civis
				ViewBag.EstadoCivil = RepositorioListas.EstadoCivil();
				// lista de regimes de casamento
				ViewBag.RegimeCasamento = RepositorioListas.RegimeCasamento();

				if (id == null)
				{
					return RedirectToAction("Index", "Home", null);
				}
				Clientes clientes = await db.Clientes.FindAsync(id);
				if (clientes == null)
				{
					return HttpNotFound();
				}
				return View(clientes);
			}
			else
			{
				return RedirectToAction("PermissaoNegada", "Usuarios", null);
			}

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
