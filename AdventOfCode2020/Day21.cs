using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Day21Help;

namespace AdventOfCode2020
{
    public class Day21 : SimpleDay
    {
        private bool[] found;

        public override IList<string> InputFiles => new List<string> { "input21test.txt", "input21.txt" };

        public override string Name => "Day 21";

        public List<Food> Foods { get; private set; }

        public List<string> Allergens { get; private set; }

        protected override void SolvePuzzle1(IList<string> input)
        {
            ReadInput(input);
            BuildAllergenDict();
            List<string> notFoundIngs = new List<string>();
            foreach (Food food in Foods)
            {
                foreach (string s in food.NotIdentifiedIngridients())
                {
                    if (!notFoundIngs.Contains(s))
                    {
                        notFoundIngs.Add(s);
                    }
                }
            }

            int cnt = 0;
            foreach (string s in notFoundIngs)
            {
                foreach (Food food in Foods)
                {
                    if (food.HasIngridient(s))
                    {
                        cnt++;
                    }
                }
            }

            Helper.Logger.Log(Name, $"part 1 cnt: {cnt}");
        }

        protected override void SolvePuzzle2(IList<string> input)
        {
            ReadInput(input);
            Dictionary<string, List<string>> allergensingridients = BuildAllergenDict();

            List<string> danger = new List<string>();
            Allergens.Sort();
            foreach (string allergen in Allergens)
            {
                danger.Add(allergensingridients[allergen][0]);
            }

            Helper.Logger.Log(Name, $"part 2 {string.Join(",", danger)}");
        }

        private Dictionary<string, List<string>> BuildAllergenDict()
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            while (NotFoundAllergens().Count > 0)
            {
                foreach (string allerg in NotFoundAllergens())
                {
                    foreach (Food food in Foods)
                    {
                        if (food.HasAllergen(allerg))
                        {
                            if (dict.ContainsKey(allerg))
                            {
                                dict[allerg] = dict[allerg].Intersect(food.NotIdentifiedIngridients()).ToList();
                            }
                            else
                            {
                                List<string> ings = food.NotIdentifiedIngridients();
                                dict.Add(allerg, ings);
                            }
                        }

                        if (dict.ContainsKey(allerg) && dict[allerg].Count == 1)
                        {
                            foreach (Food fooding in Foods)
                            {
                                fooding.SetIng(dict[allerg][0]);
                            }

                            found[Allergens.IndexOf(allerg)] = true;

                            break;
                        }
                    }
                }
            }

            return dict;
        }

        private IList<string> NotFoundAllergens()
        {
            List<string> re = new List<string>();
            for (int i = 0; i < Allergens.Count; i++)
            {
                if (!found[i])
                {
                    re.Add(Allergens[i]);
                }
            }

            return re;
        }

        private void ReadInput(IList<string> input)
        {
            Foods = new List<Food>();
            Allergens = new List<string>();
            foreach (string s in input)
            {
                string[] ings = s[0..(s.IndexOf('(') - 1)].Split(' ');
                string[] allergs = s[(s.IndexOf('(') + 10)..^1].Split(',');

                for (int i = 0; i < allergs.Length; i++)
                {
                    allergs[i] = allergs[i].Trim();
                }

                Foods.Add(new Food(ings, allergs));
                foreach (string allerg in allergs)
                {
                    if (!Allergens.Contains(allerg))
                    {
                        Allergens.Add(allerg);
                    }
                }
            }

            found = new bool[Allergens.Count];
        }
    }
}
