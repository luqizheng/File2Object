using System.Collections.Generic;

namespace Coder.File2Object
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCell"></typeparam>
    public interface IFileReader<TCell>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        void Open(string file);
        /// <summary>
        /// 
        /// </summary>
        void Close();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellIndex"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        bool TryRead(int rowIndex, int cellIndex, out TCell cell);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cells"></param>
        /// <returns></returns>
        bool TryRead(int rowIndex, out IEnumerable<TCell> cells);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellInString"></param>
        /// <returns></returns>
        bool TryReadInString(int rowIndex, out IEnumerable<string> cellInString);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        void Write(string file);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="cellIndex"></param>
        /// <param name="value"></param>
        void WriteTo(int row, int cellIndex, string value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        string Convert(TCell cell);
    }
}
