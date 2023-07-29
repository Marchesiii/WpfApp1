using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class TelaInfoVm
    {
        public int Codigo { get; set; }
        public string NomeCompleto { get; set; }
        public string Autor { get; set; }
        public int Pags { get; set; }
        public List<IItemLocado> LivrosEmprestados { get; set; }
        public IItemLocador Pessoa { get; set; }
        public TelaInfoVm() { }
    }
}
