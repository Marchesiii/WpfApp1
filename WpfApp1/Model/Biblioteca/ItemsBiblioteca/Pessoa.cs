﻿
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WpfApp1.Model.Biblioteca.ItemsBiblioteca
{
    public class Pessoa : IListable
    {
        private string nomeCompleto;
        private int codigo;
        private ObservableCollection<Livro> livrosEmprestados;

        public Pessoa()
        {
            nomeCompleto = "";
            codigo = 0;
            livrosEmprestados = new ObservableCollection<Livro>();
        }

        public string NomeCompleto
        {
            get { return nomeCompleto; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Precisamos definir um nome para a Pessoa.");
                }
                else
                {
                    nomeCompleto = value;
                }
            }
        }
        public int Codigo
        {
            get { return codigo; }
            set
            {
                codigo = value;
            }
        }
        public ObservableCollection<Livro> LivrosEmprestados { get { return livrosEmprestados; } private set { livrosEmprestados = value; } }


        public Pessoa SetNomeCompleto(string n)
        {
            nomeCompleto = n;
            return this;
        }

        public Pessoa SetCodigo(int c)
        {
            codigo = c;
            return this;
        }

        public string GetNomeCompleto()
        {
            return nomeCompleto;
        }


        public Pessoa AddLivrosEmprestados(Livro livro)
        {
            livrosEmprestados.Add(livro);
            return this;
        }

        public Pessoa RemoveLivroEmprestado(Livro livro)
        {
            livrosEmprestados.Remove(livro);
            return this;
        }

        public bool Check(PseudoExc ex)
        {
            if (string.IsNullOrEmpty(GetNomeCompleto()))
            {
                ex.ex = "Precisamos definir um nome para a Pessoa.";
                return false;
            }
            else
            {
                return true;
            }
        }

        public Window GetTelaCadastro()
        {
            return new TelaCadastro();
        }

        public Window GetTelaInfo()
        {
            return new TelaPessoa();
        }

        public bool TipoPessoa()
        {
            return true;
        }

        public bool Ocupado()
        {
            return livrosEmprestados.Any();
        }
    }
}