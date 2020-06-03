// Copyright (c) Simple. All rights reserved.

namespace Simple.Infrastructure.ControllersCore
{
    using System.Collections.Generic;
    using System.Linq;
    using Simple.Infrastructure.Entities;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ValidationResultModel
    {
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            this.Message = "Validation Failed";
            this.Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }

        public string Message { get; }

        public List<ValidationError> Errors { get; }
    }
}
