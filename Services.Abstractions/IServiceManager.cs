﻿using Services.Abstractions.Authentication;

namespace Services.Abstractions;

public interface IServiceManager
{
    ICustomerService CustomerService { get; }
    IFriendshipService FriendshipService { get; }
    IAuthenticationService AuthenticationService { get; }
    ITokenGeneratorService TokenGeneratorService { get; }
    IPaginationService PaginationService { get; }
    IIconService IconService { get; }
    IStorageService StorageService { get; }
}