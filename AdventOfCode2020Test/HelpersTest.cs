using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2020;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2020.Day19Help;
using System.Text.RegularExpressions;
using AdventOfCode2020.Day20Help;

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

        [TestMethod]
        public void Test19()
        {
            List<string> s = new List<string>
            {
                "0: 1 2",
                "1: 3 4 | 3 1 4",
                "2: \"c\"",
                "3: \"a\"",
                "4: \"b\"",
                "",
            };
            var day = new Day19();

            day.GetEmptyLineAndRuleCnt(s, out int el, out int rulecnt);
            Rule.Rules = day.GetRules(s, el, rulecnt);
            Console.WriteLine(Rule.Rules[0].Pattern);

            Regex regex = new Regex("^" + Rule.Rules[0].Pattern + "$");

            Assert.IsTrue(regex.IsMatch("abc"));
            Assert.IsTrue(regex.IsMatch("aabc"));
            Assert.IsTrue(regex.IsMatch("abbc"));
            Assert.IsTrue(regex.IsMatch("aabbc"));
            Assert.IsFalse(regex.IsMatch("c"));
            Assert.IsFalse(regex.IsMatch("ab"));
            Assert.IsFalse(regex.IsMatch("abcc"));
        }

        [TestMethod]
        public void FindSeamonster()
        {
            bool[,] map = new bool[,]
            {
                { false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false, },
                { true,false,false,false,false,true,true,false,false,false,false,true,true,false,false,false,false,true,true,true, },
                { false,true,false,false,true,false,false,true,false,false,true,false,false,true,false,false,true,false,false,false,},
            };

            Day20 day = new Day20();
            Tile tile = new Tile(0, map);
            Tile variant = tile.Variations[0];
            int v = 0;
            SeaMonster seaMonster = new SeaMonster();
            while (!day.HasSeaMonster(variant, seaMonster))
            {
                v++;
                if (v == tile.Variations.Count)
                {
                    Assert.Fail("No seamonster found");
                }

                variant = tile.Variations[v];
            }

        }
    }
}
