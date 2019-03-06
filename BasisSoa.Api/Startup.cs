using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BasisSoa.Api.AutoMapper;
using BasisSoa.Api.Jwt;
using BasisSoa.Api.ApiWebSocket;
using BasisSoa.Core;
using BasisSoa.Service.Implements;
using BasisSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using BasisSoa.Common.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Hangfire;
using BasisSoa.Api.Hangfire;
using Quartz.Spi;
using BasisSoa.Api.QuartzTask;
using BasisSoa.Api.QuartzTask.TimingTask;
using Quartz;
using Quartz.Impl;
using StackExchange.Profiling.Storage;
using System.Reflection;
using log4net.Repository;
using log4net.Config;
using log4net;
using BasisSoa.Api.Log;
using BasisSoa.Api.Filter;

namespace BasisSoa.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //log4net
            repository = LogManager.CreateRepository("");//需要获取日志的仓库名，也就是你的当然项目名 
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//配置文件
        }

        public IConfiguration Configuration { get; }
        /// <summary>
        /// log4net 仓储库
        /// </summary>
        public static ILoggerRepository repository { get; set; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            #region 依赖注入
            services.AddScoped<NoticeHandler>();
            services.AddScoped<BasisSoa.Core.BaseDbInit>();
            services.AddScoped<BasisSoa.Core.BaseDbContext>();

            //系统
            services.AddTransient<ISysLogService, SysLogService>();
            services.AddTransient<ISysUserService, SysUserService>();
            services.AddTransient<ISysUserLogonService, SysUserLogonService>();
            services.AddTransient<ISysOrganizeService, SysOrganizeService>();
            services.AddTransient<ISysRoleService, SysRoleService>();
            services.AddTransient<ISysModuleService, SysModuleService>();
            services.AddTransient<ISysModuleActionService, SysModuleActionService>();

            services.AddTransient<ISysRoleAuthorizeService, SysRoleAuthorizeService>();
            services.AddTransient<ISysRoleAuthorizeActionService, SysRoleAuthorizeActionService>();

            services.AddTransient<ISysMessageService, SysMessageService>();

            #endregion

            #region 跨域设置
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
            services.AddMvc();
            #endregion

            #region MiniProfiler请求时间性能检测框架
            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";//注意这个路径要和下边 index.html 脚本配置中的一致，
                (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);

            });
            #endregion

            #region AutoMapper服务
            services.AddAutoMapper();
            //启动配置
            AutoMapperConfig.RegisterMappings();
            #endregion

            #region 全局错误日志记录Log4只能记录Action层 Server层暂时不行
            services.AddSingleton<ILoggerHelper, LogHelper>();
            #endregion

            #region 定时任务  Hangfire
            services.AddHangfire(r => r.UseSqlServerStorage(Configuration["ConnectionStrings:DefaultConnection"]));
            #endregion

            #region 定时任务  Quart
            services.AddSingleton<IJobFactory, JobFactorys>();
            services.AddTransient<LogJob>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。
            services.AddSingleton(p => {

                var sf = new StdSchedulerFactory();
                var scheduler = sf.GetScheduler().Result;
                scheduler.JobFactory = p.GetService<IJobFactory>();
                return scheduler;
            });//注册ISchedulerFactory的实例。
            services.AddHostedService<QuartzService>();
            #endregion

            #region Swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "BasisSoa API",
                    Contact = new Contact { Name = "基础框架", Email = "377749229@qq.com", Url = "http://wwww.baidu.com" },
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

            #region JWT Token Service
            //读取配置文件
            var audienceConfig = Configuration["JwtAuth:Audience"];
            var issuer = Configuration["JwtAuth:Issuer"];
            var symmetricKeyAsBase64 = Configuration["JwtAuth:SecurityKey"];
            var webExp = double.Parse(Configuration["JwtAuth:WebExp"]);
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = issuer,//发行人
                ValidateAudience = true,
                ValidAudience = audienceConfig,//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 如果要数据库动态绑定，这里先留个空，后边处理器里动态赋值
            var permission = new List<Permission>();
            // 角色与接口的权限要求参数
            var permissionRequirement = new PermissionRequirement(
                "/api/denied",// 拒绝授权的跳转地址（目前无用）
                permission,
                ClaimTypes.Role,//基于角色的授权
                issuer,//发行人
                audienceConfig,//听众
                signingCredentials,//签名凭据
                expiration: TimeSpan.FromSeconds(60 * webExp)//接口的过期时间
             );


            services.AddAuthorization(options =>
            {
                // 自定义权限要求
                options.AddPolicy("Permission",
                         policy => policy.Requirements.Add(permissionRequirement));
            })
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;

            });



            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);

            #endregion

            #region 缓存 读取配置是否使用哪种缓存模式
            services.AddMemoryCache();
            if (Convert.ToBoolean(Configuration["Cache:IsUseRedis"]))
            {
                services.AddSingleton<ICacheService, RedisCacheService>();
            }
            else
            {
                services.AddSingleton<ICacheService, MemoryCacheService>();
            }
            #endregion

            #region 缓存 RedisCache
            //将Redis分布式缓存服务添加到服务中
            services.AddDistributedRedisCache(options =>
            {
                //用于连接Redis的配置 
                options.Configuration = "localhost";// Configuration.GetConnectionString("RedisConnectionString");
                //Redis实例名RedisDistributedCache
                options.InstanceName = "RedisInstance";
            });
            #endregion

            #region 性能 压缩
            services.AddResponseCompression();
            #endregion

            services.AddMvc(o =>
              o.Filters.Add(typeof(GlobalExceptionsFilter)) //注入全局异常捕获
             ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHostedService<HangfireService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, IDistributedCache cache)
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

            
            #region 缓存
            //lifetime.ApplicationStarted.Register(() =>
            //{
            //    var currentTimeUTC = DateTime.UtcNow.ToString();
            //    byte[] encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(currentTimeUTC);
            //    var options = new DistributedCacheEntryOptions()
            //        .SetSlidingExpiration(TimeSpan.FromSeconds(20));
            //    cache.Set("cachedTimeUTC", encodedCurrentTimeUTC, options);
            //});
            #endregion

            #region Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasisSoa API V1");
                // Other
                c.DocumentTitle = "BasisSoa.Api 在线文档调试";

                // 将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：解决方案名.index.html
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("BasisSoa.Api.index.html");
            });
            #endregion

            #region WebSocket
            app.Map("/ApiWebSocket", NoticeHandler.Map);
            #endregion

            #region http请求认证
            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            #endregion

            #region 性能压缩
            app.UseResponseCompression();
            #endregion

            #region MiniProfiler请求时间性能检测框架
            app.UseMiniProfiler();
            app.UseStaticFiles();
            #endregion

            #region 定时任务 Hangfire
            var jobOption = new BackgroundJobServerOptions
            {
                WorkerCount = 5//并发数
            };
            app.UseHangfireServer(jobOption);
            app.UseHangfireDashboard();
            #endregion

            #region 配置静态资源
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(AppContext.BaseDirectory, "Uploads/HeadImage")),
                RequestPath = "/Uploads/HeadImage"
            });
            #endregion

            app.UseMvc();
        }
    }
}
