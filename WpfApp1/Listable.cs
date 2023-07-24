using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public static class Listable
    {
        //Futuramente tem que ser atomico em!

        public static ObservableCollection<IListable> AlteraItem(IListable item, Biblioteca biblioteca)
        {
            ObservableCollection<IListable> result = new();
            IListable itemClone = item;
            bool dialogResult = true;
            while (dialogResult)
            {
                Window tela = itemClone.GetTelaCadastro();
                tela.DataContext = itemClone;
                tela.ShowDialog();
                dialogResult = (bool)tela.DialogResult;
                if(dialogResult)
                {
                    PseudoExc ex = new PseudoExc();
                    if(itemClone.Check(ex)){
                        return biblioteca.SubstituiItem(itemClone, item);
                    } else
                    {
                        MessageBox.Show(ex.ex);
                    }
                }
            }
            return result;
        }

        public static ObservableCollection<IListable> CadastraItem(IListable item, Biblioteca biblioteca)
        {
            bool dialogResult = true;
            ObservableCollection<IListable> listaResult = new ObservableCollection<IListable>();
            while (dialogResult)
            {
                Window tela = item.GetTelaCadastro();
                tela.DataContext = item;
                tela.ShowDialog();
                dialogResult = (bool)tela.DialogResult;
                if (dialogResult)
                {
                    PseudoExc exc = new PseudoExc();
                    if (item.Check(exc))
                    {
                        return new ObservableCollection<IListable>(biblioteca.AddItem(item));
                    }
                    else
                    {
                        MessageBox.Show(exc.ex);
                    }
                }
            }
            return listaResult;
        }

        public static ObservableCollection<IListable> RemoveItem(IListable itemSelecionado, Biblioteca biblioteca)
        {
            return biblioteca.RemoverItem(itemSelecionado);
        }
    }
}
