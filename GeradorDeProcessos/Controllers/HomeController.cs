using GeradorDeProcessos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace GeradorDeProcessos.Controllers
{
	public class HomeController : Controller
	{
		private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();
		// GET: Home
		public ActionResult Index(int? page, string searchString, string currentFilter)
		{
			var empreendimentos = db.Empreendimentos.ToList();

			if (!String.IsNullOrEmpty(searchString))
			{
				empreendimentos = empreendimentos.Where(e => e.Nome.ToUpper().Contains(searchString.ToUpper())).ToList();
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
				empreendimentos = empreendimentos.Where(u => u.Nome.ToUpper().Contains(searchString.ToUpper())).ToList();
			}
			
			int pageSize = 5;
			int pageNumber = (page ?? 1);
			return View(empreendimentos.ToPagedList(pageNumber, pageSize));
		}

		// GET: Home/Configuracoes
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