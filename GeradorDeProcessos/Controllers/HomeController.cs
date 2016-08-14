﻿using GeradorDeProcessos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeradorDeProcessos.Controllers
{
	public class HomeController : Controller
	{
		private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();
		// GET: Home
		public async Task<ActionResult> Index(string searchString)
		{
			var empreendimentos = db.Empreendimentos.Include(e => e.Empresas);

			if (!String.IsNullOrEmpty(searchString))
			{
				empreendimentos = empreendimentos.Where(e => e.Nome.ToUpper().Contains(searchString.ToUpper()));
			}

			return View(await empreendimentos.ToListAsync());
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