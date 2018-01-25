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

using Sinergia.SLM.Component;
using Sinergia.SLM.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TheXDS.MCART;
using TheXDS.MCART.PluginSupport;
using Xceed.Wpf.AvalonDock.Layout;

namespace Sinergia.SLM
{
    /// <summary>
    /// Ventana principal de la aplicación
    /// </summary>
    public partial class MainWindow : Window, IAvalonDockHost, IStatusReporter, IModuleExposer
    {
        #region Manejo de módulos abiertos (cosas privadas)
        private class OpenModulo
        {
            internal Page Page { get; }
            internal LayoutAnchorable Container { get; }
            internal OpenModulo(Page page, LayoutAnchorable pane)
            {
                Page = page;
                Container = pane;
            }
        }
        internal static MainWindow Instance { get; private set; }
        List<Modulo> modulos = new List<Modulo>();
        List<OpenModulo> openModulos = new List<OpenModulo>();
        #endregion

        /// <summary>
        /// Expone los módulos cargados de la app.
        /// </summary>
        public IEnumerable<Modulo> Modulos => modulos;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            Loaded += (sender, e) => Init(); //Inicializar al presentar...
        }
        /// <summary>
        /// Realiza tareas de inicialización de la app de forma asíncrona.
        /// </summary>
        async void Init()
        {
            UpdateStatus("Cargando módulos...");

            await Task.Run(() =>
            {
                var pLoader = new PluginLoader();
                foreach (var j in pLoader.LoadEverything<Modulo>())
                    modulos.Add(j);
            });

            UpdateStatus("Cargando página de inicio...");

            // Buscar y cargar una página inicial...
            var sp = typeof(MainWindow).Assembly.GetTypes()
                .FirstOrDefault(p => p.HasAttr<StartupPageAttribute>());

            if (!(sp is null)) AddPage(sp.New<Page>());

            Done();
        }
        /// <summary>
        /// Actualiza el estado de la barra de progreso.
        /// </summary>
        /// <param name="progress">Progreso actual.</param>
        /// <param name="text">Texto de estado.</param>
        public void UpdateStatus(double progress, string text)
        {
            // Usar el dispatcher asegura que el hilo de UI realiza la actualización...
            Dispatcher.Invoke(() =>
            {
                if (double.IsNaN(progress))
                {
                    pgbStatus.IsIndeterminate = true;
                }
                else
                {
                    pgbStatus.IsIndeterminate = false;
                    pgbStatus.Value = progress;
                }
                lblStatus.Text = text;
            });
        }
        /// <summary>
        /// Actualiza el estado de la barra de progreso sin cambiar el texto.
        /// </summary>
        /// <param name="progress">Progreso actual.</param>
        public void UpdateStatus(double progress)
        {
            // Usar el dispatcher asegura que el hilo de UI realiza la actualización...
            Dispatcher.Invoke(() => pgbStatus.Value = progress);
        }
        /// <summary>
        /// Actualiza el estado de la barra de progreso, indicando una
        /// operación sin progreso específico.
        /// </summary>
        /// <param name="text">Texto de ayuda.</param>
        public void UpdateStatus(string text) => UpdateStatus(double.NaN, text);
        /// <summary>
        /// Actualiza el estado de la barra de progreso, indicando que la
        /// aplicación está lista.
        /// </summary>
        public void Done() => UpdateStatus(0, "Listo");
        /// <summary>
        /// Actualiza el estado de la barra de progreso, indicando que la
        /// operación finalizó con un mensaje.
        /// </summary>
        /// <param name="text"></param>
        public void Done(string text) => UpdateStatus(0, text);
        /// <summary>
        /// Agrega una página a la sección principal de este
        /// <see cref="IAvalonDockHost"/>.
        /// </summary>
        /// <param name="page">Página a agregar.</param>
        public void AddPage(Page page) => AddTo(lapMain, page);
        /// <summary>
        /// Agrega una página al panel lateral izquierdo de este
        /// <see cref="IAvalonDockHost"/>.
        /// </summary>
        /// <param name="page">Página a agregar.</param>
        public void AddTool(Page page) => AddTo(lapLeft, page);
        void AddTo(ILayoutPane pane, Page page)
        {
            if (!page.HasAttr<MultiInstanceAttribute>())
            {
                // la página no es multi-instancia, buscar una instancia abierta.
                var pg = openModulos.FirstOrDefault(p => p.Page.GetType().Is(page.GetType()));
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
                Content = frame,
                CanClose = !page.HasAttr<StartupPageAttribute>()
            };
            var tool = new OpenModulo(page, container);
            openModulos.Add(tool);
            if (container.CanClose)
                container.Hiding += (sender, e) => openModulos.Remove(tool);
            else
                container.Hiding += (sender, e) => Close();
            (pane as LayoutAnchorablePane)?.Children.Add(container);
            (pane as LayoutDocumentPane)?.Children.Add(container);
            container.IsActive = true;
        }
    }
}