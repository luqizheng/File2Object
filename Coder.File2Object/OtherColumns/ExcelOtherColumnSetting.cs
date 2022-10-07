using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.OtherColumns
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TReturnValue"></typeparam>
    public class ExcelOtherColumnSetting<TEntity, TReturnValue> :
        OtherColumnSetting<TEntity, ICell, TReturnValue>
    {
        private readonly Func<string, TReturnValue> _convertToValue;

        /// <summary>
        /// </summary>
        /// <param name="action"></param>
        /// <param name="convertToValue"></param>
        public ExcelOtherColumnSetting(Expression<Func<TEntity, Dictionary<string, TReturnValue>>> action, Func<string, TReturnValue> convertToValue)
            : base(action)
        {
            _convertToValue = convertToValue;
        }

        /// <summary>
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="val"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        protected override bool TryConvert(ICell cell, out TReturnValue val, out string errorMessage)
        {
            errorMessage = null;
            cell.SetCellType(CellType.String);
            var strValue = cell.StringCellValue?.Trim();
            val = _convertToValue(strValue);
            return true;
        }
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ExcelOtherColumnSetting<TEntity> :
        OtherColumnSetting<TEntity, ICell, string>
    {
        /// <summary>
        /// </summary>
        /// <param name="action"></param>
        public ExcelOtherColumnSetting(Expression<Func<TEntity, Dictionary<string, string>>> action)
            : base(action)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="val"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        protected override bool TryConvert(ICell cell, out string val, out string errorMessage)
        {
            errorMessage = null;
            cell.SetCellType(CellType.String);
            val = cell.StringCellValue?.Trim();
            return true;
        }
    }
}