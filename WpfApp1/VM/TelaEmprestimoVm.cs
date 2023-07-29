using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public class TelaEmprestimoVm
    {
        public ObservableCollection<IItemListavel> ListaListable { get; set; }
        public IItemListavel? ItemSelecionado { get; set; }
        public ILocador EmpresaLocadora;
        public IItemLocado Item { get; set; }
        public bool Tentou { get; set; }

        public TelaEmprestimoVm()
        {
            Tentou = false;
        }

        public bool EmprestarItem()
        {
            Tentou = true;
            if (ItemSelecionado != null)
            {
                EmpresaLocadora.EmprestarItem(Item, (IItemLocador)ItemSelecionado);
                MessageBox.Show("Livro emprestado para: " + ((Pessoa)ItemSelecionado).NomeCompleto);
                return true;
            }
            else
            {
                MessageBox.Show("Selecione ao menos uma pessoa para efetivar o emprestimo ou feche a aba.");
                return false;
            }
        }
    }
}
