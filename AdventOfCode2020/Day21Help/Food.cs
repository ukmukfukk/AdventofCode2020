using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Day21Help
{
    public class Food
    {
        public Food(string[] ingridients, string[] allergens)
        {
            Ingridients = new List<string>(ingridients);
            Allergens = new List<string>(allergens);
            Identified = new bool[Ingridients.Count];
        }

        public List<string> Ingridients { get; }

        public List<string> Allergens { get; }

        public bool[] Identified { get; }

        public bool HasAllergen(string allerg)
        {
            return Allergens.Contains(allerg);
        }

        public List<string> NotIdentifiedIngridients()
        {
            List<string> re = new List<string>();
            for (int i = 0; i < Ingridients.Count; i++)
            {
                if (!Identified[i])
                {
                    re.Add(Ingridients[i]);
                }
            }

            return re;
        }

        public void SetIng(string ing)
        {
            int ind = Ingridients.IndexOf(ing);
            if (ind == -1)
            {
                return;
            }

            if (Identified[ind])
            {
                throw new Exception("Something bad has happened");
            }

            Identified[ind] = true;
        }

        public bool HasIngridient(string s)
        {
            return Ingridients.Contains(s);
        }
    }
}
