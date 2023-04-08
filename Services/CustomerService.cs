using AutoMapper;
using Contracts.Authentication;
using Contracts.DTOs.Customers;
using Contracts.DTOs.General;
using Contracts.Responses.Customers;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using Services.Abstractions;
using Services.Abstractions.Authentication;

namespace Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IStorageService _storageService;

    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher,
        IStorageService storageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _storageService = storageService;
    }

    public async Task<CustomerResponse> GetCustomerByCredentialsAsync(SignInUserCredentials credentials,
        CancellationToken cancellationToken = default)
    {
        var customer = await _unitOfWork.CustomerRepository
            .GetByCredentialsAsync(credentials.Username, credentials.Password, cancellationToken);
        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task<CustomerResponse> CreateCustomerAsync(SignUpUserCredentials credentials,
        CancellationToken cancellationToken = default)
    {
        var customer = new Customer
        {
            Email = credentials.Email,
            Name = credentials.Username,
            RoleId = RoleId.User
        };
        var passwordHash = _passwordHasher.HashPassword(customer, credentials.Password);
        customer.Password = passwordHash;
        await _unitOfWork.CustomerRepository.AddCustomerAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task<CustomerResponse> GetCustomerByIdAsync(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByCustomerIdAsync(customerId, cancellationToken);
        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task<CustomerAvatarIcon> ChangeCustomerAvatarAsync(ChangeCustomerAvatarDto dto,
        CancellationToken cancellationToken = default)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByCustomerIdAsync(dto.CustomerId, cancellationToken);
        var storageFile = _mapper.Map<StorageFile>(dto);
        customer.AvatarUrl = await _storageService.WriteDataAsync(storageFile, cancellationToken);
        _unitOfWork.CustomerRepository.Update(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CustomerAvatarIcon>(customer);
    }

    public async Task DeleteCustomerAvatarAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByCustomerIdAsync(customerId, cancellationToken);
        customer.AvatarUrl = null;
        _unitOfWork.CustomerRepository.Update(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}