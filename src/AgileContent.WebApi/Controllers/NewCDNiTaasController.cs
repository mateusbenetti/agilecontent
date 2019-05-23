using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileContent.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AgileContent.WebApi.Controllers
{
    public class NewCDNiTaasController : Controller
    {
        INewCDNiTaasService _newCDNiTaasService;
        public NewCDNiTaasController(INewCDNiTaasService newCDNiTaasService)
        {
            _newCDNiTaasService = newCDNiTaasService;
        }
        [HttpGet("{version}/{url}")]
        public ActionResult<string> Get(string version, string url)
        {
            var result = _newCDNiTaasService.ConvertCdnFileToNowFile(url, version, DateTime.Now);
            return Ok(result);
        }
    }
}