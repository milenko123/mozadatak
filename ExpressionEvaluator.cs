using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mozadatak
{
    public class ExpressionEvaluator : GenerateAllExpression
    {
        public ExpressionEvaluator()
        {

        }

        public static double EvaluateExpression(string expression)
        {
            //koriscenje DataTable.Compute metode za pokretanje ekspresije 
            var dataTable = new DataTable();
            var result = dataTable.Compute(expression, "");
            return Convert.ToDouble(result);
        }

        //public static Dictionary<double, List<string>> EvaluateExpressions(int[] input, int target)
        //{
        //    var evaluatedExpressions = new Dictionary<double, List<string>>();

        //    var combinations = GenerateAllExpressions(input, target);
        //    foreach (var combination in combinations)
        //    {
        //        var expression = combination.TextExpression;
        //        var result = EvaluateExpression(expression);
                
        //        if (evaluatedExpressions.ContainsKey(result))
        //        {
        //            evaluatedExpressions[result].Add(expression);
        //        }
        //        else
        //        {
        //            evaluatedExpressions[result] = new List<string>() { expression };
        //        }
        //    }

        //    return evaluatedExpressions;
        //}

        public virtual Dictionary<double, List<Expression>> EvaluateExpressionsTest(int[] input, int target)
        {
            var evaluatedExpressions = new Dictionary<double, List<Expression>>();

            var combinations = GenerateAllExpressions(input, target);
            foreach (var combination in combinations)
            {
                var expression = combination.TextExpression;
                var result = EvaluateExpression(expression);
                combination.Target= result;

                if (evaluatedExpressions.ContainsKey(result))
                {
                    evaluatedExpressions[result].Add(combination);
                }
                else
                {
                    evaluatedExpressions[result] = new List<Expression>() { combination };
                }
            }

            return evaluatedExpressions;
        }
    }
}
