using System;
using System.Linq.Expressions;
using Coder.File2Object.Columns.ExcelColumn;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns
{
    public static class ExcelColumnExtensionsBoolean
    {
        #region Boolean

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name, string trueStrExpress,
                Expression<Func<TEntity, bool>> action,
                bool isRequire = true)
        {
            manager.Add(new BooleanColumn<TEntity>(name, action, trueStrExpress, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name, string trueStrExpress,
                Expression<Func<TEntity, bool?>> action,
                bool isRequire = true)
        {
            manager.Add(new BooleanNullableColumn<TEntity>(name, action, trueStrExpress, isRequire));
            return manager;
        }


        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string trueStrExpress,
                Expression<Func<TEntity, bool>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(columnName, trueStrExpress, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string trueStrExpress,
                Expression<Func<TEntity, bool?>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(columnName, trueStrExpress, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string trueStrExpress,
                Expression<Func<TEntity, bool?>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, trueStrExpress, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string trueStrExpress,
                Expression<Func<TEntity, bool>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, trueStrExpress, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string trueStrExpress,
                Expression<Func<TEntity, bool?>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplay(action);
            return manager.Column(columnName, trueStrExpress, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string trueStrExpress,
                Expression<Func<TEntity, bool>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplay(action);
            return manager.Column(columnName, trueStrExpress, action, isRequire);
        }

        #endregion
    }
}