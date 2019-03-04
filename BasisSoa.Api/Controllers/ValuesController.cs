using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Implements;
using BasisSoa.Service.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BasisSoa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //ISysLogService sysLogService;
        //public ValuesController()
        //{
        //    sysLogService = new SysLogService();
        //}

        // GET api/values/5
        [HttpGet]
        public ActionResult<string> Get(int id)
        {
            //Console.Out.WriteLineAsync("Greetings from Job!");
            //var client = new BackgroundJobClient();
            //client.Enqueue(() => Console.WriteLine("Easy!"));
            //client.Delay(() => Console.WriteLine("Reliable!"), TimeSpan.FromMinutes(1));
            //RecurringJob.AddOrUpdate("Time_Log", () => Console.WriteLine("12"), Cron.MinuteInterval(1));
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
