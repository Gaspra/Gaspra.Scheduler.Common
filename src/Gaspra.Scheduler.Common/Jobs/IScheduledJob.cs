using System.Collections.Generic;

namespace Gaspra.Scheduler.Common.Jobs
{
    public interface IScheduledJob
    {
        public IDictionary<string, object> Context { get; set; }
        public void Execute();
    }
}
