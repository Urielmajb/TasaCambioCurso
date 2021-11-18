namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("AlumnoCurso")]
    public partial class AlumnoCurso
    {
        public int id { get; set; }

        public int Alumno_id { get; set; }

        public int Curso_id { get; set; }

        [Required (ErrorMessage = "Nota es Requerida")]
        [Range (0,100, ErrorMessage = "Nota Comprendida entre 0  y 100")]
        public decimal Nota { get; set; }

        public virtual Curso Curso { get; set; }

        public virtual Alumno Alumno { get; set; }

        public List<AlumnoCurso> Listar(int Alumno_Id)
        {
            var OalumnoCurso = new List<AlumnoCurso>();
            try
            {
                using (var cn = new BDCursos())
                {
                    OalumnoCurso = cn.AlumnoCurso.Include("Curso").Where(x => x.Alumno_id == Alumno_Id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return OalumnoCurso;
        }

        public JsonModel Guardar()
        {
            var rm = new JsonModel();
            try
            {
                using (var cn = new BDCursos())
                {

                    cn.Entry(this).State = EntityState.Added;
                    rm.SetResponse(true);
                    cn.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rm;
        }
    }
}
