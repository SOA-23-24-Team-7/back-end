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

    //staviti proveru da li je to taj person
    [HttpPut("update/{personId:long}")]
    public ActionResult<PersonResponseDto> Update([FromBody] PersonUpdateDto person, long personId)
    {
        person.Id = personId;
        var result = _personService.UpdatePerson(person);
        return CreateResponse(result);
    }

    [HttpGet]
    public ActionResult<PersonResponseDto> GetPaged(int page, int pageSize)
    {
        var result = _personService.GetAll(page, pageSize);
        return CreateResponse(result);
    }

    [HttpGet("person/{userId:long}")]
    public ActionResult<PersonResponseDto> GetByUserId(long userId)
    {
        var result = _personService.GetByUserId(userId);
        return CreateResponse(result);
    }

}