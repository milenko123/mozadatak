using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mozadatak
{
    public class CombinationsGenerator : Expression
    {
        public static List<Expression> GenerateCombinationsTest(int[] input, List<string> operators, int count)
        {
            if (count == 0)
            {
                //vracanje liste sa drugim listama gde svaka sadrzi brojeve i operatore
                return input.Select(n => new Expression
                {
                    TextExpression = n.ToString(),
                    Input = new List<int>{ n },
                    Operators = new List<string>(),
                    Target = new int(),
                    //parentheses = new List<string>()
                }).ToList();
            }
            else
            {
                var result = new List<Expression>();

                //rekurzivna metoda koja vraca sve kombinacije racunanjem operatora pomocu count-1
                var subCombinations = GenerateCombinationsTest(input, operators, count - 1);

                result.AddRange(subCombinations);
                // kombinovanje izraza kombinacija sa operatorima medjusobno 
                foreach (var subCombination in subCombinations)
                {
                    foreach (var op in operators)
                    {
                        foreach (var number in input)
                        {
                            if (subCombination.Input.Contains(number)) continue;

                            var newCombination = new Expression
                            {
                                TextExpression = $"({subCombination.TextExpression} {op} {number})",
                                Input = subCombination.Input.Concat(new List<int> { number }).ToList(),
                                Operators = subCombination.Operators.Concat(new List<string> { op }).ToList(),
                                Target = new int()
                            };

                            result.Add(newCombination);
                        }
                    }
                }

                return result;
            }
        }



    }
}
