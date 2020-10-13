using System.ComponentModel.DataAnnotations;
using Coder.File2Object;
using Xunit;

namespace XUnitFile2Object.Test1
{
#if DEBUG
    public class EnumHelperTest
    {
        public enum SomeEnuym
        {
            [Display(Name = "c")]
            a,
        }
        [Fact]
        public void TestGetDisplayNmae()
        {
            var re = SomeEnuym.a.GetEnumDisplayName();
            Assert.Equal("c", re);
        }
    }
#endif
}
