
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public class Pessoa : IItem
    {
        private string nomeCompleto;
        private int codigo;
        private List<Livro> livrosEmprestados;

        public Pessoa()
        {
            nomeCompleto = "";
            codigo = 0;
            livrosEmprestados = new List<Livro>();
        }

        public List<Livro> LivrosEmprestados { get => livrosEmprestados; private set { livrosEmprestados = value; } }

        public string NomeCompleto
        {
            get => nomeCompleto;
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
            get => codigo;
            set
            {
                codigo = value;
            }
        }
        public bool Ocupado() => livrosEmprestados.Any();

        public bool Check(PseudoExc ex)
        {
            if (string.IsNullOrEmpty(nomeCompleto))
            {
                ex.ex = "Precisamos definir um nome para a Pessoa.";
                return false;
            }
            else
            {
                return true;
            }
        }

        public IItem Clone()
        {
            Pessoa clone = (Pessoa)MemberwiseClone();
            return clone;
        }
    }
}
