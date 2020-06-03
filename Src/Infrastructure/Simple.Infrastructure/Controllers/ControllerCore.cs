// Copyright (c) Simple. All rights reserved.

namespace Simple.Infrastructure.ControllersCore
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ValidateModel]
    //[Authorize]
    public abstract class ControllerCore : ControllerBase
    {
    }
}