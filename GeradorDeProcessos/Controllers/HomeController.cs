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
	public class HomeController : Controller
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

		//GET: logout
		public void LogOut()
		{
			RepositorioUsuarios.LogOut();
		}

		// GET: Login
		public ActionResult Login()
		{
			ViewBag.Title = "Seja Bem Vindo(a)";
			return View();
		}

		// Login
		[HttpPost]
		public JsonResult AutenticacaoDeUsuario(string Login, string Senha)
		{
			if (RepositorioUsuarios.AutenticarUsuario(Login, Senha))
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

		public ActionResult Configuracoes()
		{
			return View();
		}

		//public ActionResult ListarEmpreendimentos(Listagem listar)
		//{
		//	var empreendimentos = from e in db.Empreendimentos
		//						  where e.IDEmpresa == listar.IDEmpresa
		//						  select new ResultadoListagem { Empreendimento = e.Nome };
		//	return Json(empreendimentos, JsonRequestBehavior.AllowGet);
		//}
	}
}