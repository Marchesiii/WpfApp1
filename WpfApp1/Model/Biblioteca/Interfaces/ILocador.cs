using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model.Biblioteca.Interfaces
{
    public interface ILocador
    {
        List<IListavel> AddItem(IItem item);
        bool DevolverItem(IItem item);
        bool EmprestarItem(IItem item, IItem locatario);
        Emprestimo? ProcuraEmprestimo(IItem item);
        List<IListavel> ListaItens(Type type);
        List<IListavel> RemoverItem(IItem itemSelecionado);
        List<IListavel> SubstituiItem(IItem itemClone, IItem item);
    }
}
