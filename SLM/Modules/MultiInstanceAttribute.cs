﻿/*
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

using System;

namespace Sinergia.SLM.Modules
{
    /// <summary>
    /// Marca una página para poder instanciarse varias veces.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MultiInstanceAttribute : Attribute { }
}