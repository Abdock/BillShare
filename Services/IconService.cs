using AutoMapper;
using Contracts.DTOs.General;
using Contracts.DTOs.Icons;
using Contracts.Responses.Icons;
using Domain.Models;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

public class IconService : IIconService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStorageService _storageService;

    public IconService(IUnitOfWork unitOfWork, IMapper mapper, IStorageService storageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _storageService = storageService;
    }

    public async Task<IconResponse> CreateIconAsync(CreateIconDto dto, CancellationToken cancellationToken = default)
    {
        var file = new StorageFile
        {
            Id = Guid.NewGuid(),
            Data = dto.IconImageData,
            Extension = dto.Extension
        };
        var path = await _storageService.WriteDataAsync(file, cancellationToken);
        var icon = new Icon
        {
            Id = file.Id,
            Url = path
        };
        await _unitOfWork.IconRepository.AddIconAsync(icon, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<IconResponse>(icon);
    }

    public async Task<IEnumerable<IconResponse>> GetAllIconsAsync(CancellationToken cancellationToken = default)
    {
        var icons = await _unitOfWork.IconRepository.GetAllIconsAsync(cancellationToken);
        return icons.Select(icon => _mapper.Map<IconResponse>(icon));
    }

    public async Task<IconResponse> GetIconByIdAsync(Guid iconId, CancellationToken cancellationToken = default)
    {
        var icon = await _unitOfWork.IconRepository.GetIconAsync(iconId, cancellationToken);
        return _mapper.Map<IconResponse>(icon);
    }
}