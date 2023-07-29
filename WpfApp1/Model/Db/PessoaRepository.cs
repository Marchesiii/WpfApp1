using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public class PessoaRepository : IRepositoryItemOcupavel
    {
        public bool Add(IItem item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IItem item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAll(Type type)
        {
            throw new NotImplementedException();
        }

        public IItem Get(Type type)
        {
            throw new NotImplementedException();
        }

        public IItem GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IItem GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IItem GetOcupados()
        {
            throw new NotImplementedException();
        }

        public bool Update(IItem item)
        {
            throw new NotImplementedException();
        }
    }
}
