using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases
{
    public class CouponService : CrudService<CouponResponseDto, Coupon>, ICouponService
    {
        private readonly IMapper _mapper;
        private readonly ICouponRepository _couponRepository;
        private IInternalTourService _internalTourService;
        public CouponService(ICrudRepository<Coupon> repository, ICouponRepository couponRepository, IMapper mapper, IInternalTourService internalTourService) : base(repository, mapper)
        {
            _mapper = mapper;
            _couponRepository = couponRepository;
            _internalTourService = internalTourService;
        }
        public Result<PagedResult<CouponResponseDto>> GetPagedByAuthorId(int page, int pageSize, long id)
        {
            var pagedCoupons = _couponRepository.GetPagedByAuthorId(page, pageSize, id);
            var result = MapToDto<CouponResponseDto>(pagedCoupons);
            foreach (var coupon in result.Value.Results)
            {
                var tour = _internalTourService.Get(coupon.TourId).Value;
                coupon.TourName = tour.Name;
            }
            return result;
        }
    }
}
