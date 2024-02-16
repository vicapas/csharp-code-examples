using CodeExamples.Tests;
using System;
using System.Collections.Generic;

namespace CodeExamples
{
    internal class Program
    {
        static List<(string, ITest)> tests = new List<(string, ITest)>()
        {
            ("FlowManager", new TestFlowManager())
        };

        static void Main(string[] args)
        {
            foreach (var test in tests)
            {
                Console.WriteLine($"Testing {test.Item1}");
                Console.WriteLine("-----------------------------");
                test.Item2.Run();
                Console.WriteLine("");
            }

            Console.ReadLine();
        }
    }
}