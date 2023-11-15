using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using FluentResults;
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

    [HttpPut("update/{personId:long}")]
    public ActionResult<PersonResponseDto> Update([FromBody] PersonUpdateDto person, long personId)
    {
        var loggedInUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
        var response = _personService.Get(personId);
        if (response.IsFailed) return CreateResponse(response);

        var userId = response.Value.UserId;
        if (loggedInUserId == userId)
        {
            person.Id = personId;
            var result = _personService.UpdatePerson(person);
            return CreateResponse(result);
        }
        return Forbid();
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