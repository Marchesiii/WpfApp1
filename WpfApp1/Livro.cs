using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public class Livro : IListable
    {
        private int codigo;
        private string nomeCompleto;
        private int pags;
        private string autor;
        public Pessoa Pessoas { get { return pessoas; } set { pessoas = value; } }
        private Pessoa pessoas;
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string NomeCompleto { get { return nomeCompleto; } set {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Precisamos definir um nome para o Livro.");
                }
                else
                    nomeCompleto = value; 

            } }
        public string Autor { get {return autor; } set {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Precisamos definir um autor.");
                }
                else
                    autor = value;} }
        public int Pags { get { return pags; } set { pags = value; } }

        public Livro()
        {
            pessoas = null;
            codigo = 0;
            nomeCompleto = string.Empty;
            autor = string.Empty;
            pags = 0;
        }

        public Pessoa GetPessoas()
        {
            return pessoas;
        }

        public string GetNome()
        {
            return nomeCompleto;
        }

        public int Getcodigo()
        {
            return codigo;
        }

        public string GetAutor()
        {
            return autor;
        }

        public int GetPags()
        {
            return pags;
        }

        public Livro SetPessoas(Pessoa pessoasInt)
        {
            pessoas = pessoasInt;
            return this;
        }

        public Livro Setcodigo(int codigoInt)
        {
            codigo = codigoInt;
            return this;
        }

        public Livro SetNome(string nomeInt)
        {
            nomeCompleto = nomeInt;
            return this;
        }

        public Livro SetAutor(string AutorInt)
        {
            autor = AutorInt;
            return this;
        }

        public Livro SetPags(int PagsInt)
        {
            pags = PagsInt;
            return this;
        }

        public bool TipoPessoa()
        {
            return false;
        }

        public static void CheckLivro(Livro livro)
        {
            if (!string.IsNullOrEmpty(livro.GetAutor()))
            {
                if (!string.IsNullOrEmpty(livro.GetNome()))
                {
                    if (int.IsPositive(livro.GetPags()))
                    {
                        return;
                    }
                }
            }
            throw new ArgumentException("Precisamos definir todos os campos antes de salvar.");
        }

        public Livro Clone()
        {
            Livro obj = new Livro();
            obj.autor = autor;
            obj.nomeCompleto = nomeCompleto;
            obj.pags = pags;
            obj.pessoas = pessoas;
            return obj;
        }
    }

}
