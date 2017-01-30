using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calcspace
{
    public class Calc
    {
        public Calc(IEnumerable<IOperation> opers) //позволяет создавать множество видов списков 
        {
            operations = opers; //задавание операции

        }

        public Calc(IOperation[] opers)
        {
            operations = opers; //задавание операции
        }

        
        private IEnumerable<IOperation> operations { get; set; } //массив всех операций, которые может делать калькулятор

        public object Execute(string name, object[] args) //имя и параметры операции
        {
            name = name.ToLower();
            var opers = operations.Where(o => o.Name == name); //ищем операцию
            if (!opers.Any())
                return $"Operation \"{name}\" not found";

            var opersWithCount = opers.OfType<IOperationCount>();

            var oper = opersWithCount.FirstOrDefault(o => o.Count == args.Count()) ?? opers.FirstOrDefault();


            if (oper == null)
            {
                return $"Operation \"{name}\" not found";
            }

            return oper.Execute(args); //нашли - мы ее вызываем
        }

        public IEnumerable<string> GetOperationNames()
        {
            return operations.Select(o => o.Name); //у каждой операции берем только имя
        }
    }

    public interface IOperation //интерфейс
    {
        string Name { get; } //имя операции
        object Execute(object[] args); //результат и входные данные
    }

    public interface IOperationCount : IOperation
    {
        //Количество аргументов в операции
        int Count { get; }
    }

   
    public class DivOperation : IOperation
    {
        public int Count { get { return 1; } }
        public string Name { get { return "sub"; } }

        public object Execute(object[] args)
        {
            return Convert.ToInt32(args[0]) - Convert.ToInt32(args[1]);
        }
    }
}
