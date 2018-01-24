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
using TheXDS.MCART;
using TheXDS.MCART.PluginSupport;
using Sinergia.SLM.Modules;
using Xceed.Wpf.AvalonDock.Layout;

namespace Sinergia.SLM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static MainWindow Instance { get; private set; }
        List<Modulo> Modulos=new List<Modulo>();

        private class ToolWindow
        {
            internal Page Page { get; }
            internal LayoutAnchorable Container { get; }
            internal ToolWindow(Page page, LayoutAnchorable pane)
            {
                Page = page;
                Container = pane;
            }
        }
        List<ToolWindow> openTools = new List<ToolWindow>();

        


        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            var pl = new PluginLoader();
            Modulos.AddRange(pl.LoadEverything<Modulo>(".",System.IO.SearchOption.TopDirectoryOnly));
            foreach (var j in Modulos)
            {
                pnlMenu.Children.Add(j.UIPanel<Button, StackPanel>());
            }
        }





        public void AddPage(Page page) => AddTo(lapMain, page);
        public void AddTool(Page page) => AddTo(lapLeft, page);
        void AddTo(ILayoutPane pane, Page page)
        {
            if (!page.HasAttr<MultiInstanceAttribute>())
            {
                // la página no es multi-instancia, buscar una instancia abierta.
                var pg = openTools.FirstOrDefault(p => p.Page.GetType().Is(page.GetType()));
                if (!(pg is null))
                {
                    // Ya existe una instancia, activar.
                    pg.Container.IsActive = true;
                    return;
                }
            }

            // Crear nueva página...
            var frame = new Frame
            {
                NavigationUIVisibility = NavigationUIVisibility.Hidden
            };
            frame.Navigate(page);
            var container = new LayoutAnchorable
            {
                Title = page.Title,
                Content = frame
            };
            var tool = new ToolWindow(page, container);
            openTools.Add(tool);
            container.Hiding += (sender, e) => openTools.Remove(tool);
            pane.Children.ToList().Add(container);
            container.IsActive = true;
        }
    }
}
