using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public partial class BDCursos : DbContext
    {
        public BDCursos()
            : base("name=BDCursos")
        {
        }

        public virtual DbSet<Adjunto> Adjunto { get; set; }
        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<AlumnoCurso> AlumnoCurso { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adjunto>()
                .Property(e => e.Archivo)
                .IsUnicode(false);

            modelBuilder.Entity<Alumno>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Alumno>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Alumno>()
                .Property(e => e.FechaNacimiento)
                .IsUnicode(false);

            modelBuilder.Entity<Curso>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Curso>()
                .HasMany(e => e.AlumnoCurso)
                .WithRequired(e => e.Curso)
                .HasForeignKey(e => e.Curso_id);


            modelBuilder.Entity<Alumno>()
              .HasMany(e => e.AlumnoCurso)
              .WithRequired(e => e.Alumno)
              .HasForeignKey(e => e.Alumno_id);
        }

        public System.Data.Entity.DbSet<DAL.TasaCambio> TasaCambios { get; set; }
    }
}
