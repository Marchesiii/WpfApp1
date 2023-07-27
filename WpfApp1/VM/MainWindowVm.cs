using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public class MainWindowVm : INotifyPropertyChanged
    {
        private readonly ILocatario locatario;
        public MainWindowVm()
        {
            locatario = new Biblioteca();
            IniciaCommandos();
        }

        public ObservableCollection<IItem> ListaListable { get; private set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Update { get; private set; }
        public ICommand Emprestar { get; set; }
        public ICommand Devolver { get; set; }
        public ICommand Info { get; set; }
        public IItem? ItemSelecionado { get; set; }

        public string TipoSelecionado { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Notifica(string nome)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }


        public void IniciaCommandos()
        {
            TipoSelecionado = nameof(Pessoa);
            GetVmListableWithNotify();
            Add = new RelayCommand((param) =>
            {
                IItem item = TipoSelecionado.Equals(nameof(Pessoa)) ? new Pessoa() : new Livro(); 
                if (AlterarItem(item))
                {
                    ListaListable = ConvertToListableObservable(locatario.AddItem(item));
                }
                   
                Notifica(nameof(ListaListable));
            });
            Remove = new RelayCommand((param) =>
            {
                if (ItemSelecionado != null)
                {
                    ListaListable = ConvertToListableObservable(locatario.RemoverItem(ItemSelecionado));
                }
                Notifica(nameof(ListaListable));
            }, (param2) => ItemSelecionado != null && !ItemSelecionado.Ocupado()
            );
            Update = new RelayCommand((param) =>
            {
                if (ItemSelecionado != null)
                {
                    IItem item = ItemSelecionado;
                    IItem itemClone = ItemSelecionado.Clone();
                    if(AlterarItem(itemClone))
                    {
                        ListaListable = ConvertToListableObservable(locatario.SubstituiItem(itemClone, item));
                    }
                }
                Notifica(nameof(ListaListable));
            }, (param2) => ItemSelecionado != null
            );
            Emprestar = new RelayCommand((param) =>
            {
                try
                {
                    if (ItemSelecionado != null)
                    {
                        Livro livro = (Livro)ItemSelecionado;
                        ItemSelecionado = null;
                        ListaListable = ConvertToListableObservable(locatario.ListaItens(typeof(Pessoa)));
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
                                locatario.EmprestarItem(livro, (Pessoa)ItemSelecionado);
                                MessageBox.Show("Livro emprestado para: " + ((Pessoa)ItemSelecionado).NomeCompleto);
                                return;
                            }
                            else
                            {
                                if (results)
                                { 
                                    MessageBox.Show("Selecione ao menos uma pessoa para efetivar o emprestimo ou feche a aba.");
                                }
                            }
                        }
                    }
                }
                finally
                {
                    ListaListable = ConvertToListableObservable(locatario.ListaItens(typeof(Livro)));
                    Notifica(nameof(ListaListable));
                }
            }, (param2) =>
            {
                if (ItemSelecionado != null && !GetTipoSelecionadoPessoa())
                {
                    if (!ItemSelecionado.GetType().Equals(typeof(Pessoa)))
                    {
                        return !ItemSelecionado.Ocupado();
                    }
                }
                return false;
            });
            Devolver = new RelayCommand((param) =>
            {
                if (ItemSelecionado != null)
                {
                    Livro livro = (Livro)ItemSelecionado;
                    Pessoa? pessoa = livro.Pessoa;
                    if (pessoa != null)
                    {
                        locatario.DevolverItem(livro, pessoa);
                        MessageBox.Show("Livro devolvido por: " + pessoa.NomeCompleto);
                    }
                }
                Notifica(nameof(ListaListable));
            }, (param2) =>
            {
                if (ItemSelecionado != null && !GetTipoSelecionadoPessoa())
                {
                    if (!ItemSelecionado.GetType().Equals(typeof(Pessoa)))
                    {
                        return ItemSelecionado.Ocupado();

                    }
                }
                return false;
            });
            Info = new RelayCommand((param) =>
            {
                if (ItemSelecionado != null)
                {
                    Window tela = GetTelaInfo(ItemSelecionado);
                    tela.DataContext = ItemSelecionado;
                    tela.ShowDialog();
                }
            }, (param2) => ItemSelecionado != null
            );
        }

        private static bool AlterarItem(IItem itemClone)
        {
            bool dialogResult = true;
            while (dialogResult)
            {
                Window tela = GetTelaCadastro(itemClone);
                tela.DataContext = itemClone;
                tela.ShowDialog();
                dialogResult = (bool)tela.DialogResult;
                if (dialogResult)
                {
                    PseudoExc ex = new PseudoExc();
                    if (itemClone.Check(ex))
                    {
                        return true;
                    }
                    else
                    {
                        Valids.DisplayValidationError(ex.ex);
                    }
                }
            }
            return false;
        }

        private ObservableCollection<IItem> ConvertToListableObservable(List<IItem> lista) => new ObservableCollection<IItem>(lista);
        private Window GetTelaInfo(IItem itemSelecionado) => itemSelecionado.GetType().Equals(typeof(Pessoa)) ? new TelaPessoa() : new TelaLivro();
        private static Window GetTelaCadastro(IItem item) => item.GetType().Equals(typeof(Pessoa)) ? new TelaCadastro() : new TelaCadastroLivro();

        private bool GetTipoSelecionadoPessoa()
        {
            return TipoSelecionado == nameof(Pessoa);
        }


        public void GetVmListableWithNotify()
        {
            if (GetTipoSelecionadoPessoa())
            {
                GetVmPessoasToListable();
            }
            else
            {
                GetVmLivrosToListable();
            }
            Notifica(nameof(ListaListable));
        }

        private void GetVmLivrosToListable()
        {
            ListaListable = ConvertToListableObservable(locatario.ListaItens(typeof(Livro)));
        }

        private void GetVmPessoasToListable()
        {
            ListaListable = ConvertToListableObservable(locatario.ListaItens(typeof(Pessoa)));
        }


    }
}
