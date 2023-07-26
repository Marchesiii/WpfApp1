using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp1.Model.Biblioteca
{
    public interface IListable
    {
        public bool TipoPessoa();

        public bool Check(PseudoExc ex);

        public Window GetTelaCadastro();

        public Window GetTelaInfo();
        public bool Ocupado();
    }
}
