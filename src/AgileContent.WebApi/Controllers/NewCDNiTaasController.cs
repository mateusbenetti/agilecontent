using System;
using AgileContent.Application.Interface;
using AgileContent.WebApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace AgileContent.WebApi.Controllers
{
    /// <summary>
    /// 2 - New CDN iTaas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NewCDNiTaasController : Controller
    {
        INewCDNiTaasService _newCDNiTaasService;
        public NewCDNiTaasController(INewCDNiTaasService newCDNiTaasService)
        {
            _newCDNiTaasService = newCDNiTaasService;
        }
        /// <summary>
        /// Convert CDN Log File  To Now Log File
        /// </summary>
        /// <param name="model">Input Url and version for conversion</param>
        /// <returns>Now Log Content</returns>
        [HttpPost]
        public ActionResult<string> Post([FromBody] ConvertCDNLogToNowModel model)
        {
            var result = _newCDNiTaasService.ConvertCdnFileToNowFile(model.Url, model.Version, DateTime.Now);
            if (_newCDNiTaasService.HasErrors)
            {
                foreach (var error in _newCDNiTaasService.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}