using System.Windows;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public class Livro : IItem
    {
        private int codigo;
        private string nomeCompleto;
        private int pags;
        private string autor;
        private Pessoa? pessoa;

        public Livro()
        {
            pessoa = null;
            codigo = 0;
            nomeCompleto = string.Empty;
            autor = string.Empty;
            pags = 0;
        }
        public int Codigo { get => codigo; set { codigo = value; } }
        public Pessoa? Pessoa { get => pessoa; set { pessoa = value; } }
        public int Pags { get => pags; set { pags = value; } }
        public string NomeCompleto
        {
            get => nomeCompleto;

            set
            {
                if (Valids.CheckStringNullOrEmpty(value))
                {
                    Valids.DisplayValidationError("Precisamos definir um nome para o Livro.");
                }
                else
                {
                    nomeCompleto = value;
                }
            }
        }

        public string Autor
        {
            get => autor;
            set
            {
                if (Valids.CheckStringNullOrEmpty(value))
                {
                    Valids.DisplayValidationError("Precisamos definir um autor.");
                }
                else
                {
                    autor = value;
                }
            }
        }

        public bool Ocupado() => pessoa != null;

        public bool Check(PseudoExc ex)
        {
            if (!Valids.CheckStringNullOrEmpty(nomeCompleto))
            {
                if (!Valids.CheckStringNullOrEmpty(autor))
                {
                    if (Valids.CheckIsPositive(pags))
                    {
                        return true;
                    }
                    else
                    {
                        ex.ex = "O numero de paginas precsa ser positivo.";
                    }
                }
                else
                {
                    ex.ex = "O Autor precisa ser preenchido.";
                }
            }
            else
            {
                ex.ex = "O Nome precisa ser preenchido.";
            }
            return false;
        }

        public IItem Clone()
        {
            Livro clone = (Livro)MemberwiseClone();
            return clone;
        }
    }

}
