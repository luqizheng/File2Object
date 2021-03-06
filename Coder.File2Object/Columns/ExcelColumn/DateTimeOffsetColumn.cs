﻿using System;
using System.Linq.Expressions;
using NPOI.SS.UserModel;

namespace Coder.File2Object.Columns.ExcelColumn
{
    public class DateTimeOffsetColumn<TEntity> : Column<TEntity, ICell, DateTimeOffset>
    {
        public DateTimeOffsetColumn(string name, Expression<Func<TEntity, DateTimeOffset>> action,
            bool isRequire = true) : base(name, action, isRequire)
        {
        }

        protected override bool TryConvert(ICell cell, out DateTimeOffset val, out string errorMessage)
        {
            errorMessage = null;
            val = default;
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        val = cell.DateCellValue;
                        return true;
                    }

                    break;
            }

            cell.SetCellType(CellType.String);
            var valStr = cell.StringCellValue?.Trim();
            var result = DateTimeOffset.TryParse(valStr, out val);

            if (result == false) errorMessage = $"无法把{valStr}转化为有效的日期类型";

            return result;
        }

        public override string GetErrorMessageIfEmpty()
        {
            return $"{ColumnTemplateDefined.ColumnName}必须输入日期类型";
        }
    }
}