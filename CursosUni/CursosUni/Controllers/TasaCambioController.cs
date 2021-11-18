using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace CursosUni.Controllers
{
    public class TasaCambioController : Controller
    {
        // GET: TasaCambio
        List<TasaCambio> tc = new List<TasaCambio>();

        private TasaCambio Otasa = new TasaCambio();

        //public ActionResult TasaCambioListar()
        //{

        //    var tc = from t in TasaCambio.ObtenerTasaCambio()
        //             orderby t.Fecha
        //             select t;
        //    return View(tc.ToList());
        //}

        public ActionResult TasaCambioListar(int iAno = 0, int iMes = 0)
        {
            if (iMes == 0)
            {
                 tc = (from t in TasaCambio.ObtenerTasaCambio()
                         orderby t.Fecha
                         select t).ToList();
            }
            else
            {
                if (iAno == 0)
                {
                   tc = (from t in TasaCambio.GetTasa(iAno, iMes)
                             orderby t.Fecha
                             select t).ToList();

                }

                else
                {
                    tc = (from t in TasaCambio.GetTasa(iAno, iMes)
                         orderby t.Fecha
                         select t).ToList();

                }

            }

            return View(tc);
        }

        

        public JsonResult Cargar (int iAno, int iMes)
        {
            var rm = new JsonModel();
            if(ModelState.IsValid)
            {
                if (iAno == 0)
                {
                    var tc = from t in TasaCambio.GetTasa(2021, iMes)
                             orderby t.Fecha
                             select t;

                }

                else
                {
                    var tc = from t in TasaCambio.GetTasa(iAno, iMes)
                             orderby t.Fecha
                             select t;
                }

                rm.response = true;
                rm.result = tc;
                if(rm.response)
                {
                    rm.href = Url.Content("~/TasaCambio/TasaCambioListar");

                }

            }

            return Json(rm);

        }

        //public ActionResult TasaCambioListar()
        //{
        //    return View(Otasa.Listar());
        //}

        //public ActionResult TasaCambioVer(int ID)
        //{
        //    return View(Otasa.Obtener(ID));
        //}

        //public ActionResult TasaCambioAdd(int ID = 0)
        //{
        //    return View(ID == 0 ? new TasaCambio() : Otasa.Obtener(ID));
        //}

        //public JsonResult Guardar(TasaCambio model)
        //{
        //    var rm = new JsonModel();
        //    if (ModelState.IsValid)
        //    {
        //        rm = model.Guardar();
        //        if (rm.response)
        //        {
        //            rm.href = Url.Content("~/TasaCambio/TasaCambioListar");
        //        }
        //    }
        //    return Json(rm);
        //}



    }
}