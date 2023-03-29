using AutoMapper;
using Contracts.Authentication;
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

    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<CustomerResponse> GetCustomerByCredentialsAsync(SignInUserCredentials credentials, CancellationToken token = default)
    {
        var customer = await _unitOfWork.CustomerRepository
            .GetByCredentialsAsync(credentials.Username, credentials.Password, token);
        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task<CustomerResponse> CreateCustomerAsync(SignUpUserCredentials credentials, CancellationToken token = default)
    {
        var customer = new Customer
        {
            Email = credentials.Email,
            Name = credentials.Username,
            RoleId = RoleId.User
        };
        var passwordHash = _passwordHasher.HashPassword(customer, credentials.Password);
        customer.Password = passwordHash;
        await _unitOfWork.CustomerRepository.AddCustomerAsync(customer, token);
        await _unitOfWork.SaveChangesAsync(token);
        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task<CustomerResponse> GetCustomerByIdAsync(Guid customerId, CancellationToken token = default)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByCustomerIdAsync(customerId, token);
        return _mapper.Map<CustomerResponse>(customer);
    }
}