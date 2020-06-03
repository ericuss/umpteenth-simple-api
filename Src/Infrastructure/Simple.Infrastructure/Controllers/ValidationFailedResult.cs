// Copyright (c) Simple. All rights reserved.

namespace Simple.Infrastructure.ControllersCore
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            this.StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
