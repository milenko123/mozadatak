using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mozadatak
{
    public interface INumberValidator
    {
        object ServiceInformation { get; }

        bool IsValid(string value);

        bool IsValid(string value, out bool isValid);
        
    }
}
