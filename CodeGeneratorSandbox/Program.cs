using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace CodeGeneratorSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloWorldGenerated.HelloWorld.SayHello();
        }
    }
    
}
