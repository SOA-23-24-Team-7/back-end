using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterService
    {
        Result<EncounterResponseDto> Create<EncounterCreateDto>(EncounterCreateDto encounter);
    }
}