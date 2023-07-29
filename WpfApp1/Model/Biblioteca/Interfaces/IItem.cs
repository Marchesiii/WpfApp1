using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp1
{
    public interface IItem : IItemListavel
    {
        public int Codigo { get; set; }
        public string NomeCompleto { get; set; }
        public bool Check(PseudoExc ex);
        public bool Ocupado();
        public IItem Clone();

    }
}
