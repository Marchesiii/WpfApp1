using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public class MainWindowVm : INotifyPropertyChanged
    {
        public ObservableCollection<Pessoa> vmPessoas { get; set; }
        public ObservableCollection<Livro> vmLivros { get; set; }
        public ObservableCollection<IListable> ListaListable { get; set; }
        private Biblioteca Biblioteca { get; set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Update { get; private set; }
        public ICommand Peek { get; private set; }
        public ICommand Emprestar { get; set; }
        public ICommand Devolver { get; set; }
        public Pessoa PessoaSelecionada { get; set; }
        public Livro LivroSelecionado { get; set; }
        public IListable ItemSelecionado { get; set; }

        public string TipoSelecionado { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowVm() {
            Biblioteca = new Biblioteca();
            IniciaCommandos();
        }

        public void Notifica(string nome)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }

        public bool tipoPessoas()
        {
            if (TipoSelecionado.Equals(TipoListable.Pessoa.ToString()))
            {
                return true;
            }
            return false;
        }


        public void IniciaCommandos() {
            TipoSelecionado = TipoListable.Pessoa.ToString();
            GetVmListableWithNotify();
            Add = new RelayCommand((object param) =>
            {
                if (tipoPessoas())
                {
                    //CadastraPessoa
                    ListaListable = Listable.CadastraPessoa(Biblioteca);
                }
                else
                {
                    //CadastraLivro
                    ListaListable = Listable.CadastraLivro(Biblioteca);

                }
                Notifica(nameof(ListaListable));
            });
            Remove = new RelayCommand((object param) =>
            {
                    if (tipoPessoas())
                    {
                        Pessoa pessoa = (Pessoa)ItemSelecionado;
                        ListaListable = Listable.RemovePessoa(pessoa, Biblioteca);
                    } else
                    {
                        Livro livro = (Livro)ItemSelecionado;
                        ListaListable = Listable.RemoveLivro(livro, Biblioteca);
                    }
                Notifica(nameof(ListaListable));
            }, (object param2) => {
                return ItemSelecionado != null;
            });
            Update = new RelayCommand((object param) =>
            {
                if(tipoPessoas())
                {
                    Pessoa pessoa = (Pessoa)ItemSelecionado;
                    ListaListable = Listable.AlteraPessoa(pessoa, Biblioteca);
                } else
                {
                    Livro livro = (Livro)ItemSelecionado;
                    ListaListable = Listable.AlteraLivro(livro, Biblioteca);
                }
                Notifica(nameof(ListaListable));
            }, (object param2) => {
                return ItemSelecionado != null;
            });
            Emprestar = new RelayCommand((object param) =>
            {   
                Livro livro = (Livro)ItemSelecionado;
                ItemSelecionado = null;
                try
                {
                    ListaListable = new ObservableCollection<IListable>(Biblioteca.listaPessoa);
                    bool results = true;
                    while (results)
                    {
                        ListaEmprestimos listaEmp = new ListaEmprestimos
                        {
                            DataContext = this
                        };
                        listaEmp.ShowDialog();
                        results = (bool) listaEmp.DialogResult;
                        if(results && ItemSelecionado != null)
                        {
                            Biblioteca.EmprestarLivro(livro, (Pessoa)ItemSelecionado);
                            MessageBox.Show("Livro emprestado para a pessoa selecionada.");
                            return;
                        } else
                        {
                            if(results)
                                MessageBox.Show("Selecione ao menos uma pessoa para efetivar o emprestimo ou feche a aba.");
                        }
                    }
                } finally
                {
                    ListaListable = new ObservableCollection<IListable>(Biblioteca.listaLivros);
                    Notifica(nameof(ListaListable));
                }
            }, (object param2) => {
                if(ItemSelecionado != null && !tipoPessoas()) { 
                    try
                    {
                        return ((Livro)ItemSelecionado).GetPessoas() == null;
                    } catch (InvalidCastException ex)
                    {
                        return true;
                    }
                }
                return false;
            });
            Devolver = new RelayCommand((object param) =>
            {
                Livro livro = (Livro)ItemSelecionado;
                Pessoa pessoa = livro.GetPessoas();
                Biblioteca.DevolveLivro(livro, pessoa);
                MessageBox.Show("Livro devolvido por: " + pessoa.GetNomeCompleto());
                Notifica(nameof(ListaListable));
            }, (object param2) => {
                if (ItemSelecionado != null && !tipoPessoas())
                {
                    try
                    {
                        return ((Livro)ItemSelecionado).GetPessoas() != null;
                    }
                    catch (InvalidCastException ex)
                    {
                        return true;
                    }
                }
                return false;
            });
        }

        public void GetVmListableWithNotify()
        {
            if (tipoPessoas())
            {
                GetVmPessoasToListable();
            } else
            {
                GetVmLivrosToListable();
            }
            Notifica(nameof(ListaListable));
        }

        private void GetVmLivrosToListable()
        {
            if (vmLivros == null)
            {
                vmLivros = new ObservableCollection<Livro>();
            } else
            {
                vmLivros = Biblioteca.listaLivros;
            }
            ListaListable = new ObservableCollection<IListable>(vmLivros);
        }

        private void GetVmPessoasToListable()
        {
            if (vmPessoas == null)
            {
                vmPessoas = new ObservableCollection<Pessoa>();
            } else
            {
                vmPessoas = Biblioteca.listaPessoa;
            }
            ListaListable = new ObservableCollection<IListable>(vmPessoas);
        }
    }
}
