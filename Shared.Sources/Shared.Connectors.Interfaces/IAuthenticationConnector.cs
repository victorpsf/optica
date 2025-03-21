﻿using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Input;
using Shared.Dtos.Input.AuthenticationModules;

namespace Shared.Connectors.Interfaces;

public interface IAuthenticationConnector<T>
{
    Task<T> Index(
        Autentication autentication
    );

    Task<T> Code(
        AuthenticationCode authentication
    );

    Task<T> Enterprises(
        AuthenticationName authenticationName
    );
}
