using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day14 : IDay
    {
        public IList<string> InputFiles => new List<string> { "input14test.txt", "input14test2.txt", "input14.txt" };

        public string Name => "Day 14";

        public void SolvePuzzle1(IList<IList<string>> inputs)
        {
            foreach (var input in inputs)
            {
                SolvePuzzle1(input);
            }
        }

        public void SolvePuzzle2(IList<IList<string>> inputs)
        {
            for (int i = 1; i < inputs.Count; i++)
            {
                SolvePuzzle2(inputs[i]);
            }
        }

        private void SolvePuzzle2(IList<string> input)
        {
            string currentmask = string.Empty;
            string[] temp;
            Dictionary<long, long> memory = new Dictionary<long, long>();
            Regex sqbrackets = new Regex(@"\[(\d*)\]");

            foreach (string line in input)
            {
                temp = line.Split("=");
                if (temp[0].Contains("mask"))
                {
                    currentmask = temp[1].Trim();
                }

                if (temp[0].Contains("mem"))
                {
                    long addressBase = long.Parse(sqbrackets.Match(temp[0]).Groups[1].Value);
                    long value = long.Parse(temp[1].Trim());
                    List<long> addresses = GetAllMaskedAddresses(currentmask, addressBase);
                    foreach (long address in addresses)
                    {
                        if (memory.ContainsKey(address))
                        {
                            memory[address] = value;
                        }
                        else
                        {
                            memory.Add(address, value);
                        }
                    }
                }
            }

            long sum = 0;
            foreach (var pair in memory)
            {
                sum += pair.Value;
            }

            Helper.Logger.Log(Name, $"sum: {sum}");
        }

        private List<long> GetAllMaskedAddresses(string mask, long addressBase)
        {
            string address = Convert.ToString(addressBase, 2).PadLeft(36, '0');
            string masked = GetNewMask(mask, address);
            int index = masked.IndexOf('X');

            List<int> x = new List<int>();
            while (index > -1)
            {
                x.Add(index);
                index = masked.IndexOf('X', index + 1);
            }

            int countX = x.Count;
            if (countX == 0)
            {
                return new List<long> { Convert.ToInt64(masked, 2) };
            }

            string submask;
            char[] newaddress;
            long pow2 = (long)Math.Pow(2, countX);
            List<long> retval = new List<long>();
            for (long i = 0; i < pow2; i++)
            {
                newaddress = masked.ToCharArray();
                submask = Convert.ToString(i, 2).PadLeft(countX, '0');
                for (int j = 0; j < x.Count; j++)
                {
                    newaddress[x[j]] = submask[j];
                }

                retval.Add(Convert.ToInt64(CharArrayToString(newaddress), 2));
            }

            return retval;
        }

        private string GetNewMask(string mask, string address)
        {
            StringBuilder sbmasked = new StringBuilder();
            for (int i = 0; i < 36; i++)
            {
                if (mask[i] == '0')
                {
                    sbmasked.Append(address[i]);
                }
                else
                {
                    sbmasked.Append(mask[i]);
                }
            }

            string masked = sbmasked.ToString();
            return masked;
        }

        private string CharArrayToString(char[] newaddress)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in newaddress)
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        private void SolvePuzzle1(IList<string> input)
        {
            string currentmask = string.Empty;
            string[] temp;
            Dictionary<long, long> memory = new Dictionary<long, long>();
            Regex sqbrackets = new Regex(@"\[(\d*)\]");

            foreach (string line in input)
            {
                temp = line.Split("=");
                if (temp[0].Contains("mask"))
                {
                    currentmask = temp[1].Trim();
                }

                if (temp[0].Contains("mem"))
                {
                    long address = long.Parse(sqbrackets.Match(temp[0]).Groups[1].Value);
                    long value = MaskedValue(currentmask, long.Parse(temp[1].Trim()));
                    if (memory.ContainsKey(address))
                    {
                        memory[address] = value;
                    }
                    else
                    {
                        memory.Add(address, value);
                    }
                }
            }

            long sum = 0;
            foreach (var pair in memory)
            {
                sum += pair.Value;
            }

            Helper.Logger.Log(Name, $"sum: {sum}");
        }

        private long MaskedValue(string mask, long v)
        {
            string bitstring = Convert.ToString(v, 2).PadLeft(36, '0');
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < 36; i++)
            {
                if (mask[i] == 'X')
                {
                    result.Append(bitstring[i]);
                }
                else
                {
                    result.Append(mask[i]);
                }
            }

            return Convert.ToInt64(result.ToString(), 2);
        }
    }
}
