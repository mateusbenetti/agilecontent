using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileContent.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AgileContent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyNumberController : ControllerBase
    {
        private readonly IFamilyNumber _familyNumber;
        public FamilyNumberController(IFamilyNumber familyNumber)
        {
            _familyNumber = familyNumber;
        }

        // GET api/FamilyNumber/5
        [HttpGet("{largestFamilyNumber}")]
        public ActionResult<int> Get(long number)
        {
            return _familyNumber.GetLargestFamilyNumber(number);
        }
    }
}
