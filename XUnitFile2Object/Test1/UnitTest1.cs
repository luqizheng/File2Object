using Coder.File2Object;
using Coder.File2Object.Columns;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using Xunit;

namespace XUnitFile2Object.Test1
{
    public class StudentAchievement
    {
        public int Code { get; set; }

        public string Name { get; set; }
        public decimal Achievement { get; set; }

        public DateTime AchievementCreateTime { get; set; }
    }

    public class StudentAchievementImportManager : Excel2ObjectManager<StudentAchievement>
    {
        protected override StudentAchievement Create()
        {
            return new StudentAchievement();
        }
    }

    public class UnitTest1
    {
        [Fact]
        public void ReadExcel()
        {
            var fielName = "test1.xlsx";
            var manager = new StudentAchievementImportManager();

            manager.Column("编码", f => f.Code);
            manager.Column("名称", f => f.Name);
            manager.Column("成绩", f => f.Achievement);
            manager.Column("注册时间", f => f.AchievementCreateTime);
            manager.TryRead(fielName, out var datas, out var resultFile);


            Assert.Equal(7, datas.Count);
            Assert.All(datas, f =>
            {
                Assert.False(f.HasError);
                Assert.NotNull(f.Data);
            });
        }


        [Fact]
        public void ReadExcel_DataTypeNotMatch()
        {
            var fielName = "test_dataType_not_match.xlsx";
            var manager = new StudentAchievementImportManager();

            manager.Column("编码", f => f.Code);
            manager.Column("名称", f => f.Name);
            manager.Column("成绩", f => f.Achievement);
            manager.Column("注册时间", f => f.AchievementCreateTime);
            manager.TryRead(fielName, out var datas, out var resultFile);


            Assert.Equal(3, datas[0].CellErrors.Count);

            Assert.True(datas[0].HasError);
        }


        [Fact]
        public void ReadExcel_String_Require()
        {
            var fielName = "test1_require_string.xlsx";
            var manager = new StudentAchievementImportManager();

            manager.Column("编码", f => f.Code);
            manager.Column("名称", f => f.Name, true);
            manager.Column("成绩", f => f.Achievement);
            manager.Column("注册时间", f => f.AchievementCreateTime);
            manager.TryRead(fielName, out var datas, out var resultFile);


            Assert.Equal(1, datas[0].CellErrors.Count);
            Assert.True(datas[0].HasError);
        }

        [Fact]
        public void ReadExcelEmpty()
        {
            var fielName = "test_empty.xlsx";
            var manager = new StudentAchievementImportManager();
            manager.Column("编码", f => f.Code);
            manager.Column("名称", f => f.Name);
            manager.Column("成绩", f => f.Achievement);
            manager.Column("注册时间", f => f.AchievementCreateTime);
            manager.TryRead(fielName, out var datas, out var resultFile);

            Assert.Equal(0, datas.Count);
        }


        [Fact]
        public void RewriteTemplateFile()
        {
            var fielName = "test_template.xlsx";
            var manager = new StudentAchievementImportManager();
            manager.Column("编码", f => f.Code);
            manager.Column("名称", f => f.Name);
            manager.Column("成绩", f => f.Achievement);
            manager.Column("注册时间", f => f.AchievementCreateTime);
            manager.WriteTemplateFile(fielName);


            var workbook = (IWorkbook)new XSSFWorkbook(fielName);
            var sheet = workbook.GetSheetAt(0);
            var row = sheet.GetRow(0);

            var s = "编码,名称,成绩,注册时间";
            var i = 0;
            foreach (var expect in s.Split(','))
            {
                Assert.Equal(expect, row.Cells[i].StringCellValue);
                i++;
            }
        }
    }
}
