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
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Themes;
using Pages = CoreContable.UI.Components;

namespace CoreContable.UI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            App.mainWindow = this;

            InitializeComponent();

            // Cargar el tema de AvalonDock...
            // TODO: Administrar tema con las preferencias.
            dmRoot.Theme = new Xceed.Wpf.AvalonDock.Themes.ExpressionDarkTheme();

            LoadPage(new Pages.StartPage());
        }

        /// <summary>
        /// Carga una página en la raíz de paneles de la ventana principal.
        /// </summary>
        /// <param name="page">Página a cargar.</param>
        public void LoadPage(Page page) => LoadPage(page, lapRoot);

        /// <summary>
        /// Carga una página en el panel de ventanas acoplables especificado.
        /// </summary>
        /// <param name="page">Página a cargar.</param>
        /// <param name="lap">
        /// Panel de ventanas acoplables en el cual cargar la página.
        /// </param>
        public void LoadPage(Page page, LayoutAnchorablePane lap)
        {
            page.SetBinding(Page.ForegroundProperty, new Binding(nameof(dmRoot.Foreground)) { Source = dmRoot });
            LayoutAnchorable pane = new LayoutAnchorable { Title = page.Title };
            Frame frame = new Frame();
            frame.Navigate(page);
            pane.Content = frame;
            lap.Children.Add(pane);
        }
    }
}
