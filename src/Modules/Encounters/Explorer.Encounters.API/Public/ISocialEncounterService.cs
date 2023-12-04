using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public;

public interface ISocialEncounterService
{
    Result<SocialEncounterResponseDto> Create<SocialEncounterCreateDto>(SocialEncounterCreateDto encounter);
    Result<SocialEncounterResponseDto> Update<EncounterUpdateDto>(EncounterUpdateDto encounter);
    Result<PagedResult<SocialEncounterResponseDto>> GetPaged(int page, int pageSize);
    Result<SocialEncounterResponseDto> Get(long id);
    Result Delete(long id);
    Result<TouristProgressResponseDto> CompleteSocialEncounter(long userId, long encounterId);
}
