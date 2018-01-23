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
using System.Linq;

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

        #region Creación de datos
        /// <summary>
        /// Agrega una nueva <see cref="Categoria"/> a la base de datos.
        /// </summary>
        /// <param name="db">
        /// Referencia a la instancia de base de datos.
        /// </param>
        /// <param name="nombre">Nombre del nuevo grupo.</param>
        /// <param name="parent">Categoría padre de este elemento.</param>
        /// <param name="prefix">Prefijo para generar códigos de cuenta.</param>
        /// <returns>La <see cref="Categoria"/> que ha sido creada.</returns>
        public static async Task<Categoria> AddCategoria(this ContabContext db, string nombre, Categoria parent, int prefix)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException(nameof(nombre));
            if (parent is null) throw new ArgumentNullException(nameof(parent));
            if (prefix < 1) prefix = parent.SubCategorias.Count + 1;
            var retVal = new Categoria { DisplayName = nombre, Prefix = prefix };
            await db.DoTransact(() => parent.SubCategorias.Add(retVal));
            return retVal;
        }

        /// <summary>
        /// Agrega una nueva <see cref="Cuenta"/> a la base de datos.
        /// </summary>
        /// <param name="db">
        /// Referencia a la instancia de base de datos.
        /// </param>
        /// <param name="nombre">Nombre del nuevo grupo.</param>
        /// <param name="parent">Categoría padre de este elemento.</param>
        /// <param name="prefix">Prefijo para generar códigos de cuenta.</param>
        /// <returns>La <see cref="Cuenta"/> que ha sido creada.</returns>
        public static async Task<Cuenta> AddCuenta(this ContabContext db, string nombre, Categoria parent, int prefix)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException(nameof(nombre));
            if (parent is null) throw new ArgumentNullException(nameof(parent));
            if (prefix < 1) throw new ArgumentOutOfRangeException(nameof(prefix));
            Cuenta cuenta = new Cuenta { DisplayName = nombre, Prefix = prefix };
            await db.DoTransact(() => parent.Cuentas.Add(cuenta));
            return cuenta;
        }

        /// <summary>
        /// Agrega un nuevo <see cref="CuentaGroup"/> a la base de datos.
        /// </summary>
        /// <param name="db">
        /// Referencia a la instancia de base de datos.
        /// </param>
        /// <param name="nombre">Nombre del nuevo grupo.</param>
        /// <returns>El <see cref="CuentaGroup"/> que ha sido creado.</returns>
        public static async Task<CuentaGroup> AddCuentaGroup(this ContabContext db, string nombre)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException(nameof(nombre));
            var cg = new CuentaGroup { DisplayName = nombre };
            await db.DoTransact(db.CuentaGroups.AddAsync(cg));
            return cg;
        }
        #endregion

        public static async Task<bool> RemoveCategoria(this ContabContext db, long id)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            if (id <= 3) throw new ArgumentOutOfRangeException(nameof(id));
            var cat = await db.Categorias.FindAsync(id);
            if (cat is null) return false;
            await db.DoTransact(() => RemCategoria(db, cat));
            return true;
        }

        public static async Task<bool> RemoveCuenta(this ContabContext db, long id)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            if (id < 1) throw new ArgumentOutOfRangeException(nameof(id));
            var cat = await db.Cuentas.FindAsync(id);
            if (cat is null) return false;
            await Task.Run(() =>
            {
                if (db.LibroDiario.FindAny(p => p.Movimientos.Any(q => q.RefCuenta == cat), out _)) throw new InvalidOperationException();
            });
            await db.DoTransact(() => RemCuenta(db, cat));
            return true;
        }

        public static async Task<bool> RemoveCuentaGroup(this ContabContext db, long id)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            if (id < 1) throw new ArgumentOutOfRangeException(nameof(id));
            var cat = await db.CuentaGroups.FindAsync(id);
            if (cat is null) return false;
            await db.DoTransact(() => db.CuentaGroups.Remove(cat));
            return true;
        }


        #region Grupos de cuentas
        public static async Task DeGroup(this ContabContext db, Cuenta[] cuentas, CuentaGroup[] grupos)
        {
            await db.DoTransact(() =>
            {
                foreach (var j in cuentas)
                {
                    foreach (var k in grupos)
                    {
                        if (db.N2N_Cuenta_CuentaGroup.FindAny(p => p.CuentaID.ID == j.ID && p.CuentaGroupID.ID == k.ID, out var element))
                            db.N2N_Cuenta_CuentaGroup.Remove(element);
                    }
                }
            });
        }

        public static async Task GroupCuentas(this ContabContext db, Cuenta[] cuentas, CuentaGroup[] grupos)
        {
            await db.DoTransact(() =>
            {
                foreach (var j in cuentas)
                {
                    foreach (var k in grupos)
                    {
                        if (!db.N2N_Cuenta_CuentaGroup.FindAny(p => p.CuentaID.ID == j.ID && p.CuentaGroupID.ID == k.ID, out _))
                        {
                            db.N2N_Cuenta_CuentaGroup.Add(new Cuenta_CuentaGroup_N2N
                            {
                                CuentaID = j ?? throw new NullReferenceException(),
                                CuentaGroupID = k ?? throw new NullReferenceException()
                            });
                        }
                    }
                }
            });
        }

        #endregion

        #region Transacciones contables
        /* -= NOTA=-
         * En una base de datos contable, en teoría no deben existir métodos de
         * edición ni de borrado. Sin embargo, cabe la posibilidad de mover los
         * datos a una base de datos de archivado. Eventualmente, se piensa
         * implementar una instrucción especial que hará el trabajo de forma
         * totalmente automática.
         */

        /// <summary>
        /// Agrega una nueva <see cref="Partida"/> a la base de datos.
        /// </summary>
        /// <param name="db">
        /// Referencia a la instancia de base de datos.
        /// </param>
        /// <param name="nombre">Nombre del nuevo grupo.</param>
        /// <returns>La <see cref="Partida"/> que ha sido creada.</returns>
        public static async Task<Partida> AddPartida(this ContabContext db, string synopsys, params Movimiento[] movimientos)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrWhiteSpace(synopsys)) throw new ArgumentNullException(nameof(synopsys));
            Partida partida = new Partida { TimeStamp = DateTime.Now, Synopsys = synopsys };
            foreach (var j in movimientos) partida.Movimientos.Add(j);
            if (!partida.IsValid) throw new ArgumentException(nameof(movimientos));
            await db.DoTransact(db.LibroDiario.AddAsync(partida));
            return partida;
        }
        #endregion


        #region Atomic-Ops
        static void RemCategoria(ContabContext db, Categoria c)
        {
            foreach (var j in c.SubCategorias) RemCategoria(db, j);
            db.Categorias.Remove(c);
        }
        static void RemCuenta(ContabContext db, Cuenta c)
        {
            db.N2N_Cuenta_CuentaGroup.RemoveRange(db.N2N_Cuenta_CuentaGroup.FindAll(p => p.CuentaID == c));
            db.Cuentas.Remove(c);
        }
        static void RemCuentaGroup(ContabContext db, CuentaGroup c)
        {
            db.N2N_Cuenta_CuentaGroup.RemoveRange(db.N2N_Cuenta_CuentaGroup.FindAll(p => p.CuentaGroupID == c));
            db.CuentaGroups.Remove(c);
        }
        #endregion
    }
}