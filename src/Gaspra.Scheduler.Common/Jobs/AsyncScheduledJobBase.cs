using Gaspra.Scheduler.Common.Settings;
using Hangfire;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gaspra.Scheduler.Common.Jobs
{
    public abstract class AsyncScheduledJobBase : IAsyncScheduledJob
    {
        public abstract Func<Task> Job { get; }

        public virtual async Task Manage(JobSettings jobSettings)
        {
            await Task.Run(() =>
            {
                var monitoringApi = JobStorage
                    .Current
                    .GetMonitoringApi();

                var queues = monitoringApi
                    .Queues();

                var queueCount = monitoringApi
                    .EnqueuedCount(jobSettings.QueueIdentifier);

                if (queueCount > 1)
                {
                    var enqueuedJobIds = monitoringApi
                        .EnqueuedJobs(jobSettings.QueueIdentifier, 0, int.MaxValue)
                        .Select(j => j.Key);

                    foreach (var jobId in enqueuedJobIds)
                    {
                        BackgroundJob
                            .Delete(jobId);
                    }
                }
            });
        }

        public virtual async Task Execute(JobSettings jobSettings)
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
