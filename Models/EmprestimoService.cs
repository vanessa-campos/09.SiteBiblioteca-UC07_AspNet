using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class EmprestimoService 
    {
        public void Inserir(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimos.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;
                emprestimo.Devolvido = e.Devolvido;

                if(e.Devolvido == true){
                    bc.Emprestimos.Remove(emprestimo);
                    bc.SaveChanges();
                }else{
                    bc.SaveChanges();
                }
            }
        }

        public ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro = null)
        // public void ListarTodos(FiltrosEmprestimos filtro = null)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Emprestimo> query;

                if(filtro != null)
                {
                    switch(filtro.TipoFiltro)
                    {
                        case "Usuario":
                            query = bc.Emprestimos.Where(e => e.NomeUsuario.Contains(filtro.Filtro));
                        break;

                        case "Livro":
                            query = bc.Emprestimos.Where(e => e.Livro.Titulo.Contains(filtro.Filtro));
                        break;

                        default:                                
                            query = bc.Emprestimos;
                        break;
                    }
                }
                else
                {
                    query = bc.Emprestimos;
                }

                //seleciona pra lista os livros que não foram devolvidos
                query = query.Where(e => e.Devolvido == false);
                
                //ordenação padrão
                query.Include(e => e.Livro).ToList();
                return query.OrderByDescending(e => e.DataDevolucao).ToList();
                // query.OrderByDescending(e => e.DataDevolucao).ToList();

            }
        }

        public Emprestimo ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }

        public ICollection<Emprestimo> MaxEmprestimosPg(int page, int size){
            
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                int pular = (page-1)*size; 
                                             
                IQueryable<Emprestimo> query = bc.Emprestimos.Include(e => e.Livro).OrderByDescending(e => e.DataDevolucao);

                return query.Skip(pular).Take(size).ToList();
            }
        }

        public int CountItems(){
            using(BibliotecaContext bc = new BibliotecaContext()){
                return bc.Emprestimos.Count();
            }
        }
        
        
    }
}