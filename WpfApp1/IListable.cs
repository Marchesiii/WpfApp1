namespace WpfApp1
{
    public interface IListable
    {
        int Codigo { get; set; }
        string NomeCompleto { get; set; }
        public bool TipoPessoa();
    }
}
