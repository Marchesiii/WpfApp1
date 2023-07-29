using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model.Biblioteca.Interfaces
{
    public interface ILocador
    {
        List<IItem> AddItem(IItem item);
        bool DevolverItem(IItem item);
        bool EmprestarItem(IItem item, IItem locatario);
        Emprestimo? ProcuraEmprestimo(IItem item);
        List<IItem> ListaItens(Type type);
        List<IItem> RemoverItem(IItem itemSelecionado);
        List<IItem> SubstituiItem(IItem itemClone, IItem item);
    }
}
