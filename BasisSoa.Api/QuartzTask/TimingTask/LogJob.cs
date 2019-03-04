using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.QuartzTask.TimingTask
{
    public class LogJob : IJob
    {
      
         
        public LogJob()
        {
 
            Console.Out.WriteLineAsync("Greetings from HelloJob!");

        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");

        }
    }
}
