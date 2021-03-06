﻿using System;
using System.Linq.Expressions;
using Coder.File2Object.Columns.ExcelColumn;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns
{
    public static class ExcelColumnExtensionsString
    {
        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, string>> action,
                bool isRequire = false)
        {
            manager.Add(new StringColumn<TEntity>(name, action, isRequire));
            return manager;
        }

        public static File2ObjectManager<TEntity, ICell>
            Column<TEntity>(this File2ObjectManager<TEntity, ICell> manager, Expression<Func<TEntity, string>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyInfo(action).Name;
            return manager.Column(columnName, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayNameAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, string>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return manager.Column(columnName, action, isRequire);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnDisplayAttribute<TEntity>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, string>> action,
                bool isRequire = false)
        {
            var columnName = PropertyHelper.GetPropertyNameFromDisplay(action);
            return manager.Column(columnName, action, isRequire);
        }
    }
}