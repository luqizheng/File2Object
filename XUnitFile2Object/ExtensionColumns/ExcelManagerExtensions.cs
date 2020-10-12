using Coder.File2Object;
using Coder.File2Object.Columns;
using Coder.File2Object.OtherColumns;
using System.Collections.Generic;
using Xunit;
using XUnitFile2Object.Test1;

namespace XUnitFile2Object.ExtensionColumns
{
    public class StudentAchievementExtension : StudentAchievement
    {
        public Dictionary<string, string> Extensions { get; set; } = new Dictionary<string, string>();
    }

    public class StudentAchievementImportManagerExtension
        : Excel2ObjectManager<StudentAchievementExtension>
    {
        public StudentAchievementImportManagerExtension()
        {
            this.Column("编码", f => f.Code);
            this.Column("名称", f => f.Name);
            this.Column("成绩", f => f.Achievement);
            this.Column("注册时间", f => f.AchievementCreateTime);
            OtherColumn = new ExcelOtherColumnSetting<StudentAchievementExtension>(f => f.Extensions);
        }

        protected override StudentAchievementExtension Create()
        {
            return new StudentAchievementExtension();
        }
    }

    public class ExcelTest
    {
        [Fact]
        public void Test()
        {
            var manager = new StudentAchievementImportManagerExtension();
            var fileName = "test1_Extensions.xlsx";
            manager.TryRead(fileName, out var datas, out var resultFile);


            Assert.Equal(7, datas.Count);
            Assert.All(datas, f =>
            {
                Assert.False(f.HasError);
                Assert.NotNull(f.Data);
            });

            Assert.All(datas, f =>
            {
                Assert.Equal("1", f.Data.Extensions["其他1"]);
                Assert.Equal("2", f.Data.Extensions["其他2"]);
                Assert.Equal("3", f.Data.Extensions["其他3"]);
            });
        }
    }
}
