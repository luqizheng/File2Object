using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Coder.File2Object.OtherColumns
{
    public class ExcelOtherColumnSetting<TEntity, TReturnValue> :
        OtherColumnSetting<TEntity, ICell, TReturnValue>
    {
        private readonly Func<string, TReturnValue> _convertToValue;

        public ExcelOtherColumnSetting(Expression<Func<TEntity, Dictionary<string, TReturnValue>>> action, Func<string, TReturnValue> convertToValue)
            : base(action)
        {
            _convertToValue = convertToValue;
        }

        protected override bool TryConvert(ICell cell, out TReturnValue val, out string errorMessage)
        {
            errorMessage = null;
            cell.SetCellType(CellType.String);
            var strValue = cell.StringCellValue?.Trim();
            val = _convertToValue(strValue);
            return true;
        }

    }

    public class ExcelOtherColumnSetting<TEntity> :
        OtherColumnSetting<TEntity, ICell, string>
    {

        public ExcelOtherColumnSetting(Expression<Func<TEntity, Dictionary<string, string>>> action)
            : base(action)
        {

        }

        protected override bool TryConvert(ICell cell, out string val, out string errorMessage)
        {
            errorMessage = null;
            cell.SetCellType(CellType.String);
            val = cell.StringCellValue?.Trim();
            return true;
        }

    }
}
