namespace WpfApp1.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TesteCriaEmprestimo()
        {
            Emprestimo emprestimo = CriaEmprestimoAdicionaParametros();
            bool result = emprestimo.Codigo == 0 && emprestimo.CodigoLivro == 0 && emprestimo.CodigoPessoa == 0;
            Assert.True(result);
        }

        [Test]
        public void TestCreateLivro()
        {
            Livro livro = CriaLivroAdicionaParametros(null, null, null, null);
            bool result = livro.Codigo == 0 && string.IsNullOrEmpty(livro.NomeCompleto) && string.IsNullOrEmpty(livro.Autor) && livro.Pags == 0 && livro.Ocupado() == false;
            Assert.True(result);
        }

        [Test]
        public void TestCreatePessoa()
        {
            Pessoa pessoa = CriaPessoaVaziaAdicionaNomeECodigo(null, null);
            bool result = pessoa.Codigo == 0 && string.IsNullOrEmpty(pessoa.NomeCompleto) && !pessoa.Ocupado();
            Assert.True(result);
        }

        [Test]
        public void TestCheckLivroNome()
        {
            Livro livro = CriaLivroAdicionaParametros(null, null, null, null);
            Assert.True(TesteCheckItemBase(livro, ValidsStrings.ErroNomeEmptyOrNull));

        }

        [Test]
        public void TestCheckLivroAutor()
        {
            Livro livro = CriaLivroAdicionaParametros(null, "a", null, null);
            Assert.True(TesteCheckItemBase(livro, ValidsStrings.ErroAutorEmptyOrNull));
        }

        [Test]
        public void TestCheckLivroPags()
        {
            Livro livro = CriaLivroAdicionaParametros(null, "Nome", "Autor", -1);
            Assert.True(TesteCheckItemBase(livro, ValidsStrings.ErroPagsNegativo));
        }

        [Test]
        public void TesteCheckLivroClone()
        {
            Livro livro = CriaLivroAdicionaParametros(2, "Nome", "Autor", 10);
            Livro livro2 = (Livro)livro.Clone();
            bool result = livro.Codigo.Equals(livro2.Codigo) && livro.NomeCompleto.Equals(livro2.NomeCompleto) && livro.Autor.Equals(livro2.Autor) && livro.Pags.Equals(livro2.Pags);
            Assert.True(result);
        }

        [Test]
        public void TesteCheckPessoaNome()
        {
            Pessoa pessoa = CriaPessoaVazia();
            Assert.True(TesteCheckItemBase(pessoa, ValidsStrings.ErroNomeEmptyOrNull));
        }

        [Test]
        public void TesteCheckPessoaClone()
        {
            Pessoa pessoa = CriaPessoaVaziaAdicionaNomeECodigo("Teste", 2);
            Pessoa pessoa2 = (Pessoa)pessoa.Clone();
            bool result = pessoa.Codigo.Equals(pessoa2.Codigo) && pessoa.NomeCompleto.Equals(pessoa2.NomeCompleto);
            Assert.True(result);
        }


        [Test]
        public void TestCreateBiblioteca()
        {
            Biblioteca biblioteca = CriaBibliotecaVazia();
            Assert.True(!biblioteca.ListaPessoa.Any() && !biblioteca.ListaLivros.Any());
        }

        [Test]
        public void TestEmprestarBiblioteca()
        {
            Pessoa pessoa = CriaPessoaVaziaAdicionaNomeECodigo("Pessoa1", null);
            Livro livro = CriaLivroAdicionaParametros(null, "Novo Livro", "Novo Autor", 2);
            CriaBibliotecaComPessoaELivro(pessoa, livro).EmprestarItem(livro, pessoa);
            Assert.True(livro.Ocupado() && pessoa.Ocupado());
        }

        [Test]
        public void TestDevolverBiblioteca()
        {
            Pessoa pessoa = CriaPessoaVaziaAdicionaNomeECodigo("Pessoa1", null);
            Livro livro = CriaLivroAdicionaParametros(null, "Novo Livro", "Novo Autor", 2);
            Biblioteca biblioteca = CriaBibliotecaComPessoaELivro(pessoa, livro);
            biblioteca.EmprestarItem(livro, pessoa);
            biblioteca.DevolverItem(livro);
            Assert.True(!livro.Ocupado() && !pessoa.Ocupado());
        }

        [Test]
        public void TestDevolverBibliotecaMaisDeUmItem()
        {
            Pessoa pessoa = CriaPessoaVaziaAdicionaNomeECodigo("Pessoa1", null);
            Livro livro = CriaLivroAdicionaParametros(null, "Novo Livro", "Novo Autor", 2);
            Livro livro2 = CriaLivroAdicionaParametros(null, "Novo Livro", "Novo Autor", 2);
            Biblioteca biblioteca = CriaBibliotecaComPessoaELivro(pessoa, livro);
            biblioteca.AddItem(livro2);
            biblioteca.EmprestarItem(livro, pessoa);
            biblioteca.EmprestarItem(livro2, pessoa);
            biblioteca.DevolverItem(livro);
            Assert.True(!livro.Ocupado() && pessoa.Ocupado());
        }

        [Test]
        public void TestEmprestarDuasVezesBibliioteca()
        {
            Pessoa pessoa = CriaPessoaVaziaAdicionaNomeECodigo("Pessoa1", null);
            Pessoa pessoa2 = CriaPessoaVaziaAdicionaNomeECodigo("Pessoa2", null);
            Livro livro = CriaLivroAdicionaParametros(null, "Novo Livro", "Novo Autor", 2);
            Biblioteca biblioteca = CriaBibliotecaComPessoaELivro(pessoa, livro);
            biblioteca.AddItem(pessoa2);
            Assert.True(biblioteca.EmprestarItem(livro, pessoa) && livro.Ocupado() && !biblioteca.EmprestarItem(livro, pessoa2) && !pessoa2.Ocupado());
        }

        [Test]
        public void TestSubstituiItemBiblioteca()
        {
            IItem pessoa = CriaPessoaVaziaAdicionaNomeECodigo("Pessoa1", null);
            IItem livro = CriaLivroAdicionaParametros(null, "Novo Livro", "Novo Autor", 2);
            Biblioteca biblioteca = CriaBibliotecaComPessoaELivro((Pessoa)pessoa, (Livro)livro);
            IItem pessoa2 = pessoa.Clone();
            IItem livro2 = livro.Clone();
            ((Pessoa)pessoa2).NomeCompleto = "Pessoa2";
            ((Livro)livro2).NomeCompleto = "Livro2";
            biblioteca.SubstituiItem(pessoa2, pessoa);
            biblioteca.SubstituiItem(livro2, livro);
            bool result1 = ((Pessoa)biblioteca.ListaPessoa.First()).NomeCompleto.Equals("Pessoa2");
            bool result2 = ((Livro)biblioteca.ListaLivros.First()).NomeCompleto.Equals("Livro2");
            Assert.True(result1 && result2);
        }

        [Test]
        public void TestAddItem()
        {
            IItem pessoa = CriaPessoaVaziaAdicionaNomeECodigo("Pessoa1", null);
            IItem livro = CriaLivroAdicionaParametros(null, "Novo Livro", "Novo Autor", 2);
            Biblioteca biblioteca = CriaBibliotecaComPessoaELivro((Pessoa)pessoa, (Livro)livro);
            bool result1 = biblioteca.ListaPessoa.First().Equals(pessoa);
            bool result2 = biblioteca.ListaLivros.First().Equals(livro);
            Assert.True(result1 && result2);
        }

        [Test]
        public void TesteRemoveItem()
        {
            IItem pessoa = CriaPessoaVaziaAdicionaNomeECodigo("Pessoa1", null);
            IItem livro = CriaLivroAdicionaParametros(null, "Novo Livro", "Novo Autor", 2);
            Biblioteca biblioteca = CriaBibliotecaComPessoaELivro((Pessoa)pessoa, (Livro)livro);
            biblioteca.RemoverItem(pessoa);
            biblioteca.RemoverItem(livro);
            bool result1 = biblioteca.ListaPessoa.Any();
            bool result2 = biblioteca.ListaLivros.Any();
            Assert.True(!result1 && !result2);
        }

        [Test]
        public void TesteListaItens()
        {
            IItem pessoa = CriaPessoaVaziaAdicionaNomeECodigo("Pessoa1", null);
            IItem livro = CriaLivroAdicionaParametros(null, "Novo Livro", "Novo Autor", 2);
            Biblioteca biblioteca = CriaBibliotecaComPessoaELivro((Pessoa)pessoa, (Livro)livro);
            Assert.True(biblioteca.ListaItens(typeof(IItemLocador)).First().Equals(pessoa));
        }

        private static Emprestimo CriaEmprestimoAdicionaParametros()
        {
            return new Emprestimo();
        }

        private static Biblioteca CriaBibliotecaVazia()
        {
            return new Biblioteca();
        }

        private static Biblioteca CriaBibliotecaComPessoaELivro(Pessoa pessoa, Livro livro)
        {
            Biblioteca biblioteca = CriaBibliotecaVazia();
            if(pessoa == null)
            {
                pessoa = CriaPessoaVaziaAdicionaNomeECodigo("Pessoa1", null);
            }
            if(livro == null)
            {
                livro = CriaLivroAdicionaParametros(1, "Novo Livro", "Novo Autor", null);
            }
            biblioteca.AddItem(pessoa);
            biblioteca.AddItem(livro);
            return biblioteca;
        }


        private static Livro CriaLivroVazio()
        {
            return new();
        }

        private static Livro LivroAdicionaCodigo(Livro livro, int? codigo)
        {
            if(codigo != null)
            {
                livro.Codigo = (int)codigo;
            }
            return livro;
        }

        private static Livro LivroAdicionaNome(Livro livro, string? nome)
        {
            if(nome != null)
            {
                livro.NomeCompleto = nome;
            }
            return livro;
        }

        private static Livro LivroAdicionaAutor(Livro livro, string? autor)
        {
            if(autor != null)
            {
                livro.Autor = autor;
            }
            return livro;
        }

        private static Livro LivroAdicionaPags(Livro livro, int? pags)
        {
            if(pags != null)
            {
                livro.Pags = (int)pags;
            }
            return livro;
        }

        private static Livro CriaLivroAdicionaParametros(int? codigo, string? nome, string? autor, int? pags)
        {
            Livro livro = CriaLivroVazio();
            LivroAdicionaCodigo(livro, codigo);
            LivroAdicionaNome(livro, nome);
            LivroAdicionaPags(livro, pags);
            LivroAdicionaAutor(livro, autor);
            LivroAdicionaPags(livro, pags);
            return livro;

        }

        private static Pessoa CriaPessoaVazia()
        {
            return new();
        }

        private static Pessoa PessoaAdicionaCodigo(Pessoa pessoa, int? codigo)
        {
            if(codigo != null)
            {
                pessoa.Codigo = (int)codigo;
            }
            return pessoa;
        }

        private static Pessoa PessoaAdicionaNome(Pessoa pessoa, string nome)
        {
            if(nome != null)
            {
                pessoa.NomeCompleto = nome;
            }
            return pessoa;
        }

        private static Pessoa CriaPessoaVaziaAdicionaNomeECodigo(string? nome, int? codigo)
        {
            Pessoa pessoa = CriaPessoaVazia();
            PessoaAdicionaCodigo(pessoa, codigo);
            PessoaAdicionaNome(pessoa, nome);
            return pessoa;
        }


        public bool TesteCheckItemBase(IItem item, string valid)
        {
            PseudoExc exc = new();
            return !item.Check(exc) && exc.ex.Equals(valid);
        }


    }
}