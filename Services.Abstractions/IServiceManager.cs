using Services.Abstractions.Authentication;

namespace Services.Abstractions;

public interface IServiceManager
{
    ICustomerService CustomerService { get; }
    IFriendshipService FriendshipService { get; }
    IAuthenticationService AuthenticationService { get; }
    ITokenGeneratorService TokenGeneratorService { get; }
    IPaginationService PaginationService { get; }
    IIconService IconService { get; }
    IExpenseCategoryService ExpenseCategoryService { get; }
    IExpenseTypeService ExpenseTypeService { get; }
    IStorageService StorageService { get; }
    IExpenseService ExpenseService { get; }
    IUserService UserService { get; }
    IAccountService AccountService { get; }
    IGroupService GroupService { get; }
    IReportService ReportService { get; }
}