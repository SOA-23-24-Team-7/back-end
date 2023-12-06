using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases;

public class BundleRecordService : IBundleRecordService
{
    private readonly IMapper _mapper;
    private readonly IBundleRecordRepository _bundleRecordRepository;
    private readonly IBundleRepository _bundleRepository;

    public BundleRecordService(IMapper mapper, IBundleRecordRepository bundleRecordRepository, IBundleRepository bundleRepository)
    {
        _mapper = mapper;
        _bundleRecordRepository = bundleRecordRepository;
        _bundleRepository = bundleRepository;

    }

    public Result<List<BundleRecordResponseDto>> GetAllByTourist(long touristId)
    {
        var results = new List<BundleRecordResponseDto>();
        var bundleRecords = _bundleRecordRepository.GetAll(br => br.TouristId ==  touristId);
        foreach (var bundleRecord in bundleRecords)
        {
            var bundle = _bundleRepository.Get(b => b.Id == bundleRecord.BundleId);
            var dto = _mapper.Map<BundleRecordResponseDto>(bundleRecord);
            dto.BundleName = bundle.Name;
            results.Add(dto);
        }
        return results;
    }
}
