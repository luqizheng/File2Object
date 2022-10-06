using System.Collections.Generic;

namespace Coder.File2Object
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFileTemplateWriter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="titles"></param>
        /// <param name="startRow"></param>
        void WriteTo(string file, IEnumerable<string> titles, int startRow = 0);
    }
}