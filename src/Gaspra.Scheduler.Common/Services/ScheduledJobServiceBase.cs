using Gaspra.Scheduler.Common.Jobs;
using Gaspra.Scheduler.Common.Settings;
using Hangfire;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gaspra.Scheduler.Common.Services
{
    public abstract class ScheduledJobServiceBase : IHostedService
    {
        private readonly IAsyncScheduledJob _asyncScheduledJob;
        private readonly JobSettings _jobSettings;

        public ScheduledJobServiceBase(
            IAsyncScheduledJob asyncScheduledJob,
            JobSettings jobSettings)
        {
            _asyncScheduledJob = asyncScheduledJob;
            _jobSettings = jobSettings;
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            RecurringJob.AddOrUpdate<IAsyncScheduledJob>(
                    _jobSettings.JobIdentifier,
                    job => _asyncScheduledJob.Execute(_jobSettings),
                    _jobSettings.JobCronExpression,
                    _jobSettings.JobTimeZoneInfo,
                    _jobSettings.QueueIdentifier);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_jobSettings.CheckQueue);

                await _asyncScheduledJob.Manage(_jobSettings);
            }

        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("bye");
            });
        }
    }
}
