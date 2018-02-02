using System;
using FluentAssertions;
using Xunit;

namespace DemoTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Func<int, bool> isLargerThanFive = x => x > 5;
            // Func<int, bool> isSmallerThenTen = x => x < 10;

            // Func<int, bool> isBetweenFiveAndTen = x => 
            //     isLargerThanFive(x) && isSmallerThenTen(x);

            // isBetweenFiveAndTen(7)
            //     .Should().BeTrue();


            Func<int, bool> IsDivisibleBy(int divisor) => num => num % divisor == 0;
            var isDivisibleByFive = IsDivisibleBy(5);

            isDivisibleByFive(10).Should().BeTrue();
        }
    }
}
