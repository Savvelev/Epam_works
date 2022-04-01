using System;
using System.Diagnostics;

namespace Performance
{
    internal class Program
    {             
        static void Main(string[] args)
        {           
            var deltaForClasses = MeasureArrayExecutionpParameters.GetPMS64DeltaForArrayInit(out C[] classes);
            var deltaFromStruct = MeasureArrayExecutionpParameters.GetPMS64DeltaForArrayInit(out S[] structs);
            
            var sortClassesTime = MeasureArrayExecutionpParameters.CalculateTimeExecution(() => Array.Sort(classes));
            var sortStructsTime = MeasureArrayExecutionpParameters.CalculateTimeExecution(() => Array.Sort(structs));
         
            Console.WriteLine($"Calculating PMS64 for classes {deltaForClasses}");
            Console.WriteLine($"Calculating PMS64 for struct {deltaFromStruct}\n");
            Console.WriteLine($"Array.Sort() Execution time Classes {sortClassesTime}");
            Console.WriteLine($"Array.Sort() Execution time Structs {sortStructsTime}");
        }                       
    }
}
