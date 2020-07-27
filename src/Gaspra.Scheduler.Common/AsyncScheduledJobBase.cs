using Gaspra.Scheduler.Common.Settings;
using Hangfire;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gaspra.Scheduler.Common
{
    public abstract class AsyncScheduledJobBase : IAsyncScheduledJob
    {
        public abstract JobSettings Settings { get; }
        public abstract Func<Task> Job { get; }

        public virtual async Task Manage()
        {
            await Task.Run(() =>
            {
                var monitoringApi = JobStorage
                    .Current
                    .GetMonitoringApi();

                var queues = monitoringApi
                    .Queues();

                var queueCount = monitoringApi
                    .EnqueuedCount(Settings.QueueIdentifier);

                if (queueCount > 1)
                {
                    var enqueuedJobIds = monitoringApi
                        .EnqueuedJobs(Settings.QueueIdentifier, 0, int.MaxValue)
                        .Select(j => j.Key);

                    foreach (var jobId in enqueuedJobIds)
                    {
                        BackgroundJob
                            .Delete(jobId);
                    }
                }
            });
        }

        public virtual async Task Execute()
        {
            try
            {
                await Job();
            }
            catch (Exception ex)
            {
                /*handle*/
            }
        }
    }
}
