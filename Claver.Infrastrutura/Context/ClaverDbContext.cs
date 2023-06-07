using Claver.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Claver.Infrastrutura.Context
{
    public class ClaverDbContext : DbContext
    {
        public ClaverDbContext(DbContextOptions<ClaverDbContext> options) : base(options)
        {
                        
        }
        public DbSet<Pessoa> Tb_Pessoa { get; set; }
        public DbSet<Contacto> Tb_Contacto { get; set; }
        public DbSet<Endereco> Tb_Endereco { get; set; }
        public DbSet<Inscricao> Tb_Inscricao { get; set; }
        public DbSet<Curso> Tb_Curso { get; set; }
        public DbSet<Classe> Tb_Classe { get; set; }
        public DbSet<Aluno> Tb_Aluno { get; set; }
        public DbSet<Matricula> Tb_Matricula { get; set; }
        public DbSet<Periodo> Tb_Periodo { get; set; }
        public DbSet<CursoDisciplina> Tb_CursoDisciplina { get; set; }
        public DbSet<TurmaSala> Tb_TurmaSala { get; set; }
        public DbSet<Disciplina> Tb_Disciplina { get; set; }
        public DbSet<Sala> Tb_Sala { get; set; }
        public DbSet<Turma> Tb_Turma { get; set; }
        public DbSet<AnoLectivo> Tb_AnoLectivo { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // add your own configuration here
            
            builder.Entity<Pessoa>()
                .HasIndex(p => p.NumeroBilhete)
                .IsUnique();
                
            builder.Entity<Inscricao>()
                .HasIndex(p => p.Rupe)
                .IsUnique();

            builder.Entity<Inscricao>()
                .HasIndex(p => p.NumCandidato)
                .IsUnique();

            builder.Entity<Inscricao>()
                .Property(p => p.OrderList)
                .HasIdentityOptions(1,1);

            builder.Entity<Matricula>()
                .HasOne(p => p.AnoLectivo)
                .WithMany(p => p.Alunos)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<TurmaSala>()
                .HasOne(p => p.Turma)
                .WithMany(p => p.Salas)
                .OnDelete(DeleteBehavior.ClientNoAction);     
        }
    }
}