using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Coder.File2Object
{
    public interface IOtherColumnSetting
    {
        public bool Set(object entity, object cell, string titleName);
    }

    public abstract class OtherColumnSetting<TEntity, TCell, TReturnValue> : IOtherColumnSetting
    {
        private readonly Expression<Func<TEntity, Dictionary<string, TReturnValue>>> _action;

        public OtherColumnSetting(Expression<Func<TEntity, Dictionary<string, TReturnValue>>> action)
        {
            _action = action;
        }

        bool IOtherColumnSetting.Set(object entity, object cell, string titleName)
        {
            return Set((TEntity)entity, (TCell)cell, titleName);
        }

        public bool Set(TEntity entity, TCell cell, string titleName)
        {
            var result = TryConvert(cell, out var value, out var message);
            entity.SetDictionary(titleName, _action, value);
            return result;
        }

        protected abstract bool TryConvert(TCell cell, out TReturnValue val, out string errorMessage);
    }
}