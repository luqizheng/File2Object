using System.ComponentModel.DataAnnotations;

namespace XUnitFile2Object.InvestProject
{
    public enum ProjectStatus
    {
        [Display(Name = "未执行")] Created,
        [Display(Name = "进行中")] Processing,
        [Display(Name = "已完成")] Finish
    }

    public enum SpecialtyPrimaryType
    {
        [Display(Name = "集家客")] FamilyAndCompanyCustomer,
        [Display(Name = "传输")] Trasfer,

        /// <summary>
        ///     专线
        /// </summary>
        [Display(Name = "专线")] DedicatedLine,

        [Display(Name = "室分")] IDS,
        [Display(Name = "无线")] Wireless,
        [Display(Name = "核心")] Core
    }
}
