using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models{
    public class BibliotecaContext: DbContext{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseMySql("Server=localhost;Database=Biblioteca;Uid=root;Pwd=;");
        }

        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Livro> Livros {get; set;}
        public DbSet<Emprestimo> Emprestimos {get; set;}

    }
}