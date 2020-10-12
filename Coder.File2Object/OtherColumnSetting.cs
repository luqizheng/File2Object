using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Coder.File2Object
{
    public abstract class OtherColumnSetting<TEntity, TCell>
    {
        private readonly Expression<Func<TEntity, Dictionary<string, string>>> _action;

        public OtherColumnSetting(Expression<Func<TEntity, Dictionary<string, string>>> action)
        {
            _action = action;
        }

        public bool Set(TEntity entity, TCell cell, string titleName)
        {
            var result = TryConvert(cell, out string value, out string message);
            entity.SetDictionary(titleName, _action, value);
            return result;
        }

        protected abstract bool TryConvert(TCell cell, out string val, out string errorMessage);
    }
}
