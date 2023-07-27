using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public class Biblioteca : ILocatario
    {
        public List<IItem> ListaPessoa { get => new List<IItem>(listaPessoa); private set { } }
        public List<IItem> ListaLivros { get => new List<IItem>(listaLivros); private set { } }
        private List<Pessoa> listaPessoa;
        private List<Livro> listaLivros;
        private int numPessoas;
        private int numLivros;


        public Biblioteca()
        {
            listaPessoa = new List<Pessoa>();
            listaLivros = new List<Livro>();
            numPessoas = 0;
            numLivros = 0;
        }

        private bool GetType(IItem item) => item.GetType().Equals(typeof(Pessoa));

        public void EmprestarItem(IItem item, IItem locador)
        {
            ((Livro)item).Pessoa = (Pessoa)locador;
            ((Pessoa)locador).LivrosEmprestados.Add((Livro)item);
        }

        public void DevolverItem(IItem item, IItem devolutor)
        {
            ((Pessoa)devolutor).LivrosEmprestados.Remove((Livro)item);
            ((Livro)item).Pessoa = null;
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

        public List<IItem> AddItem(IItem item)
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

        public List<IItem> AddListaItem(IItem item)
        {
            if (GetType(item))
            {
                listaPessoa.Add((Pessoa)item);
                return new List<IItem>(listaPessoa);
            }
            else
            {
                listaLivros.Add((Livro)item);
                return new List<IItem>(listaLivros);
            }
        }


        public List<IItem> RemoverItem(IItem itemSelecionado)
        {
            List<IItem> lista = RemoverListaItem(itemSelecionado);
            RemoveNumItem(itemSelecionado);
            return lista;
        }

        public List<IItem> SubstituiItem(IItem itemClone, IItem item)
        {
            if (GetType(item))
            {
                return new List<IItem>(SubstituiPessoa((Pessoa)itemClone, (Pessoa)item));
            }
            else
            {
                return new List<IItem>(SubstituiLivro((Livro)itemClone, (Livro)item));
            }
        }

        public List<IItem> ListaItens(Type type)
        {
            if (type.Equals(typeof(Pessoa))){
                return ListaPessoa;
            }
            return ListaLivros;
        }

        private List<IItem> RemoverListaItem(IItem item)
        {
            if (GetType(item))
            {
                listaPessoa.Remove((Pessoa)item);
                return new List<IItem>(listaPessoa);
            }
            else
            {
                listaLivros.Remove((Livro)item);
                return new List<IItem>(listaLivros);
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
