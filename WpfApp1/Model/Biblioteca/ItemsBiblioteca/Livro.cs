using System.Windows;

namespace WpfApp1.Model.Biblioteca.ItemsBiblioteca
{
    public class Livro : IListable
    {
        private int codigo;
        private string nomeCompleto;
        private int pags;
        private string autor;
        private Pessoa? pessoas;

        public Livro()
        {
            pessoas = null;
            codigo = 0;
            nomeCompleto = string.Empty;
            autor = string.Empty;
            pags = 0;
        }
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public Pessoa? Pessoas { get { return pessoas; } set { pessoas = value; } }
        public string NomeCompleto
        {
            get { return nomeCompleto; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Precisamos definir um nome para o Livro.");
                }
                else
                    nomeCompleto = value;

            }
        }
        public string Autor
        {
            get { return autor; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Precisamos definir um autor.");
                }
                else
                    autor = value;
            }
        }
        public int Pags { get { return pags; } set { pags = value; } }

        public Pessoa? GetPessoas()
        {
            return pessoas;
        }

        public string GetNome()
        {
            return nomeCompleto;
        }

        public int Getcodigo()
        {
            return codigo;
        }

        public string GetAutor()
        {
            return autor;
        }

        public int GetPags()
        {
            return pags;
        }

        public Livro SetPessoas(Pessoa pessoasInt)
        {
            pessoas = pessoasInt;
            return this;
        }

        public Livro Setcodigo(int codigoInt)
        {
            codigo = codigoInt;
            return this;
        }

        public Livro SetNome(string nomeInt)
        {
            nomeCompleto = nomeInt;
            return this;
        }

        public Livro SetAutor(string AutorInt)
        {
            autor = AutorInt;
            return this;
        }

        public Livro SetPags(int PagsInt)
        {
            pags = PagsInt;
            return this;
        }

        public bool TipoPessoa()
        {
            return false;
        }

        public bool Check(PseudoExc ex)
        {
            if (!string.IsNullOrEmpty(GetAutor()))
            {
                if (!string.IsNullOrEmpty(GetNome()))
                {
                    if (int.IsPositive(GetPags()))
                    {
                        return true;
                    }
                    else
                    {
                        ex.ex = "O numero de pagnas precsa ser positivo.";
                    }
                }
                else
                {
                    ex.ex = "O Nome precisa ser preenchido.";
                }
            }
            else
            {
                ex.ex = "O Autor precisa ser preenchido.";
            }
            return false;
        }

        public Window GetTelaCadastro()
        {
            return new TelaCadastroLivro();
        }

        public Window GetTelaInfo()
        {
            return new TelaLivro();
        }
        public bool Ocupado()
        {
            return pessoas != null;
        }
    }

}
