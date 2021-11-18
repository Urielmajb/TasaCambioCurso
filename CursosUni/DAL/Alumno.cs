namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Alumno")]
    public partial class Alumno
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        public int Sexo { get; set; }

        [Required]
        [StringLength(10)]
        public string FechaNacimiento { get; set; }
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }
        public List<Alumno> Listar()
        {
            try
            {
                using (var cn = new BDCursos())
                {
                    return cn.Alumno.ToList();
                    //return cn.Database.SqlQuery<Curso>("Usp_ListarCurso").ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Alumno Obtener (int id)
        {

            var Oalumno = new Alumno();

            try
            {
                using (var cn = new BDCursos())
                {
                    Oalumno = cn.Alumno.Include("AlumnoCurso").Include("AlumnoCurso.Curso").Where(x => x.id == id).SingleOrDefault();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

            return Oalumno;

        }

        public JsonModel Guardar()
        {

            var rm = new JsonModel();


            try
            {
                using (var cn = new BDCursos())
                {
                    if (this.id > 0)
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


        public void Eliminar()
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





    }
}
