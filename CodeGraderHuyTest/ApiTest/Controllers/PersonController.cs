using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        PersonService _personService;
        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var people = await _personService.GetAll();
            return Ok(people);
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            await _personService.Add();
            return Ok();
        }
    }
}
