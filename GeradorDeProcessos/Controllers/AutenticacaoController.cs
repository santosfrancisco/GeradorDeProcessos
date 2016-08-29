using GeradorDeProcessos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeradorDeProcessos.Controllers
{
    public class AutenticacaoController : Controller
    {
        // GET: Autenticacao
        public ActionResult Index()
        {
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
					Mensagem = "Usuário e/ou senha não confere. Tente novamente."
				},
					JsonRequestBehavior.AllowGet);
			}
		}


		//GET: logout
		public void LogOut()
		{
			RepositorioUsuarios.LogOut();
		}
	}
}