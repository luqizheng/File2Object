using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Coder.File2Object.Readers
{
    public class ExcelFileWriter : IFileTemplateWriter
    {



        public void WriteTo(int rowIndex, int cellIndex, string value, ISheet _sheet, bool _isXssFile)
        {
            var row = _sheet.GetRow(rowIndex);
            if (row == null)
            {
                row = _sheet.CreateRow(rowIndex);
            }
            var cell = row.GetCell(cellIndex, MissingCellPolicy.CREATE_NULL_AS_BLANK);
            cell.SetCellValue(_isXssFile
                ? (IRichTextString)new XSSFRichTextString(value)
                : new HSSFRichTextString(value));
        }



        private IWorkbook GetWorkbook(string file, out bool isXssFile)
        {
            isXssFile = file.EndsWith("xlsx");
            IWorkbook workbook = isXssFile
                ? (IWorkbook)new XSSFWorkbook()
                : new HSSFWorkbook(new POIFSFileSystem());

            return workbook;
        }

        public void WriteTo(string file, IEnumerable<string> titles, int startRow = 0)
        {
            var _workbook = GetWorkbook(file, out var isXssFile);
            var _sheet = _workbook.CreateSheet();
            int index = 0;
            foreach (var title in titles)
            {
                WriteTo(startRow, index, title, _sheet, isXssFile);
                index++;
            }

            using var writeStream = File.Open(file, FileMode.Create, FileAccess.ReadWrite);
            _workbook.Write(writeStream);
            if (writeStream.CanWrite)
            {
                writeStream.Flush();
                writeStream.Close();
            }
        }
    }
}