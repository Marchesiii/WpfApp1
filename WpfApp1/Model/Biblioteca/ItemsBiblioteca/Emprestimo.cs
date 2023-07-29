using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Emprestimo : IListavel
    {
        public int Codigo { get => codigo; set { codigo = value; } }
        public int CodigoLivro { get => codigoLivro; set {codigoLivro = value; } }
        public int CodigoPessoa { get => codigoPessoa; set { codigoPessoa = value; } }

        private int codigo;
        private int codigoLivro;
        private int codigoPessoa;

        public Emprestimo()
        {
            codigo = 0;
            codigoLivro = 0;
            codigoPessoa = 0;
        }
            
    }
}
