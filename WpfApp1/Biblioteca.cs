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

        public ObservableCollection<Livro> AddLivro(string nomeLivro, int pags, string autor)
        {
            Livro livro = new Livro().SetNome(nomeLivro).SetPags(pags).SetAutor(autor);
            return AddLivro(livro);
        }

        public ObservableCollection<Livro> AddLivro(Livro livro)
        {
            AddNumLivros();
            listaLivros.Add(livro.Setcodigo(numLivros));
            return listaLivros;
        }

        public ObservableCollection<Pessoa> AddPessoa(string nomeCompleto)
        {
            Pessoa pessoa = new Pessoa().SetNomeCompleto(nomeCompleto);
            return AddPessoa(pessoa);
        }

        internal ObservableCollection<Pessoa> AddPessoa(Pessoa pessoa)
        {
            AddNumPessoa();
            listaPessoa.Add(pessoa.SetCodigo(numPessoas));
            return listaPessoa;
        }

        public void RemoverPessoa(Pessoa pessoa)
        {
            if (pessoa != null)
            {
                listaPessoa.Remove(pessoa);
                RemoveNumPessoa();
            } else
            {
                throw new Exception("Não encontramos um pessoa com esse codigo para ser removido.");
            }

        }

        public void RemoverLivro(Livro livro)
        {
            if (livro != null)
            {
                listaLivros.Remove(livro);
                RemoveNumLivros();
            } else
            {
                throw new Exception("Não encontramos um livro com esse codigo para ser removido.");
            }

        }

        public Livro BuscaLivro(int codigo)
        {
            foreach (Livro item in listaLivros)
            {
                if (item.Getcodigo() == codigo)
                {
                    return item;
                }
            }
            return null;
        }

        public Pessoa BuscaPessoa(int codigo)
        {
            foreach (Pessoa item in listaPessoa)
            {
                if (item.GetCodigo() == codigo)
                {
                    return item;
                }
            }

            return null;
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

        public int getNumLivros()
        {
            return numLivros;
        }

        public int getNumPessoas()
        {
            return numPessoas;
        }

        //AtualizarDadosPessoa
        //ListarPessoas
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
