using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mozadatak
{
    public class Expression
    {
       
    public string TextExpression { get; set; }
    public List<int> Input { get; set; }
    public List<string> Operators { get; set; }
    public double Target { get; set; }
    public string Text { get; set; }

    public Expression(string text, double target)
    {
      Text = text;
      this.Target = target;
    }

    public Expression()
    {
            Random rand = new Random();
            Input = new List<int>();
            Operators = new List<string>();
            Target = new double();

            for (int i = 0; i < Input.Count; i++)
            {
                Input.Add(rand.Next(1, 101));
            }

            


        }


    }
}
