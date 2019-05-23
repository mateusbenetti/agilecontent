using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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