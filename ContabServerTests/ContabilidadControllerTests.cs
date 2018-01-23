using System;
using Xunit;
using ContabServer.Controllers;
using ContabServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContabServerTests
{
    public class ContabilidadControllerTests
    {
        [Fact]
        public void GetCategoriaTest()
        {
            var cont = new ContabilidadController();
            var resp = cont.GetCategoria(1).GetAwaiter().GetResult();
            Assert.NotNull(resp);
            Assert.Equal("Activo", resp.DisplayName);
        }

        [Fact]
        public void CategoriasManagementTest()
        {
            var cont = new ContabilidadController();
            var cat = cont.AddCategoria("Categoría de prueba", 1, 65535).GetAwaiter().GetResult() as JsonResult;
            Assert.NotNull(cat);
            Assert.IsType<Categoria>(cat.Value);
            Assert.IsType<OkResult>(cont.DeleteCategoria((cat.Value as Categoria).ID).GetAwaiter().GetResult());
            Assert.Null(cont.GetCategoria((cat.Value as Categoria).ID).GetAwaiter().GetResult());
        }
    }
}
