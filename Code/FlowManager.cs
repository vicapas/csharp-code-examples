using Models;
using System;
using System.Threading.Tasks;

namespace CodeExamples
{
    public class FlowManager
    {
        private Action action;
        private DateTime last;
        private int checkLapse;
        private static AsyncLock lockDuration;

        public FlowManager(Action action, int checkLapse)
        {
            this.action = action;
            last = DateTime.UtcNow.AddSeconds(-checkLapse / 1000);
            this.checkLapse = checkLapse;
            lockDuration = new AsyncLock();
        }

        private async Task<double> GetDuration()
        {
            using (await lockDuration.LockAsync())
            {
                var duration = (DateTime.UtcNow - last).TotalMilliseconds;
                last = DateTime.UtcNow;
                return duration;
            }
        }

        public async void DoAction()
        {
            var duration = await GetDuration();

            if (duration > checkLapse)
            {
                while ((DateTime.UtcNow - last).TotalMilliseconds <= checkLapse)
                {
                    await Task.Delay(checkLapse);
                }

                action();
                last.AddDays(-1);
            }
        }
    }
}
