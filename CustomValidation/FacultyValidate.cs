using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CRUDUsingADOApp.CustomValidation {
    public class FacultyValidate : Attribute, IModelValidator {
        public string[] Allowed { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context) {

            if (Allowed.Contains(context.Model as string))
                return Enumerable.Empty<ModelValidationResult>();
            else
                return new List<ModelValidationResult> {
                    new ModelValidationResult("", ErrorMessage)
                };
        }
    }
}
