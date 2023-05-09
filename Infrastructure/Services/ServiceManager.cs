using AutoMapper;
using Contracts.Authentication;
using Domain.Repositories;
using Infrastructure.Authentication.Service;
using Microsoft.AspNetCore.Hosting;
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
    private readonly Lazy<IExpenseTypeService> _lazyExpenseTypeService;
    private readonly Lazy<IExpenseService> _lazyExpenseService;
    private readonly Lazy<IUserService> _lazyUserService;
    private readonly Lazy<IAccountService> _lazyAccountService;
    private readonly Lazy<IGroupService> _lazyGroupService;
    private readonly Lazy<IReportService> _lazyReportService;

    public ServiceManager(IMapper mapper, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher,
        AuthenticationOptions authenticationOptions, IWebHostEnvironment environment)
    {
        var wwwRoot = environment.WebRootPath;
        _lazyStorageService = new Lazy<IStorageService>(new DriveStorageService(wwwRoot));
        _lazyPaginationService = new Lazy<IPaginationService>(new PaginationService());
        _lazyCustomerService = new Lazy<ICustomerService>(new CustomerService(unitOfWork, mapper, passwordHasher, StorageService));
        _lazyFriendshipService = new Lazy<IFriendshipService>(new FriendshipService(unitOfWork, mapper, PaginationService));
        _lazyTokenGeneratorService =
            new Lazy<ITokenGeneratorService>(new TokenGeneratorService(authenticationOptions, unitOfWork, mapper));
        _lazyAuthenticationService =
            new Lazy<IAuthenticationService>(new AuthenticationService(CustomerService, TokenGeneratorService));
        _lazyIconService = new Lazy<IIconService>(new IconService(unitOfWork, mapper, StorageService));
        _lazyExpenseCategoryService = new Lazy<IExpenseCategoryService>(new ExpenseCategoryService(unitOfWork, mapper));
        _lazyExpenseTypeService = new Lazy<IExpenseTypeService>(new ExpenseTypeService(unitOfWork, mapper));
        _lazyExpenseService = new Lazy<IExpenseService>(new ExpenseService(unitOfWork, mapper, PaginationService));
        _lazyUserService = new Lazy<IUserService>(new UserService(unitOfWork, mapper, PaginationService));
        _lazyAccountService = new Lazy<IAccountService>(new AccountService(unitOfWork, mapper));
        _lazyGroupService = new Lazy<IGroupService>(new GroupService(unitOfWork, mapper, PaginationService));
        _lazyReportService = new Lazy<IReportService>(new ReportService(unitOfWork, mapper));
    }

    public ICustomerService CustomerService => _lazyCustomerService.Value;
    public IFriendshipService FriendshipService => _lazyFriendshipService.Value;
    public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
    public ITokenGeneratorService TokenGeneratorService => _lazyTokenGeneratorService.Value;
    public IPaginationService PaginationService => _lazyPaginationService.Value;
    public IIconService IconService => _lazyIconService.Value;
    public IStorageService StorageService => _lazyStorageService.Value;
    public IExpenseCategoryService ExpenseCategoryService => _lazyExpenseCategoryService.Value;
    public IExpenseTypeService ExpenseTypeService => _lazyExpenseTypeService.Value;
    public IExpenseService ExpenseService => _lazyExpenseService.Value;
    public IUserService UserService => _lazyUserService.Value;
    public IAccountService AccountService => _lazyAccountService.Value;
    public IGroupService GroupService => _lazyGroupService.Value;
    public IReportService ReportService => _lazyReportService.Value;
}