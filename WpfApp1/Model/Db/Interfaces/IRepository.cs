using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public interface IRepository
    {
        public bool Add(IItem item);
        public bool Update(IItem item);
        public bool Delete(IItem item);
        public bool DeleteAll(Type type);
        public IItem Get(Type type);
        public IItem GetById(string id);
        public IItem GetByName(string name);
    }
}
