using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Implements
{
    public class SysMessageService : BaseServer<SysMessage>, ISysMessageService
    {
        public async Task<List<SysMessage>> QuerySysMessageAllAsync()
        {
            List<SysMessage> res = new List<SysMessage>();

            res = await Db.Queryable<SysMessage>()
                .Mapper(it => it.sysUser, it => it.CreatorUserId)
                .OrderBy(s => new {  s.CreateTime, s.IsRead, s.MsgStatus  },SqlSugar.OrderByType.Desc).ToListAsync();

            return res;
        }
    }
}
