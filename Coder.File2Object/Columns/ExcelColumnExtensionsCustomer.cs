using System;
using System.Linq.Expressions;
using Coder.File2Object.Columns.ExcelColumn;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns
{
    public static class ExcelColumnExtensionsCustomer
    {
        public static File2ObjectManager<TEntity, ICell>
            CustomColumn<TEntity, TValue>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, TValue>> action, Func<string, Tuple<TValue, string, bool>> convert,
                bool isRequire = true)
        {
            manager.Add(new CustomColumn<TEntity, TValue>(name, action, convert, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            CustomColumn<TEntity, TValue>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TValue>> action, Func<string, Tuple<TValue, string, bool>> convert,
                bool isRequire = true)
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            manager.Add(new CustomColumn<TEntity, TValue>(name, action, convert, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            CustomColumnDisplayNameAttribute<TEntity, TValue>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TValue>> action, Func<string, Tuple<TValue, string, bool>> convert,
                bool isRequire = true)
        {
            var name = PropertyHelper.GetPropertyNameFromDisplayName(action);
            manager.Add(new CustomColumn<TEntity, TValue>(name, action, convert, isRequire));
            return manager;
        }
        public static File2ObjectManager<TEntity, ICell>
            CustomColumnDisplayAttribute<TEntity, TValue>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TValue>> action, Func<string, Tuple<TValue, string, bool>> convert,
                bool isRequire = true)
        {
            var name = PropertyHelper.GetPropertyNameFromDisplay(action);
            manager.Add(new CustomColumn<TEntity, TValue>(name, action, convert, isRequire));
            return manager;
        }

    }
}