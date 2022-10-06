using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coder.File2Object
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ImportResultItem<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<CellInfo> CellErrors { get; } = new List<CellInfo>();
        /// <summary>
        /// 
        /// </summary>
        public IList<CellInfo> CellWarnings { get; } = new List<CellInfo>();
        /// <summary>
        /// 
        /// </summary>
        public bool HasError => CellErrors.Any();

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="titles"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string GetErrors(string[] titles)
        {
            var sb = new StringBuilder();
            foreach (var error in CellWarnings)
            {
                if (titles.Length < error.CellIndex && error.CellIndex >= 0)
                    throw new ArgumentOutOfRangeException(nameof(error), error.CellIndex + " do not match titles");
                if (error.CellIndex > 0)
                    sb.Append("[" + titles[error.CellIndex] + "]" + error.Message + " ");
                else
                    sb.Append(error.Message);
            }

            foreach (var error in CellErrors)
            {
                if (titles.Length < error.CellIndex && error.CellIndex >= 0)
                    throw new ArgumentOutOfRangeException(nameof(error), error.CellIndex + " do not match titles");
                if (error.CellIndex > 0)
                    sb.Append("[" + titles[error.CellIndex] + "]" + error.Message + " ");
                else
                    sb.Append(error.Message);
            }

            var r = sb.ToString();
            return r;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddWarning(string message)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("message", nameof(message));

            CellWarnings.Add(new CellInfo
            {
                CellIndex = -1,
                Message = message
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddWarning(int cellIndex, string message)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("message", nameof(message));


            CellWarnings.Add(new CellInfo
            {
                CellIndex = cellIndex,
                Message = message
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <param name="errorMessage"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddError(int cellIndex, string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage)) throw new ArgumentException("message", nameof(errorMessage));

            CellErrors.Add(new CellInfo
            {
                CellIndex = cellIndex,
                Message = errorMessage
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddError(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage)) throw new ArgumentException("message", nameof(errorMessage));

            CellErrors.Add(new CellInfo
            {
                CellIndex = -1,
                Message = errorMessage
            });
        }
    }
}
