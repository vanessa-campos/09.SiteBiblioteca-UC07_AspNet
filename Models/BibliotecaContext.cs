using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models{
    public class BibliotecaContext: DbContext{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            // optionsBuilder.UseMySql("Server=192.168.25.122;Database=Biblioteca;Uid=root;Pwd=;");
            optionsBuilder.UseMySql("Server=localhost;Database=Biblioteca;Uid=root;Pwd=;");
        }

        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Livro> Livros {get; set;}
        public DbSet<Emprestimo> Emprestimos {get; set;}

    }
}