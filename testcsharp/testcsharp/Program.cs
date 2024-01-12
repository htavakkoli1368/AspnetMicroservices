using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace testcsharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var classAtest = new classA();
            var classBtest = new classB();
            classAtest.gerproductinterface();       
           
             
            Console.ReadLine();
        }
    }
}
