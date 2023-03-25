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
    private readonly Lazy<IPaginationService> _lazyPaginationService;
    private readonly Lazy<IIconService> _lazyIconService;
    private readonly Lazy<IStorageService> _lazyStorageService;
    private readonly Lazy<IExpenseCategoryService> _lazyExpenseCategoryService;

    public ServiceManager(IMapper mapper, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher,
        AuthenticationOptions authenticationOptions)
    {
        _lazyStorageService = new Lazy<IStorageService>(new DriveStorageService());
        _lazyPaginationService = new Lazy<IPaginationService>(new PaginationService());
        _lazyCustomerService = new Lazy<ICustomerService>(new CustomerService(unitOfWork, mapper, passwordHasher));
        _lazyFriendshipService = new Lazy<IFriendshipService>(new FriendshipService(unitOfWork, mapper, PaginationService));
        _lazyTokenGeneratorService =
            new Lazy<ITokenGeneratorService>(new TokenGeneratorService(authenticationOptions, unitOfWork, mapper));
        _lazyAuthenticationService =
            new Lazy<IAuthenticationService>(new AuthenticationService(CustomerService, TokenGeneratorService));
        _lazyIconService = new Lazy<IIconService>(new IconService(unitOfWork, mapper, StorageService));
        _lazyExpenseCategoryService = new Lazy<IExpenseCategoryService>(new ExpenseCategoryService(unitOfWork, mapper));
    }

    public ICustomerService CustomerService => _lazyCustomerService.Value;
    public IFriendshipService FriendshipService => _lazyFriendshipService.Value;
    public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
    public ITokenGeneratorService TokenGeneratorService => _lazyTokenGeneratorService.Value;
    public IPaginationService PaginationService => _lazyPaginationService.Value;
    public IIconService IconService => _lazyIconService.Value;
    public IStorageService StorageService => _lazyStorageService.Value;
    public IExpenseCategoryService ExpenseCategoryService => _lazyExpenseCategoryService.Value;
}