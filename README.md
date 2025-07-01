
.net Core后台管理系统  


特别感谢老张\ DX \ fyt \凯旋等给帮助.....

感谢各位的支持BasisSoa只是一个基础的权限角色管理项目

很多地方可能设计的不够好我的想法也比较简单希望各位朋友能一起加入维护的行列(特别@老张、DX 我想拖你俩下水)

项目结构

Api 可在项目中配置使用权限，如后台管理，APP,微信等

Common=公共类，提供项目一些常用工具方法

Core=数据库实体对象，以及连接对象

Extensions=扩展方法

Service=业务类，接口和实现



系统环境

windows 10、SQL server 2012、Visual Studio 2017、Windows Server 2012 

后端技术：

  * .Net Core 2.0 API（因为想单纯搭建前后端分离，因此就选用的API，如果想了解.Net Core MVC，也可以交流）
  
  * Swagger 前后端文档说明，基于RESTful风格编写接口

  * Repository + Service 仓储模式编程

  * Async和Await 异步编程

  * Cors 简单的跨域解决方案

  * AOP基于切面编程技术

  * Core自带DI依赖注入

  * JWT权限验证



数据库技术

  * SqlSugar 轻量级ORM框架，CodeFirst

  * AutoMapper 自动对象映射


分布式缓存技术

  * Redis 轻量级分布式缓存


前端技术

  * Angular(想学angular基础知识请看我Anglar前端博客)

  * NgZorro 阿里angular组建库




