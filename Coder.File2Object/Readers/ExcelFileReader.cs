using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace Coder.File2Object.Readers
{
    public class ExcelFileReader : IFileReader<ICell>
    {
        /// <summary>
        /// </summary>
        private readonly int _sheetIndex;

        private bool _isXssFile;

        private ISheet _sheet;
        private IWorkbook _workbook;

        /// <summary>
        /// </summary>
        /// <param name="sheetIndex"></param>
        public ExcelFileReader(int sheetIndex = 0)
        {
            _sheetIndex = sheetIndex;
        }

        public void Open(string file)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentException("message", nameof(file));

            _workbook = GetWorkbook(file);
            _sheet = _workbook.GetSheetAt(_sheetIndex);
        }

        public void Close()
        {
        }

        public bool TryRead(int rowIndex, int cellIndex, out ICell cell)
        {
            var row = _sheet.GetRow(rowIndex);
            cell = null;
            if (row == null) return false;

            try
            {
                cell = row.GetCell(cellIndex);

                return cell != null;
            }
            catch (MissingFieldException ex)
            {
                throw new File2ObjectException($"fail to read cell(index={cellIndex})", ex);
            }
        }

        public bool TryRead(int rowIndex, out IEnumerable<ICell> cells)
        {
            var row = _sheet.GetRow(rowIndex);
            var result = new List<ICell>();
            if (row == null)
            {
                cells = null;
                return false;
            }

            for (var cellIndex = 0; cellIndex < row.LastCellNum; cellIndex++)
                try
                {
                    var cell = row.GetCell(cellIndex);
                    result.Add(cell);
                }
                catch (MissingFieldException ex)
                {
                    throw new File2ObjectException($"fail to read cell(index={cellIndex})", ex);
                }

            cells = result;
            return true;
        }

        public bool TryReadInString(int rowIndex, out IEnumerable<string> cellInString)
        {
            var result = TryRead(rowIndex, out var cells);
            var data = new List<string>();
            foreach (var cell in cells)
            {
                cell.SetCellType(CellType.String);
                data.Add(cell.StringCellValue);
            }

            cellInString = data;
            return result;
        }

        public void Write(string file)
        {
            if (file is null) throw new ArgumentNullException(nameof(file));

            using var writeStream = File.Open(file, FileMode.Create, FileAccess.ReadWrite);
            _workbook.Write(writeStream);
            writeStream.Flush();
            writeStream.Close();
        }

        public void WriteTo(int rowIndex, int cellIndex, string value)
        {
            var row = _sheet.GetRow(rowIndex);
            var cell = row.GetCell(cellIndex);
            if (cell == null)
            {
                cell = row.CreateCell(cellIndex);
            }
            cell.SetCellValue(_isXssFile
                ? (IRichTextString)new XSSFRichTextString(value)
                : new HSSFRichTextString(value));
        }

        public string Convert(ICell cell)
        {
            cell.SetCellType(CellType.String);
            return cell.StringCellValue;
        }

        private IWorkbook GetWorkbook(string file)
        {
            var fileStream = File.OpenRead(file);
            _isXssFile = file.EndsWith("xlsx");
            var workbook = _isXssFile
                ? (IWorkbook)new XSSFWorkbook(fileStream)
                : new HSSFWorkbook(new POIFSFileSystem(fileStream));
            fileStream.Close();
            return workbook;
        }
    }
}
