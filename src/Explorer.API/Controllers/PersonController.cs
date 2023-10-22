using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Authorize(Policy = "nonAdministratorPolicy")]
[Route("api/people")]
public class PersonController : BaseApiController
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPut("update")]
    public ActionResult<PersonResponseDto> Update([FromBody] PersonResponseDto person)
    {
        var result = _personService.Update(person);
        return CreateResponse(result);
    }

    [HttpGet]
    public ActionResult<PersonResponseDto> GetPaged(int page, int pageSize)
    {
        var result = _personService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }

    [HttpGet("person/{userId:long}")]
    public ActionResult<PersonResponseDto> GetByUserId(long userId)
    {
        var result = _personService.GetByUserId(userId);
        return CreateResponse(result);
    }

}