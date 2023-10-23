using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class PersonService : CrudService<PersonResponseDto, Person>, IPersonService
    {

        private readonly IPersonRepository _personRepository;
        public PersonService(ICrudRepository<Person> repository, IPersonRepository personRepository, IMapper mapper) : base(repository, mapper)
        {
            _personRepository = personRepository;
        }

        public Result<PersonResponseDto> GetByUserId(long id)
        {
            try
            {
                var result = _personRepository.GetByUserId(id);
                return MapToDto<PersonResponseDto>(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
