using Calcspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AnyOperations
{
    public class Multiple : IOperation
    {
        public string Name { get { return "mult"; } }

        public object Execute(object[] args)
        {
            return Convert.ToInt32(args[0]) * Convert.ToInt32(args[1]);
        }
    }

    public class Fact : IOperation
    {
        public string Name { get { return "fact"; } }

        public object Execute(object[] args)
        {
            int result = 1;
            for (int i = 1; i <= (int)args[0]; i++)
            {
                result *= i;
            }
            return result;
        }
    }

    public class PiOperation : IOperation
    {
        public string Name { get { return "pi"; } }
        public object Execute(object[] args)
        {
            return (float)Math.PI;
        }
    }
}
