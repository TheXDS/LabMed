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

using ContabServer.Models;
using System;
using System.Threading.Tasks;

namespace ContabServer.Logic
{
    public static class Commands
    {
        /// <summary>
        /// Inicializa la base de datos con información básica estructural.
        /// </summary>
        internal static async Task InitialSetupAsync(ContabContext db)
        {
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();
            await db.DoTransact(() =>
            {
                /* Estas categorías se crean manualmente ya que son las
                 * raíces de toda la base de categorías, y por lo tanto, no
                 * tienen padre.*/
                db.Categorias.Add(new Categoria() { DisplayName = "Activo", Prefix = 1 });
                db.Categorias.Add(new Categoria() { DisplayName = "Pasivo", Prefix = 2 });
                db.Categorias.Add(new Categoria() { DisplayName = "Capital", Prefix = 3 });
            });
        }

        public static async Task AddCategoria(this ContabContext db, string nombre, Categoria parent, int prefix)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException(nameof(nombre));
            if (parent is null) throw new ArgumentNullException(nameof(parent));
            if (prefix < 1) throw new ArgumentOutOfRangeException(nameof(prefix));
            await db.DoTransact(() =>
            {
                parent.SubCategorias.Add(new Categoria { DisplayName = nombre, Prefix = prefix });
            });
        }

        public static async Task AddCuenta(this ContabContext db, string nombre, Categoria parent, int prefix, params CuentaGroup[] memberOf)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException(nameof(nombre));
            if (parent is null) throw new ArgumentNullException(nameof(parent));
            if (prefix < 1) throw new ArgumentOutOfRangeException(nameof(prefix));
            await db.DoTransact(() =>
            {
                Cuenta cuenta = new Cuenta { DisplayName = nombre, Prefix = prefix };
                foreach (var j in memberOf)
                {
                    db.N2N_Cuenta_CuentaGroup.Add(new Cuenta_CuentaGroup_N2N
                    {
                        CuentaID = cuenta,
                        CuentaGroupID = j
                    });                    
                }
                parent.Cuentas.Add(cuenta);
            });
        }

        public static async Task AddCuentaGroup(this ContabContext db, string nombre)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException(nameof(nombre));
            await db.DoTransact(() =>
            {
                db.CuentaGroups.Add(new CuentaGroup { DisplayName = nombre });
            });
        }
        
        public static async Task AddPartida(this ContabContext db, string synopsys, params Movimiento[] movimientos)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrWhiteSpace(synopsys)) throw new ArgumentNullException(nameof(synopsys));
            await db.DoTransact(() =>
            {
                Partida partida = new Partida
                {
                    TimeStamp = DateTime.Now,
                    Synopsys = synopsys,
                };
                foreach (var j in movimientos)                
                    partida.Movimientos.Add(j);

                if (!partida.IsValid) throw new ArgumentException(nameof(movimientos));
                db.LibroDiario.Add(partida);
            });
        }

        /*
         * En una base de datos contable, en teoría no deben existir métodos de
         * edición ni de borrado. Sin embargo, cabe la posibilidad de mover los
         * datos a una base de datos de archivado. Eventualmente, se piensa
         * implementar una instrucción especial que hará el trabajo de forma
         * totalmente automática.
         */
    }
}