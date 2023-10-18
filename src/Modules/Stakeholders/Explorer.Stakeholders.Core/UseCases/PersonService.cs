using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;
using AutoMapper;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class PersonService : CrudService<PersonDto, Person>, IPersonService
    {
        public PersonService(ICrudRepository<Person> repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}
