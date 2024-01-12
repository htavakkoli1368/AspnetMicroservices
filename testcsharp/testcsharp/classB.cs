using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcsharp
{
    public class classB : classA, ireposeinterface
    {
        public string gerproductinterface()
        {
            Console.WriteLine("we are using interface methos in classb");
            return "execute interface method";
        }

        public string getproduct()
        {
            Console.WriteLine("i am using product in class b");
            return "i am using product in class b";
        }
    }
}
