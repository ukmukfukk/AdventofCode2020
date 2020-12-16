using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2020;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020Test
{
    [TestClass]
    public class HelpersTest
    {
        [TestMethod]
        public void TestPermutation()
        {
            var actual = Helper.GetPermuations(new List<int> { 1, 4, 5 });
            var expected = new List<List<int>>
            {
                new List<int> { 1, 4, 5 },
                new List<int> { 1, 5, 4},
                new List<int> {4, 1, 5},
                new List<int> {4, 5, 1},
                new List<int> {5, 1, 4 },
                new List<int> {5, 4, 1}
            };

            foreach (var list in expected)
            {
                if (!actual.Any(act => act[0] == list[0] && act[1] == list[1] && act[2] == list[2]))
                {
                    Assert.Fail($"Missing line of expected permuatations: {list[0]}, {list[1]}, {list[2]}");
                }
            }
        }

        [TestMethod]
        public void TestPermutationLong()
        {
            var actual = Helper.GetPermuations(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });
            var sb = new StringBuilder();
            int i = 0;
            foreach (var a in actual)
            {
                if (i == 100)
                {
                    break;
                }

                for (int j = 0; j < 20; j++)
                {
                    sb.Append($"{a[j]}, ");
                }

                sb.AppendLine();
                i++;
            }

            Console.WriteLine(sb);
        }
    }
}
