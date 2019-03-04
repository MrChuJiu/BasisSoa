using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Api.QuartzTask
{
    public class JobFactorys : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public JobFactorys(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                var job = _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
                return job;
            }
            catch (Exception e)
            {
                //NLogHelper.Error(e);
            }
            return null;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
