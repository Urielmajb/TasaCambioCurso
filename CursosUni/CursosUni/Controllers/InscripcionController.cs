using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.IO;


namespace CursosUni.Controllers
{
    public class InscripcionController : Controller
    {
        // GET: Inscripcion

        private Alumno Oalumno = new Alumno();
        private AlumnoCurso OalumnoCurso = new AlumnoCurso();
        private Curso Ocurso = new Curso();

        public ActionResult InscripcionListar()
        {
            return View(Oalumno.Listar());
        }

        public ActionResult InscripcionVer(int id)
        {
            return View(Oalumno.Obtener(id));
        }

        public ActionResult InscripcionEliminar(int id)
        {
            Ocurso.id = id;
            Ocurso.Eliminar();
            return Redirect("~/Inscripcion/InscripcionListar");
        }

        public PartialViewResult InscripcionCursos (int Alumno_id)
        {
            decimal promedio = 0;

            var cursos = OalumnoCurso.Listar(Alumno_id);

            //Listar los cursos de un alumno
            ViewBag.CursosElegidos = cursos;

            //Listar cursos disponibles del alumno

            ViewBag.CursosDisponibles = Ocurso.CursosDisponibles(Alumno_id);

            //Asignar el modelo del Alumno Id
            OalumnoCurso.Alumno_id = Alumno_id;

            if (cursos.Count > 0)
            {

            promedio = cursos.Sum(x => x.Nota) / cursos.Count;
            }

            ViewBag.Promedio = promedio.ToString("N2");
            return PartialView(OalumnoCurso);
        }

        public JsonResult GuardarCurso(AlumnoCurso model)
        {
            var rm = new JsonModel();
            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.function = "CargarCursos()";
                }
            }
            return Json(rm);
        }

        public JsonResult Guardar(Alumno model)
        {
            var rm = new JsonModel();
            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.href = Url.Content("~/Inscripcion/InscripcionListar");
                }
            }

            return Json(rm);
        }
        public ActionResult InscripcionAdd(int id = 0)
        {
            return View(id == 0 ? new Alumno(): Oalumno.Obtener(id));
        }



    }
}