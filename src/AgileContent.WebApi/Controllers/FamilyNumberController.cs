using AgileContent.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace AgileContent.WebApi.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// 1. DecReprSenior.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyNumberController : ControllerBase
    {
        private readonly IFamilyNumberService _familyNumber;
        /// <summary>
        /// Controller Constructor 
        /// </summary>
        /// <param name="familyNumber">Application Service</param>
        public FamilyNumberController(IFamilyNumberService familyNumber)
        {
            _familyNumber = familyNumber;
        }

        // GET api/FamilyNumber/5
        /// <summary>
        /// Get Largest Family Number
        /// </summary>
        /// <param name="number">Number for get largest Family Number</param>
        /// <returns></returns>
        [HttpGet("{number}")]
        public ActionResult<int> Get(long number)
        {
            var result = _familyNumber.GetLargestFamilyNumber(number);
            return  Ok(result);
        }
    }
}
