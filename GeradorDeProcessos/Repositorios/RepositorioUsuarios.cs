using GeradorDeProcessos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Security;

namespace GeradorDeProcessos.Repositorios
{
	public class RepositorioUsuarios
	{
		public static bool AutenticarUsuario(string Login, string Senha)
		{
			var senhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(Senha, "sha1");
			using (GeradorDeProcessosEntities db = new GeradorDeProcessosEntities())
			{
				var QueryAutenticaUsuario = db.Usuarios.Where(x => x.Email == Login && x.Senha == Senha).SingleOrDefault();
				if (QueryAutenticaUsuario == null)
				{
					return false;
				}
				else
				{
					RepositorioCookies.RegistraCookieAutenticacao(QueryAutenticaUsuario.IDUsuario);
					return true;
				}
			}
		}

		public static void LogOut()
		{
			var usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
			if (usuario != null)
			{
				RepositorioCookies.LogOut();
			}
		}

		public static Usuarios RecuperaUsuarioPorID(long IDUsuario)
		{
			try
			{
				using (GeradorDeProcessosEntities db = new GeradorDeProcessosEntities())
				{
					var usuario =
						db.Usuarios.Where(u => u.IDUsuario == IDUsuario).SingleOrDefault();

					return usuario;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static long RecuperaIDUsuario()
		{
			var usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
			try
			{ 
				long IDUsuario = Convert.ToInt64(RepositorioCriptografia.Descriptografar(usuario.Values["IDUsuario"]));
				return IDUsuario;
			}
			catch
			{
				HttpContext.Current.Response.Redirect("/Home/Login", false);
				return 0;
			}
		}

		public static Usuarios VerificaSeOUsuarioEstaLogado()
		{
			try
			{
				var usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
				if (usuario == null)
				{
					return null;
				}
				else
				{
					long IDUsuario = Convert.ToInt64(RepositorioCriptografia.Descriptografar(usuario.Values["IDUsuario"]));

					var usuarioRetornado = RecuperaUsuarioPorID(IDUsuario);
					return usuarioRetornado;
				}

			}
			catch
			{
				return null;
			}

		}

		public static long VerificaTipoUsuario()
		{
			try
			{
				using (GeradorDeProcessosEntities db = new GeradorDeProcessosEntities())
				{
					var usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
					long ID = Convert.ToInt64(RepositorioCriptografia.Descriptografar(usuario.Values["IDUsuario"]));
					int Tipo = db.Usuarios.Where(u => u.IDUsuario == ID).First().TipoUsuario.Value;

					return Tipo;
				}
			}
			catch (Exception)
			{
				return -1;
			}
		}

		public static int VerificaEmpresaUsuario()
		{
			try
			{
				using (GeradorDeProcessosEntities db = new GeradorDeProcessosEntities())
				{
					var usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
					long ID = Convert.ToInt64(RepositorioCriptografia.Descriptografar(usuario.Values["IDUsuario"]));
					int Empresa = db.Usuarios.Where(u => u.IDUsuario == ID).First().IDEmpresa;

					return Empresa;
				}
			}
			catch (Exception)
			{
				return -1;
			}
		}
	}
}