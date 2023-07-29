using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Model.Biblioteca.Interfaces;

namespace WpfApp1
{
    public class MainWindowVm : INotifyPropertyChanged
    {
        private readonly ILocador EmpresaLocadora;
        private readonly IDbConnection conn;
        public MainWindowVm()
        {
            conn = new PostgresConnection();
            try
            {
                EmpresaLocadora = new Biblioteca();
                IniciaCommandos();
            }
            finally
            {
                conn.Close();
            }
        }

        public ObservableCollection<IItemListavel> ListaListable { get; private set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Update { get; private set; }
        public ICommand Emprestar { get; set; }
        public ICommand Devolver { get; set; }
        public ICommand Info { get; set; }
        public IItemListavel? ItemSelecionado { get; set; }

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
                IItem item = GetTipoSelecionadoPessoa() ? new Pessoa() : new Livro();
                if (AlterarItem(item))
                {
                    ListaListable = ConvertToListableObservable(EmpresaLocadora.AddItem(item));
                }

                Notifica(nameof(ListaListable));
            });
            Remove = new RelayCommand((param) =>
            {
                if (ItemSelecionado != null)
                {
                    ListaListable = ConvertToListableObservable(EmpresaLocadora.RemoverItem(((IItem)ItemSelecionado)));
                }
                Notifica(nameof(ListaListable));
            }, (param2) => ItemSelecionado != null && !((IItem)ItemSelecionado).Ocupado()
            );
            Update = new RelayCommand((param) =>
            {
                if (ItemSelecionado != null)
                {
                    IItem item = ((IItem)ItemSelecionado);
                    IItem itemClone = ((IItem)ItemSelecionado).Clone();
                    if (AlterarItem(itemClone))
                    {
                        ListaListable = ConvertToListableObservable(EmpresaLocadora.SubstituiItem(itemClone, item));
                    }
                }
                Notifica(nameof(ListaListable));
            }, (param2) => ItemSelecionado != null
            );
            Emprestar = new RelayCommand((param) =>
            {
            if (ItemSelecionado != null)
            {
                IItemLocado livro = (IItemLocado)ItemSelecionado;
                try
                {
                    bool results = false;
                    TelaEmprestimoVm tela = null;
                    while (!results && (tela == null || tela.Tentou == true))
                        {
                            tela = new TelaEmprestimoVm();
                            ListaEmprestimos listaEmp = new ListaEmprestimos();
                            tela.ListaListable = ConvertToListableObservable(EmpresaLocadora.ListaItens(typeof(IItemLocador)));
                            tela.EmpresaLocadora = EmpresaLocadora;
                            tela.Item = (IItemLocado)ItemSelecionado;
                            listaEmp.Tela = tela;
                            listaEmp.DataContext = tela;
                            listaEmp.ShowDialog();
                            results = (bool)listaEmp.DialogResult;
                        }
                    }
                    finally
                    {
                        ListaListable = ConvertToListableObservable(EmpresaLocadora.ListaItens(typeof(IItemLocado)));
                        Notifica(nameof(ListaListable));
                    }
                }
            }, (param2) =>
            {
                if (ItemSelecionado != null && !GetTipoSelecionadoPessoa())
                {
                    if (ItemSelecionado.GetType().GetInterface(nameof(IItemLocado)) != null)
                    {
                        return !((IItem)ItemSelecionado).Ocupado();
                    }
                }
                return false;
            });
            Devolver = new RelayCommand((param) =>
            {
                if (ItemSelecionado != null)
                {
                    IItemLocado livro = (IItemLocado)ItemSelecionado;
                    EmpresaLocadora.DevolverItem(livro);
                    MessageBox.Show("Livro devolvido");
                    
                }
                Notifica(nameof(ListaListable));
            }, (param2) =>
            {
                if (ItemSelecionado != null && !GetTipoSelecionadoPessoa())
                {
                    if (ItemSelecionado.GetType().GetInterface(nameof(IItemLocado)) != null)
                    {
                        return ((IItem)ItemSelecionado).Ocupado();
                    }
                }
                return false;
            });
            Info = new RelayCommand((param) =>
            {
                if (ItemSelecionado != null)
                {
                    Window tela = GetTelaInfo(ItemSelecionado);
                    TelaInfoVm telaInfo = new TelaInfoVm();
                    TelaInfoBuilder telaInfoBuilder = new TelaInfoBuilder(ItemSelecionado.GetType());
                    telaInfoBuilder.CreateTelaInfo(telaInfo, (IItem)ItemSelecionado, EmpresaLocadora);
                    tela.DataContext = telaInfo;
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

        private ObservableCollection<IItemListavel> ConvertToListableObservable(List<IItem> lista) => new ObservableCollection<IItemListavel>(lista);
        private Window GetTelaInfo(IItemListavel itemSelecionado) => itemSelecionado.GetType().GetInterface(nameof(IItemLocador)) != null ? new TelaPessoa() : new TelaLivro();
        private static Window GetTelaCadastro(IItemListavel item) => item.GetType().GetInterface(nameof(IItemLocador)) != null ? new TelaCadastro() : new TelaCadastroLivro();

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
            ListaListable = ConvertToListableObservable(EmpresaLocadora.ListaItens(typeof(IItemLocado)));
        }

        private void GetVmPessoasToListable()
        {
            ListaListable = ConvertToListableObservable(EmpresaLocadora.ListaItens(typeof(IItemLocador)));
        }


    }
}
