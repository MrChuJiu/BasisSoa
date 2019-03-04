using BasisSoa.Api.QuartzTask.TimingTask;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasisSoa.Api.QuartzTask
{
    public class QuartzService : BackgroundService
    {
        private readonly IScheduler _scheduler;

        private readonly IServiceProvider _serviceProvider;
        public QuartzService(IScheduler scheduler, IServiceProvider serviceProvider)
        {
            _scheduler = scheduler;
            _serviceProvider = serviceProvider;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {

            DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(DateTime.Now, 1);
            DateTimeOffset endTime = DateBuilder.NextGivenMinuteDate(DateTime.Now, 10);

            IJobDetail job = JobBuilder.Create<LogJob>()
                            .WithIdentity("job", "group")
                            .Build();

            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                                       .StartAt(startTime)
                                       .EndAt(endTime)
                                       .WithIdentity("job", "group")
                                       .WithCronSchedule("*/5 * * * * ?")
                                       .Build();

            await _scheduler.ScheduleJob(job, cronTrigger);

            await _scheduler.Start(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Shutdown(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
