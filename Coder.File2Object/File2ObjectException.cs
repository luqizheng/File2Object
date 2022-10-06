using System;
using System.Collections.Generic;
using System.Text;

namespace Coder.File2Object
{
    /// <summary>
    /// 
    /// </summary>
    public class File2ObjectException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public File2ObjectException()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public File2ObjectException(string message) : base(message)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public File2ObjectException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
