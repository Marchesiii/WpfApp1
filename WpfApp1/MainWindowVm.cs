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
        public ObservableCollection<IListable> ListaListable { get; set; }
        private Biblioteca Biblioteca { get; set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Update { get; private set; }
        public ICommand Emprestar { get; set; }
        public ICommand Devolver { get; set; }
        public ICommand Info { get; set; }
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

        public IListable GetTipoSelecionado()
        {
            if (TipoSelecionado.Equals(nameof(Pessoa)))
            {
                return new Pessoa();
            }
            return new Livro();
        }


        public void IniciaCommandos() {
            TipoSelecionado = nameof(Pessoa);
            GetVmListableWithNotify();
            Add = new RelayCommand((object param) =>
            {
                ListaListable = Listable.CadastraItem(GetTipoSelecionado(), Biblioteca);
                Notifica(nameof(ListaListable));
            });
            Remove = new RelayCommand((object param) =>
            {
                ListaListable = Listable.RemoveItem(ItemSelecionado, Biblioteca);
                Notifica(nameof(ListaListable));
            }, (object param2) => {
                
                return ItemSelecionado != null && !ItemSelecionado.Ocupado();
            });
            Update = new RelayCommand((object param) =>
            {
                ListaListable = Listable.AlteraItem(ItemSelecionado, Biblioteca);
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
                        results = (bool)listaEmp.DialogResult;
                        if (results && ItemSelecionado != null)
                        {
                            Biblioteca.EmprestarLivro(livro, (Pessoa)ItemSelecionado);
                            MessageBox.Show("Livro emprestado para a pessoa selecionada.");
                            return;
                        } else
                        {
                            if (results)
                                MessageBox.Show("Selecione ao menos uma pessoa para efetivar o emprestimo ou feche a aba.");
                        }
                    }
                } finally
                {
                    ListaListable = new ObservableCollection<IListable>(Biblioteca.listaLivros);
                    Notifica(nameof(ListaListable));
                }
            }, (object param2) => {
                if (ItemSelecionado != null && !GetTipoSelecionado().GetType().Equals(typeof(Pessoa))) {
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
                if (ItemSelecionado != null && !GetTipoSelecionado().GetType().Equals(typeof(Pessoa)))
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
            Info = new RelayCommand((object param) => {
                Window tela = ItemSelecionado.GetTelaInfo();
                tela.DataContext = ItemSelecionado;
                tela.ShowDialog();
            }, (object param2) =>
            {
                return ItemSelecionado != null;
            });
        }

        public void GetVmListableWithNotify()
        {
            if (TipoSelecionado.Equals(nameof(Pessoa)))
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
            ListaListable = new ObservableCollection<IListable>(Biblioteca.listaLivros);
        }

        private void GetVmPessoasToListable()
        {
            ListaListable = new ObservableCollection<IListable>(Biblioteca.listaPessoa);
        }
    }
}
