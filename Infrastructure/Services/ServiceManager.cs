using AutoMapper;
using Contracts.Authentication;
using Domain.Repositories;
using Infrastructure.Authentication.Service;
using Services;
using Services.Abstractions;
using Services.Abstractions.Authentication;

namespace Infrastructure.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICustomerService> _lazyCustomerService;
    private readonly Lazy<IAuthenticationService> _lazyAuthenticationService;
    private readonly Lazy<IFriendshipService> _lazyFriendshipService;
    private readonly Lazy<ITokenGeneratorService> _lazyTokenGeneratorService;

    public ServiceManager(IMapper mapper, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher,
        AuthenticationOptions authenticationOptions)
    {
        _lazyCustomerService = new Lazy<ICustomerService>(new CustomerService(unitOfWork, mapper, passwordHasher));
        _lazyFriendshipService = new Lazy<IFriendshipService>(new FriendshipService(unitOfWork, mapper));
        _lazyTokenGeneratorService =
            new Lazy<ITokenGeneratorService>(new TokenGeneratorService(authenticationOptions, unitOfWork, mapper));
        _lazyAuthenticationService =
            new Lazy<IAuthenticationService>(new AuthenticationService(CustomerService, TokenGeneratorService));
    }

    public ICustomerService CustomerService => _lazyCustomerService.Value;
    public IFriendshipService FriendshipService => _lazyFriendshipService.Value;
    public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
    public ITokenGeneratorService TokenGeneratorService => _lazyTokenGeneratorService.Value;
}