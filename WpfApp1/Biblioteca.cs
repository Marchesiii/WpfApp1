using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Biblioteca
    {
        public ObservableCollection<Pessoa> listaPessoa;
        public ObservableCollection<Livro> listaLivros;
        private int numPessoas;
        private int numLivros;

        public Biblioteca()
        {
            listaPessoa = ListaStaticPessoa.listaStaticPessoa;
            listaLivros = ListaStaticLivro.listaStaticLivro;
            numPessoas = 0;
            numLivros = 0;
        }

        public void EmprestarLivro(Livro livro, Pessoa pessoa)
        {
            livro.SetPessoas(pessoa);
            pessoa.AddLivrosEmprestados(livro);
        }

        public void DevolveLivro(Livro livro, Pessoa pessoa)
        {
            pessoa.RemoveLivroEmprestado(livro);
            livro.SetPessoas(null);
        }

        public ObservableCollection<Pessoa> SubstituiPessoa(Pessoa pessoaInt, Pessoa pessoaOri)
        {
            listaPessoa.Remove(pessoaOri);
            listaPessoa.Add(pessoaInt);
            return listaPessoa;
        }

        public ObservableCollection<Livro> SubstituiLivro(Livro livroInt, Livro livroOri)
        {
            listaLivros.Remove(livroOri);
            listaLivros.Add(livroInt);
            return listaLivros;
        }

        public ObservableCollection<Livro> AddLivro(Livro livro)
        {
            AddNumLivros();
            listaLivros.Add(livro.Setcodigo(numLivros));
            return listaLivros;
        }

        public ObservableCollection<IListable> AddItem(IListable ìtem)
        {
            if (ìtem.TipoPessoa())
            {
                return new ObservableCollection<IListable>(AddPessoa((Pessoa)ìtem));
            }
            return new ObservableCollection<IListable>(AddLivro((Livro)ìtem));
        }

        internal ObservableCollection<Pessoa> AddPessoa(Pessoa pessoa)
        {
            AddNumPessoa();
            listaPessoa.Add(pessoa.SetCodigo(numPessoas));
            return listaPessoa;
        }

        public ObservableCollection<IListable> RemoverItem(IListable itemSelecionado)
        {
            if (itemSelecionado.TipoPessoa())
            {
                RemoverPessoa((Pessoa)itemSelecionado);
                return new ObservableCollection<IListable>(listaPessoa);
                
            }
            else
            {
                RemoverLivro((Livro)itemSelecionado);
                return new ObservableCollection<IListable>(listaLivros); 
            }
        }

        public bool RemoverPessoa(Pessoa pessoa)
        {
            if (pessoa != null)
            {
                listaPessoa.Remove(pessoa);
                RemoveNumPessoa();
                return true;
            } else
            {
                return false;
            }

        }

        public bool RemoverLivro(Livro livro)
        {
            if (livro != null)
            {
                listaLivros.Remove(livro);
                RemoveNumLivros();
                return true;
            } else
            {
                return false;
            }

        }

        public Biblioteca AddNumPessoa()
        {
            numPessoas++;
            return this;
        }

        public Biblioteca AddNumLivros()
        {
            numLivros++;
            return this;
        }

        public Biblioteca RemoveNumLivros()
        {
            return this;
        }

        public Biblioteca RemoveNumPessoa()
        {
            return this;
        }

        public ObservableCollection<IListable> SubstituiItem(IListable itemClone, IListable item)
        {
            if (item.TipoPessoa())
            {
                return new ObservableCollection<IListable>(SubstituiPessoa((Pessoa)itemClone,(Pessoa) item));
            } else
            {
                return new ObservableCollection<IListable>(SubstituiLivro((Livro)itemClone,(Livro) item));
            }
        }
    }

    internal static class ListaStaticPessoa
    {
        public static ObservableCollection<Pessoa> listaStaticPessoa = new ObservableCollection<Pessoa>();
    }

    internal static class ListaStaticLivro
    {
        public static ObservableCollection<Livro> listaStaticLivro = new ObservableCollection<Livro>();
    }
}
