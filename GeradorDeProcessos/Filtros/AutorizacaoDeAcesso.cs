using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using GeradorDeProcessos.Repositorios;
using System.Web.Mvc;

namespace GeradorDeProcessos.Filtros
{
    [HandleError]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AutorizacaoDeAcesso : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext FiltroDeContexto)
        {
            var Controller = FiltroDeContexto.ActionDescriptor.ControllerDescriptor.ControllerName;
			var Action = FiltroDeContexto.ActionDescriptor.ActionName;

			if (Action != "Login")
            {
                if (RepositorioUsuarios.VerificaSeOUsuarioEstaLogado() == null)
                {
					FiltroDeContexto.RequestContext.HttpContext.Response.Redirect("/Home/Login", false);



				}
            }

			//if(Controller == "Usuarios" && Action == "Edit")
			//{
				
			//	if (RepositorioUsuarios.VerificaSeOUsuarioEstaLogado() == null || RepositorioUsuarios.VerificaTipoUsuario() != 0)
			//	{
			//		FiltroDeContexto.RequestContext.HttpContext.Response.Redirect("/Home/Index");
			//		//+ FiltroDeContexto.HttpContext.Request.Url.LocalPath);

			//	} else
			//	{

			//	}
			//}
        }
		
    }
}