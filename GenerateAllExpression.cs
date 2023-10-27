using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace mozadatak
{
    public class GenerateAllExpression : CombinationsGenerator
    {
        public static List<Expression> GenerateAllExpressions(int[] input, int target)
        {
            //generisanje kombinacija gde se za svaki broj iz niza vrsi racunanje u skladu sa tim da se dobije ciljani broj
           
            var operators = new List<string> { "+", "-", "*", "/"};

            var combinations = GenerateCombinationsTest(input, operators, input.Length - 1);

            return combinations;
        }
    }
}
