using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public interface IRepositoryItemOcupavel : IRepository
    {
        public IItem GetOcupados();
    }
}
