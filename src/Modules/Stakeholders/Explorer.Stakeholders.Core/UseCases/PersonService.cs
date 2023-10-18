using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class PersonService : CrudService<PersonDto, Person>, IPersonService
    {
        public PersonService(ICrudRepository<Person> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public Result<PersonDto> GetByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
