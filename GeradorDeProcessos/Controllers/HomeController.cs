using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using GeradorDeProcessos.Repositorios;

namespace GeradorDeProcessos.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Login
        public ActionResult Login()
        {
            ViewBag.Title = "Seja Bem Vindo(a)";
            return View();
        }

        [HttpGet]
        public JsonResult AutenticarLogin(string Login, string Senha)
        {
            if (RepositorioUsuarios.AutenticarUsuario(Login, Senha))
            {
                return Json(new
                {
                    OK = true,
                    Mensagem = "Autenticado, redirecionando..."
                },
                JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    OK = false,
                    Mensagem = "Usuário não encontrato. Tente novamente."
                },
                JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Configuracoes()
		{
			return View();
		}
    }
}