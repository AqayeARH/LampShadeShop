using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0.Framework.Application
{
    public class MaxFileSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            if (value is not IFormFile file)
            {
                return true;
            }

            return file.Length <= _maxFileSize;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val","true");
            context.Attributes.Add("data-val-maxFileSize", ErrorMessage);
        }
    }
}
