using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Implements;
using BasisSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasisSoa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ISysLogService sysLogService;
        public ValuesController()
        {
            sysLogService = new SysLogService();
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        {

            SysLog sysLog = new SysLog();

            await  sysLogService.Add(sysLog);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
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
