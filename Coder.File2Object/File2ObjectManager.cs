using Coder.File2Object.Columns;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Coder.File2Object
{
    public abstract class File2ObjectManager<TEntity, TCell>
    {
        private static readonly Regex _templateRegex = new Regex("\\[[\\w\\d]*?\\]");
        private readonly IList<Column<TEntity, TCell>> _columns = new List<Column<TEntity, TCell>>();
        private readonly IFileReader<TCell> _fileReader;
        private readonly IFileTemplateWriter _fileWriter;
        private int _lastColumn;

        protected File2ObjectManager(IFileReader<TCell> fileReader, IFileTemplateWriter fileWriter)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
        }

        /// <summary>
        /// </summary>
        public int TitleRowIndex { get; set; } = 0;

        /// <summary>
        /// </summary>
        public IEnumerable<string> Titles
        {
            get { return _columns.Select(f => f.Name); }
        }


        public OtherColumnSetting<TEntity, TCell> OtherColumn { get; set; }

        protected abstract TEntity Create();

        public bool TryRead(string file, out IList<ImportResultItem<TEntity>> data, out string resultFile)
        {
            var fileInfo = new FileInfo(file);
            resultFile = GetResultFile(fileInfo);
            data = Read(file);

            var correct = true;
            foreach (var item in data)
                if (item.HasError)
                {
                    correct = false;
                    var errorMessage = item.GetErrors(Titles.ToArray());
                    _fileReader.WriteTo(item.Row, _columns.Count, errorMessage);
                }

            _fileReader.Write(resultFile);

            return correct;
        }

        public void RewriteResultFile(string resultFile, IList<ImportResultItem<TEntity>> data)
        {
            _fileReader.Open(resultFile);
            foreach (var importResult in data)
                _fileReader.WriteTo(importResult.Row, _lastColumn, importResult.GetErrors(Titles.ToArray()));
            _fileReader.Write(resultFile);
        }

        private string GetResultFile(FileInfo file)
        {
            var path = Path.Combine(file.Directory.FullName,
                file.Name.Substring(0, file.Name.Length - file.Extension.Length) + "-结果" + file.Extension);
            var index = 1;
            while (File.Exists(path))
            {
                path = Path.Combine(file.Directory.FullName,
                    file.Name + "-结果" + index + file.Extension);
                index++;
            }

            return path;
        }

        public IList<ImportResultItem<TEntity>> Read(string file)
        {
            _fileReader.Open(file);
            CheckTitles();

            try
            {
                var result = ImportResultItems();
                return result;
            }
            finally
            {
                _fileReader.Close();
            }
        }


        private IList<ImportResultItem<TEntity>> ImportResultItems()
        {
            var result = new List<ImportResultItem<TEntity>>();

            // read titles.
            _fileReader.TryReadInString(TitleRowIndex, out var titleEnum);
            var titles = titleEnum.ToList();
            var rowIndex = TitleRowIndex + 1;


            while (TryGetRows(rowIndex, out var cells))
            {
                var resultItem = new ImportResultItem<TEntity> { Row = rowIndex };
                var entity = resultItem.Data = Create();
                result.Add(resultItem);


                var otherIndex = _columns.Count;

                for (var titleIndex = 0; titleIndex < titles.Count(); titleIndex++)
                {
                    if (titleIndex >= otherIndex) // 带有扩展列
                    {
                        if (OtherColumn == null)
                            break;

                        OtherColumn.Set(entity, cells[titleIndex], titles[titleIndex]);
                        continue;
                    }


                    var column = _columns[titleIndex];
                    //看一下当前行。
                    if (titleIndex < cells.Count)
                    {
                        var cell = cells[titleIndex];
                        SetEntityByColumn(cell, column, resultItem, titleIndex, entity);
                    }
                    else
                    {
                        column.SetEmptyOrNull(entity);
                    }

                }

                rowIndex++;
            }

            return result;
        }

        private void SetEntityByColumn(TCell cell, Column<TEntity, TCell> column, ImportResultItem<TEntity> resultItem,
            int index, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(cell?.ToString()))
            {
                //为空的时候。

                if (column.IsRequire)
                {
                    var errorMessage = BuildErrorMessageByTemplate(column.GetErrorMessageIfEmpty(), column);
                    resultItem.AddError(index, errorMessage);
                }
                else
                {
                    column.SetEmptyOrNull(entity);
                }
            }
            else
            {
                if (!column.TrySetValue(entity, cell, out var errorMessage))
                {
                    errorMessage = BuildErrorMessageByTemplate(errorMessage, column);
                    resultItem.AddError(index, errorMessage);
                }
            }
        }

        private string BuildErrorMessageByTemplate(string errorMessage, Column<TEntity, TCell> column)
        {
            return _templateRegex.Replace(errorMessage, f =>
            {
                switch (f.Value)
                {
                    case ColumnTemplateDefined.ColumnName:
                        return column.Name;
                }

                return f.Value;
            });
            ;
        }

        private bool TryGetRows(int rowIndex, out IList<TCell> result)
        {
            result = new List<TCell>();

            var readAgain = _fileReader.TryRead(rowIndex, out var cells);
            if (!readAgain)
                return false;
            var emptyRow = true;

            foreach (var cell in cells)
            {
                result.Add(cell);
                emptyRow = false;
            }

            return !emptyRow;
        }

        private void CheckTitles()
        {
            var excelTitles = ReadTitles();
            _lastColumn = excelTitles.Count;
            var index = 0;
            foreach (var settingTitle in Titles)
            {
                var fileTitle = excelTitles[index];

                if (settingTitle != fileTitle)
                    throw new TitleNotMatchSettingException(settingTitle, fileTitle);

                index++;
            }
        }

        private List<string> ReadTitles()
        {
            var readResult = _fileReader.TryReadInString(TitleRowIndex, out var titles);
            return readResult ? new List<string>(titles) : new List<string>();
        }

        public void Add(Column<TEntity, TCell> column)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            _columns.Add(column);
        }

        /// <summary>
        ///     写入template 文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="startRow"></param>
        public void WriteTemplateFile(string file, int startRow = 0)
        {
            _fileWriter.WriteTo(file, Titles, startRow);
        }
    }
}
