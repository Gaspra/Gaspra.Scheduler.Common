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

        public ScheduledJobServiceBase(IAsyncScheduledJob asyncScheduledJob)
        {
            _asyncScheduledJob = asyncScheduledJob;
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            RecurringJob.AddOrUpdate<IAsyncScheduledJob>(
                    _asyncScheduledJob.Settings.JobIdentifier,
                    job => _asyncScheduledJob.Execute(),
                    _asyncScheduledJob.Settings.JobCronExpression,
                    _asyncScheduledJob.Settings.JobTimeZoneInfo,
                    _asyncScheduledJob.Settings.QueueIdentifier);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_asyncScheduledJob.Settings.CheckQueue);

                await _asyncScheduledJob.Manage();
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
