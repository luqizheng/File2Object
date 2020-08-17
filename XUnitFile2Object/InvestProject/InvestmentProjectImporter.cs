using Coder.File2Object;
using Coder.File2Object.Columns;

namespace XUnitFile2Object.InvestProject
{
    internal class InvestmentProjectImporter : Excel2ObjectManager<InvestmentProject>
    {
        public InvestmentProjectImporter()
        {
            this.Column("项目名称", _ => _.Name);
            this.Column("项目编号", _ => _.Code);
            this.Column("旧项目编号", _ => _.OldCode);
            this.ColumnEnum("专业大类", _ => _.SpecialtyPrimaryType, fromDisplayAttribute: true);
            this.ColumnEnum("专业小类", _ => _.SpecialtySecondaryType, fromDisplayAttribute: true);
            this.Column("下达年份", _ => _.CreateYear);
            this.Column("投资金额（万元）", _ => _.Amount);
            this.Column("建设内容", _ => _.ConstructionContent);
            this.ColumnEnum("状态", _ => _.Status, fromDisplayAttribute: true);
            this.Column("负责人", _ => _.Owner);
            this.Column("描述", _ => _.Comment);
            this.Column("是否已结算", "是", _ => _.HasFinalAccount);
        }

        protected override InvestmentProject Create()
        {
            return new InvestmentProject();
        }
    }
}
