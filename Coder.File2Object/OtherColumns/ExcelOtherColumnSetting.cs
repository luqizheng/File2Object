using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Coder.File2Object.OtherColumns
{
    public class ExcelOtherColumnSetting<TEntity> :
        OtherColumnSetting<TEntity, ICell>
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
