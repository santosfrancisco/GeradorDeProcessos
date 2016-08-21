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
        public static bool AutenticarUsuario(string Email, string Senha)
        {
            var senhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(Senha, "sha1");
            using (GeradorDeProcessosEntities db = new GeradorDeProcessosEntities())
            {
                var QueryAutenticaUsuario = db.Usuarios.Where(x => x.Email == Email && x.Senha == Senha).SingleOrDefault();
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

        public static Usuarios VerificaSeOUsuarioEstaLogado()
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
    }
}