 
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

   manager.Column("����", f => f.Code);
   manager.Column("����", f => f.Name, true);
   manager.Column("�ɼ�", f => f.Achievement);
   manager.Column("ע��ʱ��", f => f.AchievementCreateTime);
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
            this.Column("����", f => f.Code);
            this.Column("����", f => f.Name);
            this.Column("�ɼ�", f => f.Achievement);
            this.Column("ע��ʱ��", f => f.AchievementCreateTime);
            //export Dictionary<string, string>
            OtherColumn = new ExcelOtherColumnSetting<StudentAchievementExtension>(f => f.Extensions);
        }

        protected override StudentAchievementExtension Create()
        {
            return new StudentAchievementExtension();
        }
    }
```