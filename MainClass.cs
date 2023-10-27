using mozadatak;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;


namespace ConsoleApp1
{
    class MainClass
    {
        static void Main(string[] args)

        {
            var calculations = new Calculations(new NumberValidator(), new ExpressionEvaluator());

            Console.Write("Unesite niz brojeva, odvojene zarezima: ");
            string inputString = Console.ReadLine();
            int[] input = Array.ConvertAll(inputString.Split(','), int.Parse);

            Console.Write("Unesite ciljani broj: ");
            int target = int.Parse(Console.ReadLine());

            Console.WriteLine($"~~~~~~~~~~~~~~~Prikazati kombinacije koje daju ciljani broj {target}~~~~~~~~~~~~~~~~~");
            calculations.PrintExpressions(input, target);

            Console.WriteLine("~~~~~~~~~~~~~~Traziti kombinacije koje sadrze odredjeni broj niza i operator~~~~~~~~~~~~~");
            Console.Write("Unesite odabrani broj: ");
            int selectedNumber = int.Parse(Console.ReadLine());
            Console.Write("Unesite operator: "); 
            string selectedOperator = Console.ReadLine();

            var expression = GenerateAllExpression.GenerateAllExpressions(input, target);
            calculations.SelectingSolution(input, target, selectedNumber, selectedOperator);

            Console.WriteLine("~~~~~~~~~~~~~~Prikazati najprostiju kombinaciju~~~~~~~~~~~~~");
            calculations.SimplestExpression(input, target);
            Console.WriteLine("~~~~~~~~~~~~~~Prikazati samo jednu kombinaciju~~~~~~~~~~~~~");
            calculations.OnlyOneExpress(input, target);
        


           
        }
        
    }
}


