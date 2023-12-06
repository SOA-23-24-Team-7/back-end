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

    public BundleRecordService(IMapper mapper, IBundleRecordRepository bundleRecordRepository)
    {
        _mapper = mapper;
        _bundleRecordRepository = bundleRecordRepository;
    }

    public Result<List<BundleRecordResponseDto>> GetAllByTourist(long touristId)
    {
        var results = new List<BundleRecordResponseDto>();
        var bundleRecords = _bundleRecordRepository.GetAll(br => br.TouristId ==  touristId);
        foreach (var bundleRecord in bundleRecords)
        {
            var dto = _mapper.Map<BundleRecordResponseDto>(bundleRecord);
            results.Add(dto);
        }
        return results;
    }
}
