using BasisSoa.Service.Implements;
using BasisSoa.Service.Interfaces;
using Hangfire;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasisSoa.Api.Hangfire
{
    public class HangfireService : BackgroundService
    {


        ISysLogService sysLogService;
        public HangfireService(ISysLogService _sysLogService)
        {
            sysLogService = _sysLogService;
        }


        public override async Task StartAsync(CancellationToken cancellationToken)
        {

            RecurringJob.AddOrUpdate("Time_LogTime", () => Console.WriteLine("12"), Cron.MinuteInterval(1));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
