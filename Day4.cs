using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day4 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input4.txt" };

        public string Name => "Day 4";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            List<string> passwords = ExtractPasswords(inputs[0]);

            List<Regex> regices = new List<Regex>
            {
                new Regex("byr"),
                new Regex("iyr"),
                new Regex("eyr"),
                new Regex("hgt"),
                new Regex("hcl"),
                new Regex("ecl"),
                new Regex("pid"),
            };

            int validpws = 0;
            foreach (string s in passwords)
            {
                bool pwvalid = true;
                int rcount = 0;
                while (pwvalid && rcount < regices.Count)
                {
                    pwvalid = regices[rcount].IsMatch(s);
                    rcount++;
                }

                if (pwvalid)
                {
                    validpws++;
                }
            }

            Helper.Logger.Log(Name, $"valid pw count{validpws}");
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            List<string> passwords = ExtractPasswords(inputs[0]);

            List<Regex> regices = new List<Regex>
            {
                new Regex("byr:((19[2-9][0-9])|2000|2001|2002)"),
                new Regex("iyr:((201[0-9])|2020)"),
                new Regex("eyr:((202[0-9])|2030)"),
                new Regex("hgt:((((1[5-8][0-9])|190|191|192|193)cm)|((59|60|61|62|63|64|65|66|67|68|69|70|71|72|73|74|75|76)in))"),
                new Regex(@"hcl:\#(([a-f]|\d){6})"),
                new Regex("ecl:(amb|blu|brn|gry|grn|hzl|oth)"),
                new Regex(@"pid:\d{9}($|\s)"), // this was the tricky part, original pattern pid:\d{9} matched 10 characters pid (3966920279)
            };

            int validpws = 0;
            foreach (string s in passwords)
            {
                bool pwvalid = true;
                int rcount = 0;
                while (pwvalid && rcount < regices.Count)
                {
                    pwvalid = regices[rcount].IsMatch(s);
                    rcount++;
                }

                if (pwvalid)
                {
                    validpws++;
                    Helper.Logger.Log(Name, $"{s} {pwvalid}{Environment.NewLine}");
                }
                else
                {
                    Helper.Logger.Log(Name, $"{s} {pwvalid}   regex failed: {regices[rcount - 1]}{Environment.NewLine}");
                }
            }

            Helper.Logger.Log(Name, $"valid pw count{validpws}");

            Solve2Alternative(passwords);
        }

        private void Solve2Alternative(List<string> passwords)
        {
            Regex paramSplitter = new Regex(@"\w\w\w:\#?\w*");

            var validations = new Dictionary<string, Func<string, bool>>
            {
                { "byr", s => int.Parse(s).Between(1920, 2002) },
                { "iyr", s => int.Parse(s).Between(2010, 2020) },
                { "eyr", s => int.Parse(s).Between(2020, 2030) },
                { "hgt", s => s.Substring(s.Length - 2, 2) == "cm" ? s.Length == 5 && int.Parse(s[0..3]).Between(150, 193) : s.Length == 4 && int.Parse(s[0..2]).Between(59, 76) },
                { "hcl", s => s.Length == 7 && s.AllChars(1, 6, CharRange.HexaDecimal) },
                { "ecl", s => s.IsOneOf("amb", "blu", "brn", "gry", "grn", "hzl", "oth") },
                { "pid", s => s.Length == 9 && s.AllChars(0, 8, CharRange.Numeric) },
            };

            int validpws = 0;
            Dictionary<string, string> passwordparams;
            string failedparam = string.Empty;
            foreach (string pw in passwords)
            {
                passwordparams = new Dictionary<string, string>();
                var psplitted = paramSplitter.Matches(pw);
                foreach (Match m in psplitted)
                {
                    var p = m.Value.Split(':');
                    passwordparams.Add(p[0], p[1]);
                }

                bool pwvalid = true;
                foreach (var v in validations)
                {
                    if (!passwordparams.ContainsKey(v.Key) || !v.Value(passwordparams[v.Key]))
                    {
                        pwvalid = false;
                        failedparam = v.Key;
                        break;
                    }
                }

                if (pwvalid)
                {
                    validpws++;
                    Helper.Logger.Log(Name, $"{pw} {pwvalid}{Environment.NewLine}");
                }
                else
                {
                    Helper.Logger.Log(Name, $"{pw} {pwvalid}   param failed: {failedparam}{Environment.NewLine}");
                }
            }

            Helper.Logger.Log(Name, $"valid pw count{validpws}");
        }

        private List<string> ExtractPasswords(IList<string> inputs)
        {
            List<string> passwords = new List<string>();
            string pw = string.Empty;

            foreach (string s in inputs)
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    passwords.Add(pw);
                    pw = string.Empty;
                }
                else
                {
                    pw += " " + s;
                }
            }

            passwords.Add(pw);
            return passwords;
        }
    }
}
