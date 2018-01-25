/*
Copyright © 2017, 2018 César Andrés Morgan
Pendiente de licenciamiento
===============================================================================
Este archivo está pensado para uso interno exclusivamente por su autor y otro
personal autorizado. No debe ser distribuido en ningún producto comercial sin
haber antes pasado por un control de calidad adecuado, ni tampoco debe ser
considerado como código listo para producción. El autor se absuelve de toda
responsabilidad y daños causados por el uso indebido de este archivo o de
cualquier parte de su contenido.
*/

#if DEBUG
using System.Diagnostics;
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
using Sinergia.SLM.Component;
using TheXDS.MCART.Dialogs;

namespace Sinergia.SLM.Pages
{
    /// <summary>
    /// Interaction logic for DeveloperDiagnostics.xaml
    /// </summary>
    [StartupPage]
    public partial class DeveloperDiagnostics : Page
    {
        MainWindow app = MainWindow.Instance;
        public DeveloperDiagnostics()
        {
            InitializeComponent();
        }

        private void BtnPluginInfo_Click(object sender, RoutedEventArgs e)
        {
            (new PluginBrowser()).ShowDialog();
        }
    }
}
#endif