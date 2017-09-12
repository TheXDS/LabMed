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
using CoreContable.Entities;
using MCART;
namespace CoreContable.UI.Dialogs
{
    public partial class CuentaSelector : Window
    {
        Cuenta selected = null;

        public CuentaSelector()
        {
            InitializeComponent();
        }

        public Cuenta Select(ContabContext db)
        {
            TreeViewItem activo = new TreeViewItem { Header = nameof(db.Activo) };
            foreach (var j in db.Activo.SubCategorias) Add2Trv(j, activo);
            trvCuentas.Items.Add(activo);

            TreeViewItem pasivo = new TreeViewItem { Header = nameof(db.Pasivo) };
            foreach (var j in db.Pasivo.SubCategorias) Add2Trv(j, pasivo);
            trvCuentas.Items.Add(pasivo);

            TreeViewItem capital = new TreeViewItem { Header = nameof(db.Capital) };
            foreach (var j in db.Capital.SubCategorias) Add2Trv(j, capital);
            trvCuentas.Items.Add(capital);

            ShowDialog();
            return selected;
        }

        public Cuenta Select(Categoria parent)
        {
            TreeViewItem activo = new TreeViewItem { Header = parent.DisplayName };
            foreach (var j in parent.SubCategorias) Add2Trv(j, activo);
            trvCuentas.Items.Add(activo);
            ShowDialog();
            return selected;
        }

        private void Add2Trv(Categoria categoria, TreeViewItem parent)
        {
            TreeViewItem roth = new TreeViewItem { Header = categoria.DisplayName };
            parent.Items.Add(roth);
            foreach (var j in categoria.SubCategorias) Add2Trv(j, roth);
            foreach (var j in categoria.Cuentas) Add2Trv(j, roth);
        }

        private void Add2Trv(Cuenta cuenta, TreeViewItem parent)
        {
            TreeViewItem roth = new TreeViewItem { Header = cuenta.DisplayName, Tag=cuenta };
            roth.Selected += Cuenta_Selected;
            parent.Items.Add(roth);
        }

        private void Cuenta_Selected(object sender, RoutedEventArgs e)
        {
            selected = ((Cuenta)((TreeViewItem)sender).Tag);
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            selected = null;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !DialogResult.HasValue;
        }

        private void SearchEntered(object sender, MCART.Events.ValueEventArgs<string> e)
        {
            Predicate<object> f = (p) =>
            {
                Identificable j = ((TreeViewItem)p).Tag as Identificable;
                if (!j.IsNull())
                    return j.DisplayName.ToLower().Contains(txtSearch.Search.ToLower());
                return false;
            };
            SetFilter(trvCuentas.Items,f);            
            trvCuentas.Items.Refresh();
        }

        void SetFilter(ItemCollection c, Predicate<object> f)
        {
            foreach(TreeViewItem j in c.OfType<TreeViewItem>())
            {
                SetFilter(j.Items, f);
                j.Items.Filter = f;
            }
        }

        private void txtSearch_SearchClosed(object sender, EventArgs e)
        {
            SetFilter(trvCuentas.Items,null);
            trvCuentas.Items.Refresh();
        }
    }
}
