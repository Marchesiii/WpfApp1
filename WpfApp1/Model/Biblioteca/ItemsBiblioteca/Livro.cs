using System.Windows;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public class Livro : IItemLocado
    {
        private int codigo;
        private string nomeCompleto;
        private int pags;
        private string autor;
        private bool ocupado;

        public Livro()
        {
            ocupado = false;
            codigo = 0;
            nomeCompleto = string.Empty;
            autor = string.Empty;
            pags = 0;
        }
        public int Codigo { get => codigo; set { codigo = value; } }
        public int Pags { get => pags; set { pags = value; } }
        public string NomeCompleto
        {
            get => nomeCompleto;

            set
            {
                if (Valids.CheckStringNullOrEmpty(value))
                {
                    Valids.DisplayValidationError(ValidsStrings.ErroNomeEmptyOrNull);
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
                    Valids.DisplayValidationError(ValidsStrings.ErroAutorEmptyOrNull);
                }
                else
                {
                    autor = value;
                }
            }
        }

        public bool Ocupado() => ocupado;

        public void SetOcupado(bool ocupadoExt)
        {
            ocupado = ocupadoExt;
        }
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
                        ex.ex = ValidsStrings.ErroPagsNegativo;
                    }
                }
                else
                {
                    ex.ex = ValidsStrings.ErroAutorEmptyOrNull;
                }
            }
            else
            {
                ex.ex = ValidsStrings.ErroNomeEmptyOrNull;
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
