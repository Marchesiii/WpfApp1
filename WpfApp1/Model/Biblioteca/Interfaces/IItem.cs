using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp1.Model.Biblioteca.Interfaces
{
    public interface IItem
    {
        public int Codigo { get; set; }
        public bool Check(PseudoExc ex);
        public bool Ocupado();
        public IItem Clone();

    }
}
