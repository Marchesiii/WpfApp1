using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public static class Listable
    {
     //Futuramente tem que ser atomico em!
        public static ObservableCollection<IListable> AlteraPessoa(Pessoa pessoa, Biblioteca biblioteca) 
        {
            Pessoa pessoaClone = pessoa.Clone();
            bool dialogResult = true;
            while (dialogResult)
            {
                TelaCadastro telaCadastro = new TelaCadastro
                {
                    DataContext = pessoaClone
                };
                telaCadastro.ShowDialog();
                dialogResult = (bool)telaCadastro.DialogResult;
                if (dialogResult)
                {
                    try
                    {
                        Pessoa.CheckPessoa(pessoaClone);
                        return new ObservableCollection<IListable>(biblioteca.SubstituiPessoa(pessoaClone, pessoa));
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            return new ObservableCollection<IListable>(biblioteca.listaPessoa);
        }

        public static ObservableCollection<IListable> AlteraLivro(Livro livro, Biblioteca biblioteca)
        {
            Livro livroClone = livro.Clone();
            bool dialogResult = true;
            while(dialogResult) {
                TelaCadastro telaCadastro = new TelaCadastro
                {
                    DataContext = livroClone
                };
                telaCadastro.ShowDialog();
                dialogResult = (bool)telaCadastro.DialogResult;
                if (dialogResult)
                {
                    try
                    {
                        Livro.CheckLivro(livroClone);
                        return new ObservableCollection<IListable>(biblioteca.SubstituiLivro(livroClone, livro));
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            return new ObservableCollection<IListable>(biblioteca.listaLivros);
        }
        public static ObservableCollection<IListable> CadastraPessoa(Biblioteca biblioteca)
        {
            Pessoa pessoa = new Pessoa();
            bool dialogResult = true;
            while (dialogResult)
            {
                TelaCadastro telaCadastro = new TelaCadastro
                {
                    DataContext = pessoa
                };
                telaCadastro.ShowDialog();
                dialogResult = (bool)telaCadastro.DialogResult;
                if (dialogResult)
                {
                    try
                    {
                        Pessoa.CheckPessoa(pessoa);
                        return new ObservableCollection<IListable>(biblioteca.AddPessoa(pessoa));
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            return new ObservableCollection<IListable>(biblioteca.listaPessoa);
        }

        public static ObservableCollection<IListable> CadastraLivro(Biblioteca biblioteca)
        {
            Livro livro = new Livro();
            bool dialogResult = true;
            while (dialogResult)
            {
                TelaCadastroLivro telaCadastro = new TelaCadastroLivro
                {
                    DataContext = livro
                };
                telaCadastro.ShowDialog();
                dialogResult = (bool)telaCadastro.DialogResult;
                if (dialogResult)
                {   
                    try
                    {
                        Livro.CheckLivro(livro);
                        return new ObservableCollection<IListable>(biblioteca.AddLivro(livro));
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            return new ObservableCollection<IListable>(biblioteca.listaLivros);
        }

        public static ObservableCollection<IListable> RemovePessoa(Pessoa pessoaInt, Biblioteca biblioteca)
        {
            biblioteca.RemoverPessoa(pessoaInt);
            return new ObservableCollection<IListable>(biblioteca.listaPessoa);
        }

        public static ObservableCollection<IListable> RemoveLivro(Livro livroInt, Biblioteca biblioteca)
        {
            biblioteca.RemoverLivro(livroInt);
            return new ObservableCollection<IListable>(biblioteca.listaLivros);
        }
    }
}
