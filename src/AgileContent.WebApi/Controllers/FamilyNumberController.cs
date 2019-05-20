using AgileContent.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AgileContent.WebApi.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Api para Test 1. DecReprSenior.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyNumberController : ControllerBase
    {
        private readonly IFamilyNumber _familyNumber;
        /// <summary>
        /// Controller Constructor 
        /// </summary>
        /// <param name="familyNumber">Application Service</param>
        public FamilyNumberController(IFamilyNumber familyNumber)
        {
            _familyNumber = familyNumber;
        }

        // GET api/FamilyNumber/5
        /// <summary>
        /// Get Largest Family Number
        /// </summary>
        /// <param name="number">Number for get largest Family Number</param>
        /// <returns></returns>
        [HttpGet("{largestFamilyNumber}")]
        public ActionResult<int> Get(long number)
        {
            return _familyNumber.GetLargestFamilyNumber(number);
        }
    }
}
