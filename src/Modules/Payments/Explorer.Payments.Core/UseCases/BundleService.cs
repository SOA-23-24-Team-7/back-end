using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using Explorer.Tours.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Payments.Core.Domain.Bundles;
using Explorer.BuildingBlocks.Core.Domain;
using System.Linq.Expressions;

namespace Explorer.Payments.Core.UseCases
{
    public class BundleService : IBundleService
    {
        private readonly IMapper _mapper;
        private IInternalTourService _tourService;
        private IBundleRepository _bundleRepository;

        public BundleService(IMapper mapper, IInternalTourService tourService, IBundleRepository bundleRepository)
        {
            _mapper = mapper;
            _tourService = tourService;
            _bundleRepository = bundleRepository;
        }

        public Result<BundleResponseDto> Create(BundleCreationDto bundleDto, long authorId)
        {
            try
            {
                var bundle = new Bundle(bundleDto.Name, bundleDto.Price, authorId, BundleStatus.Draft);

                List<BundleItem> bundleItems = new List<BundleItem>();
                foreach (int tourId in bundleDto.TourIds)
                {
                    var tour = _tourService.Get(tourId).Value;
                    if (tour.Status != Tours.API.Dtos.TourStatus.Published)
                        throw new KeyNotFoundException();
                    bundle.AddBundleItem(tourId);
                }

                var createdBundle = _bundleRepository.Create(bundle);

                var responseDto = _mapper.Map<BundleResponseDto>(createdBundle);

                return responseDto;
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<BundleResponseDto> Edit(long id, BundleEditDto bundleDto, long authorId)
        {
            try
            {
                Expression<Func<Bundle, bool>> filter = b => b.Id == id && b.AuthorId == authorId &&
                                                        (b.Status == BundleStatus.Draft || b.Status == BundleStatus.Archived);
                Bundle bundle = _bundleRepository.Get(filter, include: "BundleItems");

                bundle.Rename(bundleDto.Name);
                bundle.ChangePrice(bundleDto.Price);

                bundle.BundleItems.Clear();
                List<BundleItem> bundleItems = new List<BundleItem>();
                foreach (int tourId in bundleDto.TourIds)
                {
                    var tour = _tourService.Get(tourId).Value;
                    if (tour.Status != Tours.API.Dtos.TourStatus.Published)
                        throw new KeyNotFoundException();
                    bundle.AddBundleItem(tourId);
                }

                var editedBundle = _bundleRepository.Update(bundle);

                var responseDto = _mapper.Map<BundleResponseDto>(editedBundle);

                return responseDto;
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            };
        }

        public Result<int> Publish(long id)
        {
            throw new NotImplementedException();
        }

        public Result<int> Archive(long id)
        {
            throw new NotImplementedException();
        }
        
        public Result<int> Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
