using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Stakeholders.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class WalletService : CrudService<WalletResponseDto, Wallet>, IWalletService
    {
        private readonly IInternalUserService _internalUserService;
        private readonly ICrudRepository<Wallet> _walletRepository;
        public WalletService(ICrudRepository<Wallet> repository, IMapper mapper, IInternalUserService internalUserService) : base(repository, mapper)
        {
            _internalUserService = internalUserService;
            _walletRepository = repository;
        }

        public Result<WalletResponseDto> Create(WalletCreateDto walletCreateDto)
        {
            var userResponse = _internalUserService.Get(walletCreateDto.TouristId);
            if(!userResponse.IsSuccess)
            {
                throw new ArgumentException("User with given Id does not exists");
            }
            var wallet = _walletRepository.GetAll().FirstOrDefault(w => w.TouristId == userResponse.Value.Id);
            if (wallet != null)
            {
                throw new ArgumentException("User already have wallet");
            }
            try
            {
                var result = _walletRepository.Create(MapToDomain(walletCreateDto));
                return MapToDto<WalletResponseDto>(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<WalletResponseDto> Update(WalletUpdateDto walletUpdateDto)
        {
            try
            {
                var wallet = _walletRepository.Get(walletUpdateDto.Id);
                wallet.SetAdventureCoin(walletUpdateDto.AdventureCoin);
                var result = _walletRepository.Update(wallet);
                return MapToDto<WalletResponseDto>(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result DeleteForTourist(long touristId)
        {
            try
            {
                var wallet = _walletRepository.GetAll().FirstOrDefault(w => w.TouristId == touristId);
                if (wallet != null)
                {
                    _walletRepository.Delete(wallet.Id);
                    return Result.Ok();
                }
                else
                {
                    return Result.Fail(FailureCode.Conflict).WithError("Toursit does not have wallet");
                }
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<WalletResponseDto> GetForTourist(long touristId)
        {
            var wallet = _walletRepository.GetAll().FirstOrDefault(w => w.TouristId == touristId);
            if(wallet != null)
            {
                return MapToDto<WalletResponseDto>(wallet);
            }
            else
            {
                return Result.Fail(FailureCode.NotFound).WithError("Tourist does not have wallet");
            }
        }
    }
}
