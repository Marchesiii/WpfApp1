using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public class TelaInfoBuilder
    {
        private Type tipo;
        public TelaInfoBuilder(Type type) {
            tipo = type;
        }

        public TelaInfoVm CreateTelaInfo(TelaInfoVm tela, IItem item, ILocador empresa)
        {
            tela.Codigo = item.Codigo;
            tela.NomeCompleto = item.NomeCompleto;
            if (tipo.Equals(typeof(Pessoa))){
                Pessoa pessoa = (Pessoa)item;
                IEnumerable<Emprestimo> lista = ((Biblioteca)empresa).ListaEmprestimos.Where(emprestimo => emprestimo.CodigoPessoa == pessoa.Codigo);
                List<IItemLocado> livros = new List<IItemLocado>();
                foreach(Emprestimo emp in lista)
                {
                    livros.Add((IItemLocado)empresa.ListaItens(typeof(Livro)).Where( livro => livro.Codigo == emp.CodigoLivro).First());
                }
                tela.LivrosEmprestados = livros;
            } else
            {

                Livro livro = (Livro)item;
                IEnumerable<Emprestimo> lista = ((Biblioteca)empresa).ListaEmprestimos.Where(emprestimo => emprestimo.CodigoLivro == livro.Codigo);
                tela.Autor = livro.Autor;
                tela.Pags = livro.Pags;
                if (lista.Any())
                {
                    tela.Pessoa = (IItemLocador)((Biblioteca)empresa).ListaPessoa.Where(pessoa => pessoa.Codigo == lista.First().CodigoPessoa).First();
                }
            }
            return tela;
        }


    }
}
