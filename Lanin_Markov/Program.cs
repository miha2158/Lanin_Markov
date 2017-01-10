using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanin_Markov
{
    class Program
    {
        static void Main(string[] args)
        {
            const string BB = "Л";
            var rules = new string[0];//{"*a->a*", "*b->b*", "*_->.bbb", "_->*"};

            Console.WriteLine("Введите начальную строку \n");
            string inputString = Console.ReadLine();
            Console.WriteLine("\nВведите правила\n {0} - лямбда\n \'->\', " +
                              "\'->.\' - замены\n Конец ввода - пустая строка \n", BB);

            for(int i = 0;;i++)
            {
                var temp = new string[rules.Length + 1];
                for (int j = 0; j < rules.Length; j++)
                {
                    temp[j] = rules[j];
                }
                string tempS = Console.ReadLine();
                if(tempS == string.Empty)
                    break;

                rules = temp;
                rules[i] = tempS;
            }

            bool ruleApplied;
            bool lastRule = false;

            do
            {
                ruleApplied = false;
                foreach (string rule in rules)
                {
                    string L, R;
                    {
                        string[] ruleParts = rule.Split(new[] {"->.", "->"}, StringSplitOptions.None);
                        L = ruleParts[0];
                        R = ruleParts[1];
                    }

                    if (L == BB)
                    {
                        inputString = R + inputString;
                        ruleApplied = true;
                        continue;
                    }

                    if (L.IndexOf(BB) == L.Length - 1)
                        L = L.Remove(L.Length - 1, 1);

                    if (!inputString.Contains(L)) continue;

                    if (R.Contains(BB))
                        R = R.Remove(R.IndexOf(BB), 1);

                    var search = new Regex(Regex.Escape(L));
                    inputString = search.Replace(inputString, R, 1, 0);

                    ruleApplied = true;
                    if (rule.Contains("->."))
                        lastRule = true;
                    break;
                }

                /*if (ruleApplied)
                    Console.WriteLine(inputString);*/

            } while (ruleApplied && !lastRule);

            Console.WriteLine("Результат: {0}", inputString);
            Console.ReadLine();
        }
    }
}