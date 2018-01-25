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

using System.Windows.Controls;

namespace Sinergia.SLM.Component
{
    /// <summary>
    /// Define una serie de métodos a implementar por una ventana que pueda
    /// funcionar como un host para AvalonDock.
    /// </summary>
    public interface IAvalonDockHost
    {
        /// <summary>
        /// Agrega una página a la sección principal de este
        /// <see cref="IAvalonDockHost"/>.
        /// </summary>
        /// <param name="page">Página a agregar.</param>
        void AddPage(Page page);
        /// <summary>
        /// Agrega una página al panel lateral izquierdo de este
        /// <see cref="IAvalonDockHost"/>.
        /// </summary>
        /// <param name="page">Página a agregar.</param>
        void AddTool(Page page);
    }
}