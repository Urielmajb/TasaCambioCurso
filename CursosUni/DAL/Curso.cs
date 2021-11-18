namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using System.Linq;
    using System.Data.SqlClient;

    [Table("Curso")]
    public partial class Curso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curso()
        {
            AlumnoCurso = new HashSet<AlumnoCurso>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Range(16,64, ErrorMessage ="Horas comprendidas entre 16 y 64")]

        public int? Horas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }

        public List<Curso> Listar()
        {
            try
            {
                using (var cn = new BDCursos())
                {
                    return cn.Curso.ToList();
                    //return cn.Database.SqlQuery<Curso>("Usp_ListarCurso").ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Curso Obtener (int id)
        {
            var Ocurso = new Curso();

            try
            {
                using (var cn = new BDCursos())
                {
                    Ocurso = cn.Curso.Where(x => x.id==id).SingleOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ocurso;

        }


        public void Eliminar ()
        {

            try
            {
                using (var cn = new BDCursos())
                {
                    cn.Entry(this).State = EntityState.Deleted;
                    cn.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public JsonModel Guardar()
        {

            var rm = new JsonModel();


            try
            {
                using (var cn = new BDCursos())
                {
                    if(this.id > 0 )
                    {
                        cn.Entry(this).State = EntityState.Modified;
                       // cn.SaveChanges();
                    }
                    else
                    {
                        cn.Entry(this).State = EntityState.Added;
                    }

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

        public List<Curso> CursosDisponibles(int IdAlumno = 0)
        {
            var cursos = new List<Curso>();
            try
            {
                using (var cn = new BDCursos())
                {
                    if (IdAlumno > 0)
                    {
                        var cursos_actuales = cn.AlumnoCurso.Where(x => x.Alumno_id == IdAlumno)
                            .Select(x => x.Curso_id).ToList();
                        cursos = cn.Curso.Where(x => !cursos_actuales.Contains(x.id)).ToList();
                        //forma sencilla
                        //cursos = cn.Database.SqlQuery<Curso>("SELECT C.* FROM Curso AS c WHERE c.id NOT IN (SELECT ac.Curso_id FROM  alumnocurso ac WHERE ac.Curso_id=c.id AND ac.Alumno_id=@Alumno_id)", new SqlParameter("Alumno_id", IdAlumno)).ToList();
                        //cursos = cn.Database.SqlQuery<Curso>("EXEC Usp_AlumnoCursos @Alumno_id)", new SqlParameter("Alumno_id", IdAlumno)).ToList();
                    }
                    else
                    {
                        cursos = cn.Curso.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cursos;
        }

    }
}
