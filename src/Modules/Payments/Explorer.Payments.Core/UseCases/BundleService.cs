using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using FluentResults;
using Explorer.Payments.Core.Domain.Bundles;
using System.Linq.Expressions;
using System.Reflection.Metadata;

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
                var bundle = new Bundle(bundleDto.Name, bundleDto.Price, authorId, Domain.Bundles.BundleStatus.Draft);

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
                                                        (b.Status == Domain.Bundles.BundleStatus.Draft || b.Status == Domain.Bundles.BundleStatus.Archived);
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

        public Result<BundleResponseDto> Publish(long id, long authorId)
        {
            try
            {
                Expression<Func<Bundle, bool>> filter = b => b.Id == id && b.AuthorId == authorId &&
                                                        (b.Status == Domain.Bundles.BundleStatus.Draft || b.Status == Domain.Bundles.BundleStatus.Archived);
                Bundle bundle = _bundleRepository.Get(filter, include: "BundleItems");

                bundle.Publish();

                var publishedBundle = _bundleRepository.Update(bundle);

                var responseDto = _mapper.Map<BundleResponseDto>(publishedBundle);

                return responseDto;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            };
        }

        public Result<BundleResponseDto> Archive(long id, long authorId)
        {
            try
            {
                Expression<Func<Bundle, bool>> filter = b => b.Id == id && b.AuthorId == authorId &&
                                                        b.Status == Domain.Bundles.BundleStatus.Published;
                Bundle bundle = _bundleRepository.Get(filter, include: "BundleItems");

                bundle.Archive();

                var archivedBundle = _bundleRepository.Update(bundle);

                var responseDto = _mapper.Map<BundleResponseDto>(archivedBundle);

                return responseDto;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            };
        }

        public Result<BundleResponseDto> Delete(long id, long authorId)
        {
            try
            {
                Expression<Func<Bundle, bool>> filter = b => b.Id == id && b.AuthorId == authorId &&
                                                        (b.Status == Domain.Bundles.BundleStatus.Draft || b.Status == Domain.Bundles.BundleStatus.Archived);
                Bundle bundle = _bundleRepository.Get(filter, include: "BundleItems");

                bundle.Delete();

                var deletedBundle = _bundleRepository.Update(bundle);

                var responseDto = _mapper.Map<BundleResponseDto>(deletedBundle);

                return responseDto;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            };
        }

        public Result<List<BundleResponseDto>> GetByAuthor(long authorId)
        {
            var result = _bundleRepository.GetAll(b => b.AuthorId == authorId && b.Status != Domain.Bundles.BundleStatus.Deleted, include: "BundleItems");
            var mapped = new List<BundleResponseDto>();
            foreach (var bundle in result)
            {
                mapped.Add(_mapper.Map<BundleResponseDto>(bundle));
            }
            return mapped;
        }

        public Result<List<BundleResponseDto>> GetPublished(long authorId)
        {
            var result = _bundleRepository.GetAll(b => b.Status == Domain.Bundles.BundleStatus.Published, include: "BundleItems");
            var mapped = new List<BundleResponseDto>();
            foreach (var bundle in result)
            {
                mapped.Add(_mapper.Map<BundleResponseDto>(bundle));
            }
            return mapped;
        }

        public Result<BundleResponseDto> GetById(long id)
        {
            try
            {
                var result = _bundleRepository.Get(b => b.Id == id, include: "BundleItems");
                var mapped = _mapper.Map<BundleResponseDto>(result);
                return mapped;
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
