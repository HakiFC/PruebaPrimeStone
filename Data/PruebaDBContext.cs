using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Data
{
    public class PruebaDBContext : DbContext
    {
        public PruebaDBContext(DbContextOptions<PruebaDBContext> options) : base(options)
        {
        }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<Entidad> Entidad { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<EstudianteCurso> EstudianteCurso { get; set; }
        public DbSet<DoWork> DoWork { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstudianteCurso>().HasKey(x => new { x.CursoId, x.EstudianteId });

            modelBuilder.Entity<DoWork>().HasNoKey();
        }
    }
}
