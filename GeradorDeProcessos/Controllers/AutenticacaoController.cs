using GeradorDeProcessos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeradorDeProcessos.Models;

namespace GeradorDeProcessos.Controllers
{
    public class AutenticacaoController : Controller
    {
        // GET: Autenticacao
        public ActionResult Index()
        {
            return View();
        }

		//Dados do usuario
		[ChildActionOnly]
		public string Dados()
		{
			var idUsuario = RepositorioUsuarios.RecuperaIDUsuario();
			Usuarios usuario = RepositorioUsuarios.RecuperaUsuarioPorID(idUsuario);

			string usuarioLogado = usuario.Nome.ToString(); /*+ "(" + usuario.Empresas.Nome.ToString() + ")";*/

			return usuarioLogado; 

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