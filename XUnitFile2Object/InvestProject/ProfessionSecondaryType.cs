using System.ComponentModel.DataAnnotations;

namespace XUnitFile2Object.InvestProject
{
    /// <summary>
    ///     专业小类
    /// </summary>
    public enum ProfessionSecondaryType
    {
        /// <summary>
        ///     集团客户预覆盖
        /// </summary>
        [Display(Name = "集客预覆盖")] CustomerGroupCoverage,

        /// <summary>
        ///     家庭预覆盖
        /// </summary>
        [Display(Name = "家客预覆盖")] FamilyCoverage,
        [Display(Name = "管道")] Pipe,
        [Display(Name = "WLAN")] WLAN
    }
}
