using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Lógica interna para TelaCadastroLivro.xaml
    /// </summary>
    public partial class TelaCadastroLivro : Window
    {
        public TelaCadastroLivro()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int pags;
            if (!int.TryParse(TextBox1.Text, out pags))
            {
                MessageBox.Show("Insira apenas numeros nas paginas.");
                TextBox1.Text = "0";
                return;
            } 
            DialogResult = true;
        }
    }
}
