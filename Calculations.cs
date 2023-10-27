using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mozadatak
{
    public class Calculations : ExpressionEvaluator
    {
       
        private readonly INumberValidator _validator;
        private readonly ExpressionEvaluator _expressionEvaluator;

        public Calculations(INumberValidator validator, ExpressionEvaluator expressionEvaluator)
        {
            _validator = validator ?? throw new System.ArgumentNullException(nameof(validator));
            _expressionEvaluator = expressionEvaluator;

        }

        public IEnumerable<Expression> ApproximateSolution(int[] input, int target) {

            var expressions = _expressionEvaluator.EvaluateExpressionsTest(input, target);

            Console.WriteLine("Nema izraza koji odgovaraju zadatim kriterijumima. Priblizni izraz je:");
            var approximateExpressions = expressions.OrderBy(e => Math.Abs(target - e.Key)).First();
            var approximateResult = approximateExpressions.Key;
            foreach (var expression in approximateExpressions.Value.Take(2))
            {
                Console.WriteLine(expression.TextExpression + " = " + approximateResult);
            }
            return approximateExpressions.Value;
        }

        public IEnumerable<Expression> GetExpressionsByTarget(int[] input, int target)
        {
            //za broj iz niza koji je jednak ciljanom broju, sprecava duplikate
            var expressions = _expressionEvaluator.EvaluateExpressionsTest(input, target);
            var exactExpressions = expressions[target].DistinctBy(x => x.TextExpression).Take(1);
            return exactExpressions;
        }

        public void PrintExpressions(IEnumerable<Expression> expressions, int target)
        {
            foreach (var expression in expressions)
            {
                Console.WriteLine(expression.TextExpression + " = " + target);
            }
        }

        public void OnlyOneExpress(int[] input, int target)
        {
            Console.WriteLine("Prikazati samo jedan izraz iz liste rezultata kombinacija:");

            var expressions = EvaluateExpressionsTest(input, target);

            if (expressions.ContainsKey(target))
            {
                var exactExpressions = GetExpressionsByTarget(input, target);
                PrintExpressions(exactExpressions, target);
            }
            else
            {
                ApproximateSolution(input, target);
            }
        }

        public IEnumerable<Expression> GetSelectingSolution(int[] input, int target, int selectedNumber, string selectedOperator)
        {
            var expressions = _expressionEvaluator.EvaluateExpressionsTest(input, target);
            var expressionsWithCorrectResult = expressions[target];
            Console.WriteLine($"Resenje za ciljani broj {target} sa odabranim operatorom {selectedOperator} i odabranim brojem {selectedNumber}:");
            var filteredExpressions = expressionsWithCorrectResult.Where(e => e.Input.Contains(selectedNumber) && e.Operators.Contains(selectedOperator)).DistinctBy(x => x.TextExpression);
          

            return filteredExpressions;
        }
        public void PrintSelectingExpressions(IEnumerable<Expression> filteredExpressions, int target)
        {
            int count = 0;
            foreach (var expression in filteredExpressions.Take(6))
            {
                Console.WriteLine(expression.TextExpression);
                count++;
                if (count == 6)
                {
                    break;
                }
            }
        }
        public void SelectingSolution(int[] input, int target, int selectedNumber, string selectedOperator)
        {
            Console.WriteLine($"Traže se izrazi koji daju ciljani broj {target} s odabranim brojem {selectedNumber} i odabranim operatorom {selectedOperator} :");


            var expressions = EvaluateExpressionsTest(input, target);

            if (expressions.ContainsKey(target))
            {
                var filteredExpression = GetSelectingSolution(input, target, selectedNumber, selectedOperator);
                PrintSelectingExpressions(filteredExpression, target);
            }
            else 
            {
                Console.WriteLine($"Nije pronadjeno resenje za ciljani broj  {target} sa odabranim operatorom {selectedOperator} i odabranim brojem {selectedNumber}. Prilizno resenje je:");
                ApproximateSolution(input, target);
            }
            
        }


        public IEnumerable<Expression> GetSimplestSolution(int[] input, int target)
        {
            var expressions = _expressionEvaluator.EvaluateExpressionsTest(input, target);
            var simplestExpressions = new List<Expression>();

            var minOperations = int.MaxValue;
            foreach (var kvp in expressions)
            {
                foreach (var expression in kvp.Value)
                {
                    var operationCount = expression.TextExpression.Split(new char[] { '+', '-', '*', '/' }).Length - 1;
                    if (kvp.Key == target && operationCount <= minOperations)
                    {
                        if (operationCount < minOperations)
                        {
                            simplestExpressions.Clear();
                            minOperations = operationCount;
                        }
                        simplestExpressions.Add(new Expression
                        {
                            TextExpression = expression.TextExpression,
                            Text = expression.Text,
                            Target = expression.Target
                        });
                    }
                }
            }
            return simplestExpressions;
        }

        public void PrintSimplestSolution(IEnumerable<Expression> simplestExpressions, int[] input, int target)
        {
            if (simplestExpressions.Any())
            {

                foreach (var expr in simplestExpressions)
                {
                    Console.WriteLine(expr.Text + " = " + target);
                }
            }
            else
            {
                //trazenje pribliznog resenja ukoliko ne postoji najjednostavnija kombinacija koja ce dati ciljani broj
                ApproximateSolution(input, target);
            }
        }
        public void SimplestExpression(int[] input, int target)
        {
            Console.WriteLine("Prikazati najjednostavniji izraz:");

            var expressions = _expressionEvaluator.EvaluateExpressionsTest(input, target);

            if (expressions.ContainsKey(target))
            {
                var exactExpressions = GetExpressionsByTarget(input, target);
                PrintExpressions(exactExpressions, target);
            }
            else
            {
                var simplestExpressions = GetSimplestSolution(input, target);
                PrintSimplestSolution(simplestExpressions, input , target);
                
            }
           

        }

        public void PrintExpressions(int[] input, int target)
        {
            //stampanje svih izraza
            var expressions = EvaluateExpressionsTest(input, target);

            if (expressions.ContainsKey(target))
            {
                var exactExpressions = expressions[target].DistinctBy(x => x.TextExpression).Take(450);
                foreach (var expression in exactExpressions)
                {
                    Console.WriteLine(expression.TextExpression + " = " + target);
                }
            }
            else
            {
                //trazenje priblizne solucije ukoliko nije pronadjeno tacno resenje
                ApproximateSolution(input, target); 

            }
        }

       
    }
}
