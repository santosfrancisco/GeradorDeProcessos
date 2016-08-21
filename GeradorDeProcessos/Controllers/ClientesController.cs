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
	public class ClientesController : Controller
	{
		private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();

		// GET: Clientes
		public async Task<ActionResult> Index(int? page, string searchString, string currentFilter)
		{
			var clientes = await db.Clientes.Include(c => c.Usuarios).ToListAsync();

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
			// lista de tipo de pessoa
			IList<SelectListItem> tipo = new List<SelectListItem>();
			tipo.Add(new SelectListItem() { Text = "Física", Value = "0" });
			tipo.Add(new SelectListItem() { Text = "Jurídica", Value = "1" });
			ViewBag.TipoPessoa = tipo.ToList();
			// lista sexos
			IList<SelectListItem> sexos = new List<SelectListItem>();
			sexos.Add(new SelectListItem() { Text = "Masculino", Value = "Masculino" });
			sexos.Add(new SelectListItem() { Text = "Feminino", Value = "Feminino" });
			ViewBag.Sexo = sexos.ToList();
			// lista de estados civis
			IList<SelectListItem> estadosCivis = new List<SelectListItem>();
			estadosCivis.Add(new SelectListItem() { Text = "Solteiro(a)", Value = "Solteiro(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Casado(a)", Value = "Casado(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Divorciado(a)", Value = "Divorciado(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Separado(a) judicialmente", Value = "Separado(a) judicialmente" });
			estadosCivis.Add(new SelectListItem() { Text = "Viúvo(a)", Value = "Viúvo(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "União Estável", Value = "União Estável" });
			ViewBag.EstadoCivil = estadosCivis.ToList();
			// lista de regimes de casamento
			IList<SelectListItem> regimesCasamento = new List<SelectListItem>();
			regimesCasamento.Add(new SelectListItem() { Text = "Comunhão universal de bens", Value = "Comunhão universal de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Comunhão parcial de bens", Value = "Comunhão parcial de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Separação total de bens", Value = "Separação total de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Participação final nos aquestos", Value = "Participação final nos aquestos" });
			regimesCasamento.Add(new SelectListItem() { Text = "Separação obrigatória de bens", Value = "Separação obrigatória de bens" });
			ViewBag.RegimeCasamento = regimesCasamento.ToList();

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
				return RedirectToAction("Index", "Home", null);
			}
			Clientes clientes = await db.Clientes.FindAsync(id);
			if (clientes == null)
			{
				return HttpNotFound();
			}
			// lista de tipo de pessoa
			IList<SelectListItem> tipo = new List<SelectListItem>();
			tipo.Add(new SelectListItem() { Text = "Física", Value = "0" });
			tipo.Add(new SelectListItem() { Text = "Jurídica", Value = "1" });
			ViewBag.TipoPessoa = tipo.ToList();
			// lista sexos
			IList<SelectListItem> sexos = new List<SelectListItem>();
			sexos.Add(new SelectListItem() { Text = "Masculino", Value = "Masculino" });
			sexos.Add(new SelectListItem() { Text = "Feminino", Value = "Feminino" });
			ViewBag.Sexo = sexos.ToList();
			// lista de estados civis
			IList<SelectListItem> estadosCivis = new List<SelectListItem>();
			estadosCivis.Add(new SelectListItem() { Text = "Solteiro(a)", Value = "Solteiro(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Casado(a)", Value = "Casado(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Divorciado(a)", Value = "Divorciado(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Separado(a) judicialmente", Value = "Separado(a) judicialmente" });
			estadosCivis.Add(new SelectListItem() { Text = "Viúvo(a)", Value = "Viúvo(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "União Estável", Value = "União Estável" });
			ViewBag.EstadoCivil = estadosCivis.ToList();
			// lista de regimes de casamento
			IList<SelectListItem> regimesCasamento = new List<SelectListItem>();
			regimesCasamento.Add(new SelectListItem() { Text = "Comunhão universal de bens", Value = "Comunhão universal de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Comunhão parcial de bens", Value = "Comunhão parcial de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Separação total de bens", Value = "Separação total de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Participação final nos aquestos", Value = "Participação final nos aquestos" });
			regimesCasamento.Add(new SelectListItem() { Text = "Separação obrigatória de bens", Value = "Separação obrigatória de bens" });
			ViewBag.RegimeCasamento = regimesCasamento.ToList();
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
			// lista de tipo de pessoa
			IList<SelectListItem> tipo = new List<SelectListItem>();
			tipo.Add(new SelectListItem() { Text = "Física", Value = "0" });
			tipo.Add(new SelectListItem() { Text = "Jurídica", Value = "1" });
			ViewBag.TipoPessoa = tipo.ToList();
			// lista sexos
			IList<SelectListItem> sexos = new List<SelectListItem>();
			sexos.Add(new SelectListItem() { Text = "Masculino", Value = "Masculino" });
			sexos.Add(new SelectListItem() { Text = "Feminino", Value = "Feminino" });
			ViewBag.Sexo = sexos.ToList();
			// lista de estados civis
			IList<SelectListItem> estadosCivis = new List<SelectListItem>();
			estadosCivis.Add(new SelectListItem() { Text = "Solteiro(a)", Value = "Solteiro(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Casado(a)", Value = "Casado(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Divorciado(a)", Value = "Divorciado(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Separado(a) judicialmente", Value = "Separado(a) judicialmente" });
			estadosCivis.Add(new SelectListItem() { Text = "Viúvo(a)", Value = "Viúvo(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "União Estável", Value = "União Estável" });
			ViewBag.EstadoCivil = estadosCivis.ToList();
			// lista de regimes de casamento
			IList<SelectListItem> regimesCasamento = new List<SelectListItem>();
			regimesCasamento.Add(new SelectListItem() { Text = "Comunhão universal de bens", Value = "Comunhão universal de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Comunhão parcial de bens", Value = "Comunhão parcial de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Separação total de bens", Value = "Separação total de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Participação final nos aquestos", Value = "Participação final nos aquestos" });
			regimesCasamento.Add(new SelectListItem() { Text = "Separação obrigatória de bens", Value = "Separação obrigatória de bens" });
			ViewBag.RegimeCasamento = regimesCasamento.ToList();
			ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nome", clientes.IDUsuario);
			return View(clientes);
		}

		// GET: Clientes/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			// lista de tipo de pessoa
			IList<SelectListItem> tipo = new List<SelectListItem>();
			tipo.Add(new SelectListItem() { Text = "Física", Value = "0" });
			tipo.Add(new SelectListItem() { Text = "Jurídica", Value = "1" });
			ViewBag.TipoPessoa = tipo.ToList();
			// lista sexos
			IList<SelectListItem> sexos = new List<SelectListItem>();
			sexos.Add(new SelectListItem() { Text = "Masculino", Value = "Masculino" });
			sexos.Add(new SelectListItem() { Text = "Feminino", Value = "Feminino" });
			ViewBag.Sexo = sexos.ToList();
			// lista de estados civis
			IList<SelectListItem> estadosCivis = new List<SelectListItem>();
			estadosCivis.Add(new SelectListItem() { Text = "Solteiro(a)", Value = "Solteiro(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Casado(a)", Value = "Casado(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Divorciado(a)", Value = "Divorciado(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Separado(a) judicialmente", Value = "Separado(a) judicialmente" });
			estadosCivis.Add(new SelectListItem() { Text = "Viúvo(a)", Value = "Viúvo(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "União Estável", Value = "União Estável" });
			ViewBag.EstadoCivil = estadosCivis.ToList();
			// lista de regimes de casamento
			IList<SelectListItem> regimesCasamento = new List<SelectListItem>();
			regimesCasamento.Add(new SelectListItem() { Text = "Comunhão universal de bens", Value = "Comunhão universal de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Comunhão parcial de bens", Value = "Comunhão parcial de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Separação total de bens", Value = "Separação total de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Participação final nos aquestos", Value = "Participação final nos aquestos" });
			regimesCasamento.Add(new SelectListItem() { Text = "Separação obrigatória de bens", Value = "Separação obrigatória de bens" });
			ViewBag.RegimeCasamento = regimesCasamento.ToList();

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
