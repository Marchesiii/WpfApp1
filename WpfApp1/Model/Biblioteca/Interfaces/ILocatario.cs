using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model.Biblioteca.Interfaces
{
    public interface ILocatario
    {
        List<IItem> AddItem(IItem item);
        void DevolverItem(IItem item, IItem devolutor);
        void EmprestarItem(IItem item, IItem locador);
        List<IItem> ListaItens(Type type);
        List<IItem> RemoverItem(IItem itemSelecionado);
        List<IItem> SubstituiItem(IItem itemClone, IItem item);
    }
}
