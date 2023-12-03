using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface IWalletService
    {
        public Result<WalletResponseDto> Create(WalletCreateDto walletCreateDto);
        public Result<WalletResponseDto> Update(WalletUpdateDto walletUpdateDto);
        public Result Delete(long id);
        public Result<WalletResponseDto> Get(long id);
        public Result<WalletResponseDto> GetForTourist(long touristId);
        public Result DeleteForTourist(long touristId);
    }
}
