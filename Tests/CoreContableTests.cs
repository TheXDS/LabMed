/*
Copyright © 2017 César Andrés Morgan
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreContable.Entities;
using static CoreContable.Logic.Commands;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class CoreContableTests
    {
        [TestMethod]
        public void AddCategoriaTest()
        {
            using (var db = new ContabContext())
            {
                Task.WaitAll(db.AddCategoria("Activos corrientes test", db.Activo, 10));
                Assert.IsNotNull(db.Activo.SubCategorias.Find(p=> p.Prefix == 10));
            }
        }
    }
}
