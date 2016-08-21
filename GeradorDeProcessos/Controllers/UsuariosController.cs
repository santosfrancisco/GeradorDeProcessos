using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GeradorDeProcessos.Models;
using GeradorDeProcessos.Repositorios;

namespace GeradorDeProcessos.Controllers
{
    public class UsuariosController : Controller
    {
        private GeradorDeProcessosEntities db = new GeradorDeProcessosEntities();

        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Empresas);
            return View(await usuarios.ToListAsync());
        }
		// GET: Usuarios
		[HttpGet]
		public JsonResult AutenticacaoDeUsuario(string Email, string Senha)
		{
			if (RepositorioUsuarios.AutenticarUsuario(Email, Senha))
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

		// GET: Usuarios/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDUsuario,Nome,Email,Senha,IDEmpresa")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome", usuarios.IDEmpresa);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome", usuarios.IDEmpresa);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDUsuario,Nome,Email,Senha,IDEmpresa")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDEmpresa = new SelectList(db.Empresas, "IDEmpresa", "Nome", usuarios.IDEmpresa);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            db.Usuarios.Remove(usuarios);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
