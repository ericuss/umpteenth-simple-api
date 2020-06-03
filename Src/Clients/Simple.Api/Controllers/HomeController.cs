// Copyright (c) simple. All rights reserved.

namespace Simple.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Simple.Infrastructure.ControllersCore;

    public class HomeController : ControllerCore
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return this.Ok();
        }
    }
}
