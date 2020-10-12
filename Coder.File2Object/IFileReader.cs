using System.Collections.Generic;

namespace Coder.File2Object
{
    public interface IFileReader<TCell>
    {
        void Open(string file);
        void Close();
        bool TryRead(int rowIndex, int cellIndex, out TCell cell);
        bool TryRead(int rowIndex, out IEnumerable<TCell> cells);

        bool TryReadInString(int rowIndex, out IEnumerable<string> cellInString);
        void Write(string file);

        void WriteTo(int row, int cellIndex, string value);

        string Convert(TCell cell);
    }

    public interface IFileTemplateWriter
    {
        void WriteTo(string file, IEnumerable<string> titles, int startRow = 0);
    }
}
