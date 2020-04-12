using System.ComponentModel.DataAnnotations;
using Coder.File2Object;
using Coder.File2Object.Columns;
using Xunit;

namespace XUnitFile2Object.Test2
{
    public enum A
    {
        ALetter1,
        a2
    }

    public enum Classes
    {
        [Display(Name = "数学")] Math,
        [Display(Name = "语文")] Chinese
    }

    public class EnumTest
    {
        public class TypeTest
        {
            [Display(Name = "科目1")]
            public Classes Class1
            {
                get; set;

            }

            [Display(Name = "科目2")]
            public Classes Class2
            {
                get; set;

            }
        }

        public class TypeTestManager : Excel2ObjectManager<TypeTest>
        {
            protected override TypeTest Create()
            {
                return new TypeTest();
            }
        }

        [Fact]
        public void TestMultiType()
        {
            var manager = new TypeTestManager();
            manager.ColumnEnumDisplayNameAttribute(f => f.Class1);
            manager.Column(f => f.Class2);
        }
    }
}