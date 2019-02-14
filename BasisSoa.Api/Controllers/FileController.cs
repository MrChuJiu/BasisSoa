using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BasisSoa.Api.Jwt;
using BasisSoa.Common.ClientData;
using BasisSoa.Common.EnumHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasisSoa.Api.Controllers
{
    /// <summary>
    /// 文件上传处理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="Files"></param>
        /// <param name="FileType">文件类型  0头像</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Upload(IFormCollection Files, int FileType)
        {

            ApiResult<string> res = new ApiResult<string>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);
            try
            {
                //var form = Request.Form;//直接从表单里面获取文件名不需要参数
                string dd = Files["File"];
                var form = Files;//定义接收类型的参数
                Hashtable hash = new Hashtable();
                IFormFileCollection cols = Request.Form.Files;
                if (cols == null || cols.Count == 0)
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "没有上传文件";
                    return res;
                }
                foreach (IFormFile file in cols)
                {
                    //定义图片数组后缀格式
                    string[] LimitPictureType = { ".JPG", ".JPEG", ".GIF", ".PNG", ".BMP" };
                    //获取图片后缀是否存在数组中
                    string currentPictureExtension = Path.GetExtension(file.FileName).ToUpper();
                    if (LimitPictureType.Contains(currentPictureExtension))
                    {
                        string new_path = "";
                        switch (FileType) {
                            case 0:
                                new_path = Path.Combine("Uploads/HeadImage/", token.Id+DateTime.Now.ToString("yyyy-mm-ddhhddss")+ ".JPG");
                                break;
                        }

                        var path = Path.Combine(Directory.GetCurrentDirectory(), new_path);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                           //再把文件保存的文件夹中
                           file.CopyTo(stream);
                           hash.Add("file", "/" + new_path);
                           res.data = new_path;
                        }
                    }
                    else
                    {
                        res.code = (int)ApiEnum.Failure;
                        res.message = "请上传指定格式的图片";
                        return res;
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                res.code = (int)ApiEnum.Failure;
                res.message = "上传失败";
                return res;
            }

        }
    }
}