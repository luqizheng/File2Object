using System.ComponentModel.DataAnnotations;

namespace XUnitFile2Object.InvestProject
{
    public class InvestmentProject
    {
        private string _code;
        /// <summary>
        ///     项目
        /// </summary>
        [Display(Name = "项目名称")]
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        [Display(Name = "备注")]
        public string Comment { get; set; }


        /// <summary>
        ///     项目编码
        /// </summary>
        [Display(Name = "项目编码")]
        public string Code
        {
            get => _code;
            set => _code = value?.Trim();
        }

        /// <summary>
        ///     旧项目编码
        /// </summary>
        [Display(Name = "旧项目编码")]
        public string OldCode { get; set; }

        /// <summary>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     创建年份
        /// </summary>
        [Display(Name = "创建年份")]
        public int CreateYear { get; set; }

        /// <summary>
        ///     项目投资，不含税（万元为单位)
        /// </summary>
        [Display(Name = "投资金额(万元)")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     建设内容
        /// </summary>
        [Display(Name = "建设内容")]
        public string ConstructionContent { get; set; }


        /// <summary>
        ///     是否已经决算
        /// </summary>
        [Display(Name = "是否已经决算")]
        public bool HasFinalAccount { get; set; }


        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        public ProjectStatus Status { get; set; }

        /// <summary>
        ///     专业大类
        /// </summary>
        [Display(Name = "专业大类")]
        public SpecialtyPrimaryType? SpecialtyPrimaryType { get; set; }

        /// <summary>
        ///     专业小类
        /// </summary>
        [Display(Name = "专业小类")]
        public ProfessionSecondaryType? SpecialtySecondaryType { get; set; }

        /// <summary>
        ///     负责人
        /// </summary>
        [Display(Name = "负责人")]
        public string Owner { get; set; }
    }
}
