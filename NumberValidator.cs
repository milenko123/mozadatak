using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mozadatak
{
    public class NumberValidator : INumberValidator
    {
        public object ServiceInformation => throw new NotImplementedException();

        public bool IsValid(string value)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(string value, out bool isValid)
        {
            throw new NotImplementedException();
        }

    }
}
