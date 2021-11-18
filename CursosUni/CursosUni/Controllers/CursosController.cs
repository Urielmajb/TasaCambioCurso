using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.IO; 

namespace CursosUni.Controllers
{
    public class CursosController : Controller
    {
        // GET: Cursos

        private Curso Ocurso = new Curso();

        public ActionResult CursosListar()
        {
            return View(Ocurso.Listar());
        }

        public ActionResult CursosVer(int id)
        {
            return View(Ocurso.Obtener(id));
        }

        public ActionResult CursosEliminar(int id)
        {
            Ocurso.id = id;
            Ocurso.Eliminar();
            return Redirect("~/Cursos/CursosListar");
        }

        public ActionResult CursosAdd(int id = 0)
        {
            return View(id == 0 ? new Curso() : Ocurso.Obtener(id));
        }

        public JsonResult Guardar(Curso model)
        {
            var rm = new JsonModel();
            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.href = Url.Content("~/Cursos/CursosListar");
                }
            }
            return Json(rm);
        }

    }
}