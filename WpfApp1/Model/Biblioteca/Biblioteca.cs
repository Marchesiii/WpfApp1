using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public class Biblioteca : ILocador
    {
        public List<IListavel> ListaPessoa { get => new List<IListavel>(listaPessoa); private set { } }
        public List<IListavel> ListaLivros { get => new List<IListavel>(listaLivros); private set { } }
        private List<Pessoa> listaPessoa;
        public List<IListavel> ListaEmprestimos { get => new List<IListavel>(listaEmprestimos); private set { } }
        private List<Livro> listaLivros;
        private List<Emprestimo> listaEmprestimos;
        private int numPessoas;
        private int numLivros;
        private int numEmp;


        public Biblioteca()
        {
            listaPessoa = new List<Pessoa>();
            listaLivros = new List<Livro>();
            listaEmprestimos = new List<Emprestimo>();
            numPessoas = 0;
            numLivros = 0;
            numEmp = 0;
        }

        public bool DevolverItem(IItem item)
        {
            Emprestimo? query = ProcuraEmprestimo(item);
            if(query != null) { 
                Pessoa pessoa = listaPessoa.Where(pessoa => pessoa.Codigo == query.CodigoPessoa).First();

                listaEmprestimos.Remove(query);
                if (!listaEmprestimos.Where(emprestimo => emprestimo.CodigoPessoa == pessoa.Codigo).Any())
                {
                    pessoa.SetOcupado(false);
                }
                ((Livro)item).SetOcupado(false);
                return true;
            }
            return false;
        }
        public bool EmprestarItem(IItem item, IItem locatario)
        {
            Emprestimo emp = CriaEmprestimo(item.Codigo, locatario.Codigo);
            if (ProcuraEmprestimo(item) != null)
            {
                return false;
            }
            ((Livro)item).SetOcupado(true);
            ((Pessoa)locatario).SetOcupado(true);
            listaEmprestimos.Add(emp);
            return true;
        }

        public Emprestimo? ProcuraEmprestimo(IItem item)
        {
            IEnumerable<Emprestimo> query = listaEmprestimos.Where(emprestimo => emprestimo.CodigoLivro == item.Codigo);
            if (query.Any())
            {
                return query.First<Emprestimo>();
            }
            return null;
        }

        public List<Pessoa> SubstituiPessoa(Pessoa pessoaInt, Pessoa pessoaOri)
        {
            pessoaOri.Codigo = pessoaInt.Codigo;
            pessoaOri.NomeCompleto = pessoaInt.NomeCompleto;
            return listaPessoa;
        }

        public List<Livro> SubstituiLivro(Livro livroInt, Livro livroOri)
        {
            livroOri.Autor = livroInt.Autor;
            livroOri.NomeCompleto = livroInt.NomeCompleto;
            livroOri.Pags = livroInt.Pags;
            return listaLivros;
        }

        public List<IListavel> AddItem(IItem item)
        {
            item.Codigo = AddNumItem(item);
            return AddListaItem(item);
        }

        public int AddNumItem(IItem item)
        {
            if (GetType(item))
            {
                numPessoas++;
                return numPessoas;
            }
            else
            {
                numLivros++;
                return numLivros;
            }

        }

        public List<IListavel> AddListaItem(IItem item)
        {
            if (GetType(item))
            {
                listaPessoa.Add((Pessoa)item);
                return new List<IListavel>(listaPessoa);
            }
            else
            {
                listaLivros.Add((Livro)item);
                return new List<IListavel>(listaLivros);
            }
        }


        public List<IListavel> RemoverItem(IItem itemSelecionado)
        {
            List<IListavel> lista = RemoverListaItem(itemSelecionado);
            RemoveNumItem(itemSelecionado);
            return lista;
        }

        public List<IListavel> SubstituiItem(IItem itemClone, IItem item)
        {
            if (GetType(item))
            {
                return new List<IListavel>(SubstituiPessoa((Pessoa)itemClone, (Pessoa)item));
            }
            else
            {
                return new List<IListavel>(SubstituiLivro((Livro)itemClone, (Livro)item));
            }
        }

        public List<IListavel> ListaItens(Type type)
        {
            if (type.Equals(typeof(IItemLocador)))
            {
                return ListaPessoa;
            }
            return ListaLivros;
        }

        private Emprestimo CriaEmprestimo(int item, int locatario)
        {
            Emprestimo emp = new Emprestimo();
            emp.Codigo = numEmp;
            emp.CodigoLivro = item;
            emp.CodigoPessoa = locatario;
            return emp;
        }

        private bool GetType(IItem item) => item.GetType().Equals(typeof(Pessoa));

        private List<IListavel> RemoverListaItem(IItem item)
        {
            if (GetType(item))
            {
                listaPessoa.Remove((Pessoa)item);
                return new List<IListavel>(listaPessoa);
            }
            else
            {
                listaLivros.Remove((Livro)item);
                return new List<IListavel>(listaLivros);
            }

        }

        private void RemoveNumItem(IItem item)
        {
            if (GetType(item))
            {
                numPessoas--;
            }
            else
            {
                numLivros--;
            }
        }


    }
}
