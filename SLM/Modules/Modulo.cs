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

using TheXDS.MCART.PluginSupport;
using Sinergia.SLM.Component;

namespace Sinergia.SLM.Modules
{
    /// <summary>
    /// Define la estructura básica de un módulo del sistema SLM.
    /// </summary>
    public abstract class Modulo : Plugin
    {
        /// <summary>
        /// Expone métodos para agregar elementos a la ventana de la aplicación.
        /// </summary>
        protected IAvalonDockHost App { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Modulo"/>.
        /// </summary>
        protected Modulo() { App = MainWindow.Instance; }
    }
}