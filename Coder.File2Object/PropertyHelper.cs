using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Coder.File2Object
{
    internal static class PropertyHelper
    {
        public static PropertyInfo GetPropertyInfo<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            var memberSelectorExpression = expression.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property == null) throw new NotSupportedException("只支持属性表达式的");
                return property;
            }

            throw new NotSupportedException("只支持属性表达式的");
        }


        public static string GetPropertyNameFromDisplayName<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            var property = GetPropertyInfo(expression);

            DisplayNameAttribute attr = null;
            foreach (var att in property.GetCustomAttributes(typeof(DisplayNameAttribute)))
            {
                attr = att as DisplayNameAttribute;
                if (attr != null) return attr.DisplayName;
            }

            return property.Name;
        }

        public static string GetPropertyNameFromDisplay<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            var property = GetPropertyInfo(expression);

            DisplayAttribute attr = null;
            foreach (var att in property.GetCustomAttributes(typeof(DisplayAttribute)))
            {
                attr = att as DisplayAttribute;
                if (attr != null) return attr.Name;
            }

            return property.Name;
        }

        public static void SetPropertyValue<T, TValue>(this T target, Expression<Func<T, TValue>> memberLamda,
            TValue value)
        {
            var property = GetPropertyInfo(memberLamda);

            property.SetValue(target, value);
        }

        public static void SetDictionary<T>(this T target,
            string key,
            Expression<Func<T, Dictionary<string, string>>> memberLamda,
            string value)
        {
            var property = GetPropertyInfo(memberLamda);
            var po = property.GetValue(target) as Dictionary<string, string>;
            po.TryAdd(key, value);
        }
    }
}
