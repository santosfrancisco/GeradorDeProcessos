using GeradorDeProcessos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using System.Web.Mvc;
using GeradorDeProcessos.Repositorios;

namespace GeradorDeProcessos.Controllers
{
    public class HomeController : BaseController
    {
<<<<<<< HEAD
		private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();
		// GET: Home
		public async Task<ActionResult> Index(string filtro = "")
		{
			if (filtro != "")
			{
				var empreendimentos = db.Empreendimentos.Where(e => e.Nome.Contains(filtro));
				return View(await empreendimentos.ToListAsync());
			}
			else
			{
				var empreendimentos = db.Empreendimentos.Include(e => e.Empresas);
				return View(await empreendimentos.ToListAsync());
			}
		}
		// GET: Home/Configuracoes
		public ActionResult Configuracoes()
=======
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
>>>>>>> origin/master
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