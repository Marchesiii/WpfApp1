
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public class Pessoa : IItemLocador
    {
        private string nomeCompleto;
        private int codigo;
        private bool ocupado;

        public Pessoa()
        {
            nomeCompleto = "";
            codigo = 0;
            ocupado = false;
        }


        public string NomeCompleto
        {
            get => nomeCompleto;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show(ValidsStrings.ErroNomeEmptyOrNull);
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
        public bool Ocupado() => ocupado;
        public void SetOcupado(bool ocupadoExt)
        {
            ocupado = ocupadoExt;
        }
        public bool Check(PseudoExc ex)
        {
            if (string.IsNullOrEmpty(nomeCompleto))
            {
                ex.ex = ValidsStrings.ErroNomeEmptyOrNull;
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
