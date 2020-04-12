using System;
using System.Linq.Expressions;
using Coder.File2Object.Columns.ExcelColumn;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns
{
    public static class ExcelColumnExtensionsDateTimeOffset
    {
        public static File2ObjectManager<TEntity, ICell> Column<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager, string name,
            Expression<Func<TEntity, DateTimeOffset>> action, bool isRequire = true)
        {
            manager.Add(new DateTimeOffsetColumn<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell> Column<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,
            Expression<Func<TEntity, DateTimeOffset>> action, bool isRequire = true)
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(name, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell> Column<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager, string name,
            Expression<Func<TEntity, DateTimeOffset?>> action, bool isRequire = false)
        {
            manager.Add(new DateTimeOffsetColumnNullable<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell> Column<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,
            Expression<Func<TEntity, DateTimeOffset?>> action, bool isRequire = false)
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(name, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell> ColumnDisplayNameAttribute<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,
            Expression<Func<TEntity, DateTimeOffset?>> action, bool isRequire = false)
        {
            var name = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(name, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell> ColumnDisplayNameAttribute<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,
            Expression<Func<TEntity, DateTimeOffset>> action, bool isRequire = false)
        {
            var name = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(name, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell> ColumnDisplayAttribute<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,
            Expression<Func<TEntity, DateTimeOffset?>> action, bool isRequire = false)
        {
            var name = PropertyHelper.GetPropertyNameFromDisplay(action);
            return manager.Column(name, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell> ColumnDisplayAttribute<TEntity>(
            this File2ObjectManager<TEntity, ICell> manager,
            Expression<Func<TEntity, DateTimeOffset>> action, bool isRequire = false)
        {
            var name = PropertyHelper.GetPropertyNameFromDisplay(action);
            return manager.Column(name, action, isRequire);
        }
    }
}