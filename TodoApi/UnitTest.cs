using Xunit;

namespace TodoApi
{
    public class UnitTest
    {

        [Fact]
        public void Test()
        {
            int a = 1;
            int b = 2;

            int sum = a + b;

            Assert.Equal(3, sum);
        }
    }
}
