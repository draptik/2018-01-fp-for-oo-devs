using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using LanguageExt;
using static LanguageExt.Prelude;
using Xunit;

namespace LanguageExtDemo
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var optional = Some(123);

            // Version 1
            int x1 = optional.Match(
                Some: v => v * 2,
                None: () => 0);

            // Version 2 (functional)
            int x2 = match(optional,
                Some: v => v * 2,
                None: () => 0);

            // Version 3 (fluent)
            int x3 = optional
                .Some(v => v * 2)
                .None(() => 0);

            x1.Should().BeOfType(typeof(int));
        }

        public int Sum(IEnumerable<int> list) =>
            match(list,
                () => 0,
                (x, xs) => x + Sum(xs));

        [Fact]
        public void RecursiveMatchSumTest()
        {
            var list0 = List<int>();
            var list1 = List(10);
            var list5 = List(10, 20, 30, 40, 50);

            Sum(list0).Should().Be(0);
            Sum(list1).Should().Be(10);
            Sum(list5).Should().Be(150);
        }

        //[Fact]
        //public void AlternativeSumTest()
        //{
        //    var list = List(10, 20, 30, 40, 50);
        //    var total = fold(list, 0, (s, x) => s + x);
        //}
    }
}
