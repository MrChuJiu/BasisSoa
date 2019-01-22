using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasisSoa.Api.AutoMapper;
using BasisSoa.Core;
using BasisSoa.Service.Implements;
using BasisSoa.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace BasisSoa.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }//

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region 依赖注入

            //系统
            services.AddTransient<ISysLogService, SysLogService>();
            services.AddTransient<ISysUserLogonService, SysUserLogonService>();
            services.AddTransient<ISysUserService, SysUserService>();

            #endregion


            //跨域设置
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
            services.AddMvc();

            //添加服务
            services.AddAutoMapper();
            //启动配置
            AutoMapperConfig.RegisterMappings();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            #region Swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "BasisSoa API",
                    Contact = new Contact { Name = "永思科技", Email = "377749229@qq.com", Url = "http://wwww.baidu.com" },
                    Description = @" <table style='height: 304px; width: 706px;' border='0'>
                                            <tbody>
                                            <tr>
                                            <td><strong>HTTP方法</strong></td>
                                            <td><strong>URI</strong></td>
                                            <td><strong>描述</strong></td>
                                            <td><strong>幂等</strong></td>
                                            <td><strong>安全</strong></td>
                                            </tr>
                                            <tr>
                                            <td>&nbsp;GET</td>
                                            <td>&nbsp;/api/members</td>
                                            <td>&nbsp;获取成员列表</td>
                                            <td>&nbsp;是</td>
                                            <td>&nbsp;是</td>
                                            </tr>
                                            <tr>
                                            <td>&nbsp;GET</td>
                                            <td>&nbsp;/api/members/{id}&nbsp;</td>
                                            <td>&nbsp;获取指定成员</td>
                                            <td>&nbsp;是</td>
                                            <td>&nbsp;是</td>
                                            </tr>
                                            <tr>
                                            <td>&nbsp;POST</td>
                                            <td>&nbsp;/api/members</td>
                                            <td>&nbsp;创建一个成员</td>
                                            <td>&nbsp;否</td>
                                            <td>&nbsp;否</td>
                                            </tr>
                                            <tr>
                                            <td>&nbsp;PUT</td>
                                            <td>&nbsp;/api/members/{id}&nbsp;</td>
                                            <td>&nbsp;更新成员所有信息</td>
                                            <td>&nbsp;是</td>
                                            <td>&nbsp;否</td>
                                            </tr>
                                            <tr>
                                            <td>&nbsp;PATCH</td>
                                            <td>&nbsp;/api/members/{id}&nbsp;</td>
                                            <td>&nbsp;更新成员部分信息</td>
                                            <td>&nbsp;是</td>
                                            <td>&nbsp;否</td>
                                            </tr>
                                            <tr>
                                            <td>&nbsp;DELETE</td>
                                            <td>&nbsp;/api/members/{id}&nbsp;</td>
                                            <td>&nbsp;删除指定成员</td>
                                            <td>&nbsp;是</td>
                                            <td>&nbsp;否</td>
                                            </tr>
                                            </tbody>
                                            </table>"
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "BasisSoa.Api.xml");
                var entityXmlPath = Path.Combine(basePath, "BasisSoa.Core.xml");
                options.IncludeXmlComments(xmlPath, true);
                options.IncludeXmlComments(entityXmlPath);
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();

                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                //添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                options.AddSecurityRequirement(security);
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    //jwt默认的参数名称
                    Name = "Authorization",
                    //jwt默认存放Authorization信息的位置(请求头中)
                    In = "header",
                    Type = "apiKey",

                });

            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasisSoa API V1");
                // Other
                c.DocumentTitle = "BasisSoa.Api 在线文档调试";
            });
            //认证
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
