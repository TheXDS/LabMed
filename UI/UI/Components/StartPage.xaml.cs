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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoreContable.Entities;
using CoreContable.UI.Dialogs;

namespace CoreContable.UI.Components
{
    /// <summary>
    /// Lógica de interacción para StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
            NewModBtn("Probar CuentaSelect", Test_Click);
        }
        void NewModBtn(string name, RoutedEventHandler action)
        {
            Button newBtn = new Button { Content = name, Margin = (Thickness)FindResource("thk") };
            newBtn.Click += action;
            pnlModules.Children.Add(newBtn);
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            Categoria roth = new Categoria { DisplayName = "ActivosTest" , Prefix = 1};

            Categoria c1 = new Categoria { DisplayName = "Activos corrientes", Prefix = 10 };
            Categoria c2 = new Categoria { DisplayName = "Bancos", Prefix = 10 };
            c1.SubCategorias.Add(c2);
            c2.Cuentas.Add(new Cuenta { DisplayName = "Banco Atlántida" });
            c2.Cuentas.Add(new Cuenta { DisplayName = "Banco de Occidente" });
            c2.Cuentas.Add(new Cuenta { DisplayName = "Banco Promérica" });
            c1.Cuentas.Add(new Cuenta { DisplayName = "Caja chica" });
            Categoria c3 = new Categoria { DisplayName = "Activos no corrientes", Prefix = 10 };
            c3.Cuentas.Add(new Cuenta { DisplayName = "Prop. planta y equipo" });
            roth.SubCategorias.Add(c1);
            roth.SubCategorias.Add(c3);



            var j = new CuentaSelector();
            Cuenta s = j.Select(roth);
            if (j.DialogResult == true) MessageBox.Show(s.DisplayName);

        }

        private void BtnLibroDiario_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
