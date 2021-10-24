 
# Quick Start


```
    // object class
    public class StudentAchievement
    {
        public int Code { get; set; }

        public string Name { get; set; }
        public decimal Achievement { get; set; }

        public DateTime AchievementCreateTime { get; set; }
    }

   var fielName = "test1_require_string.xlsx";
   var manager = new StudentAchievementImportManager();

   manager.Column("编码", f => f.Code);
   manager.Column("名称", f => f.Name, true);
   manager.Column("成绩", f => f.Achievement);
   manager.Column("注册时间", f => f.AchievementCreateTime);
   manager.TryRead(fielName, out var datas, out var resultFile);

   Assert.Equal(1, datas[0].CellErrors.Count);
   
```

## Dynamic Column
```
 public class StudentAchievementImportManagerExtension
        : Excel2ObjectManager<StudentAchievementExtension>
    {
        public StudentAchievementImportManagerExtension()
        {
            this.Column("编码", f => f.Code);
            this.Column("名称", f => f.Name);
            this.Column("成绩", f => f.Achievement);
            this.Column("注册时间", f => f.AchievementCreateTime);
            //export Dictionary<string, string>
            OtherColumn = new ExcelOtherColumnSetting<StudentAchievementExtension>(f => f.Extensions);
        }

        protected override StudentAchievementExtension Create()
        {
            return new StudentAchievementExtension();
        }
    }
```