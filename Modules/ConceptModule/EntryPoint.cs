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
using TheXDS.MCART.Attributes;
using TheXDS.MCART.PluginSupport;

namespace Sinergia.SLM.Modules
{
    [MinMCARTVersion(0, 8, 1, 2)]
    [TargetMCARTVersion(0, 8, 1, 2)]
    public class ConceptModule : Modulo
    {
        [InteractionItem]
        [Name("Página de prueba")]
        public void ShowTestPage(object sender, EventArgs e) => App.AddPage(new Pages.TestPage());

        [InteractionItem]
        [Name("Acerca de este plugin de prueba...")]
        public void AboutThis(object sender, EventArgs e) => About(this);
    }
}