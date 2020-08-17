using System.ComponentModel.DataAnnotations;

namespace XUnitFile2Object.InvestProject
{
    /// <summary>
    /// 大专业
    /// </summary>
    public enum ProfessionType
    {
        /// <summary>
        ///     预覆盖
        /// </summary>
        [Display(Name = "预覆盖")] PreCoverage,

        /// <summary>
        ///     无线
        /// </summary>
        [Display(Name = "无线")] Wireless
    }
}
