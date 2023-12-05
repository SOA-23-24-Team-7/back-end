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

        public Result<int> Create(BundleCreationDto bundleDto)
        {
            try
            {
                Bundle bundle = new Bundle(bundleDto.Name, bundleDto.Price, BundleStatus.Draft);

                List<BundleItem> bundleItems = new List<BundleItem>();
                foreach (int tourId in bundleDto.TourIds)
                {
                    var tour = _tourService.Get(tourId).Value;
                    if (tour.Status != Tours.API.Dtos.TourStatus.Published)
                        throw new KeyNotFoundException();
                    bundle.AddBundleItem(tourId);
                }

                _bundleRepository.Create(bundle);
                return 1;
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

        public Result<int> Edit(BundleCreationDto bundleDto)
        {
            throw new NotImplementedException();
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
