using GeradorDeProcessos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using GeradorDeProcessos.Repositorios;

namespace GeradorDeProcessos.Controllers
{
	public class HomeController : BaseController
	{
		private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();
		// GET: Home
		public async Task<ActionResult> Index(int? page, string searchString, string currentFilter)
		{
			List<Empreendimentos> empreendimentos;

			var tipoUsuario = RepositorioUsuarios.VerificaTipoUsuario();
			var empresa = RepositorioUsuarios.VerificaEmpresaUsuario();

			if (tipoUsuario == 0)
			{
				empreendimentos = await db.Empreendimentos.ToListAsync();
			}
			else if (tipoUsuario == 1)
			{
				empreendimentos = await db.Empreendimentos.Where(e => e.IDEmpresa == empresa).ToListAsync();
			}
			else
			{
				empreendimentos = await db.Empreendimentos.Where(e => e.IDEmpresa == empresa).ToListAsync();
			}

			if (!String.IsNullOrEmpty(searchString))
			{
				page = 1;
				empreendimentos = empreendimentos.Where(e => e.Nome.ToUpper().Contains(searchString.ToUpper())).ToList();
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			if (!String.IsNullOrEmpty(searchString))
			{
				empreendimentos = empreendimentos.Where(u => u.Nome.ToUpper().Contains(searchString.ToUpper())).ToList();
			}

			int pageSize = 5;
			int pageNumber = (page ?? 1);
			return View(empreendimentos.ToPagedList(pageNumber, pageSize));
		}


		// GET: Login
		public ActionResult Login()
		{
			ViewBag.Title = "Seja Bem Vindo(a)";
			return View();
		}

		public ActionResult Configuracoes()
		{
			return View();
		}

	}
}