using System;
using System.Threading.Tasks;

namespace CodeExamples.Tests
{
    public class TestFlowManager : ITest
    {
        FlowManager flowManager;
        int counterTries = 0;
        int counterAction = 0;

        public void Run()
        {
            flowManager = new FlowManager(DoingAction, 2000);

            Task.Run(async () =>
            {
                for (int i = 0; i < 5; i++)
                {
                    TryinAction(); // 1, 2, 3, 4, 5
                    flowManager.DoAction(); // 1
                    await Task.Delay(500);
                }

                Console.WriteLine("Long");
                await Task.Delay(5000);

                for (int i = 0; i < 2; i++)
                {
                    TryinAction(); // 6, 7
                    flowManager.DoAction(); // 2
                    await Task.Delay(500);
                }

                Console.WriteLine("Long");
                await Task.Delay(5000);

                TryinAction(); // 8
                flowManager.DoAction(); // 3
                await Task.Delay(500);
            });

            // Trying Action 8
            // Doing Action 3
        }

        private void TryinAction()
        {
            counterTries++;
            Console.WriteLine($"Trying Action_{counterTries}");
        }

        private void DoingAction()
        {
            counterAction++;
            Console.WriteLine($"Doing Action_{counterAction}");
        }
    }
}
