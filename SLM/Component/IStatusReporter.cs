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

namespace Sinergia.SLM.Component
{
    /// <summary>
    /// Define una serie de métodos a implementar por una ventana con una barra de estado y progreso.
    /// </summary>
    public interface IStatusReporter
    {
        /// <summary>
        /// Cambia la barra al estado de "Listo".
        /// </summary>
        void Done();
        /// <summary>
        /// Cambia la barra al estado de listo, mostrando un mensaje.
        /// </summary>
        /// <param name="text">Mensaje a mostrar.</param>
        void Done(string text);
        /// <summary>
        /// Actualiza el estado de la barra de progreso.
        /// </summary>
        /// <param name="progress">Valor de progreso.</param>
        void UpdateStatus(double progress);
        /// <summary>
        /// Actualiza el estado de la barra de progreso.
        /// </summary>
        /// <param name="progress">Valor de progreso.</param>
        /// <param name="text">Mensaje de estado.</param>
        void UpdateStatus(double progress, string text);
        /// <summary>
        /// <param name="progress">Valor de progreso.</param>
        /// </summary>
        /// <param name="text">Mensaje de estado.</param>
        void UpdateStatus(string text);
    }
}