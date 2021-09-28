using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Camila Viana Nunes",
                    Email = "cvn.camila@gmail.com",
                    Idade = 32
                },
                new Aluno
                {
                    Id = 2,
                    Nome = "Giovanna Zanetti",
                    Email = "gvn.giovanna@gmail.com",
                    Idade = 26
                }
            );
        }
    }
}
