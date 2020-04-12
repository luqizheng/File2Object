using System;
using System.Linq.Expressions;
using Coder.File2Object.Columns.ExcelColumn;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns
{
    public static class ExcelColumnExtensionsEnum
    {
      

        public static File2ObjectManager<TEntity, ICell>
            ColumnEnum<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, TEnum>> action, bool isRequire = true, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            manager.Add(new EnumColumn<TEntity, TEnum>(name, action, isRequire, fromDisplayAttribute));
            return manager;
        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnEnum<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TEnum>> action, bool isRequire = true, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            manager.Add(new EnumColumn<TEntity, TEnum>(name, action, isRequire, fromDisplayAttribute));
            return manager;
        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnEnum<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager, string name,
                Expression<Func<TEntity, TEnum?>> action, bool isRequire = false, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            manager.Add(new EnumColumnNullable<TEntity, TEnum>(name, action, isRequire, fromDisplayAttribute));
            return manager;
        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnEnum<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TEnum?>> action, bool isRequire = false, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            var name = PropertyHelper.GetPropertyInfo(action).Name;
            manager.Add(new EnumColumnNullable<TEntity, TEnum>(name, action, isRequire, fromDisplayAttribute));
            return manager;
        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnEnumDisplayNameAttribute<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TEnum?>> action, bool isRequire = false, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            var name = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return ColumnEnum(manager, name, action, isRequire, fromDisplayAttribute);

        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnEnumDisplayNameAttribute<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TEnum>> action, bool isRequire = false, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            var name = PropertyHelper.GetPropertyNameFromDisplayName(action);
            return ColumnEnum(manager, name, action, isRequire, fromDisplayAttribute);
        }

        public static File2ObjectManager<TEntity, ICell>
            ColumnEnumDisplayAttribute<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TEnum?>> action, bool isRequire = false, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            var name = PropertyHelper.GetPropertyNameFromDisplay(action);
            return ColumnEnum(manager, name, action, isRequire, fromDisplayAttribute);

        }
        public static File2ObjectManager<TEntity, ICell>
            ColumnEnumDisplayAttribute<TEntity, TEnum>(this File2ObjectManager<TEntity, ICell> manager,
                Expression<Func<TEntity, TEnum>> action, bool isRequire = false, bool fromDisplayAttribute = false)
            where TEnum : struct
        {
            var name = PropertyHelper.GetPropertyNameFromDisplay(action);
            return ColumnEnum(manager, name, action, isRequire, fromDisplayAttribute);
        }

        
    }
}