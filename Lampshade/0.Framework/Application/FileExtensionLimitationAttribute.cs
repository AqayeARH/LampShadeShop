using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0.Framework.Application
{
    public class FileExtensionLimitationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _validExtension;
        public FileExtensionLimitationAttribute(string[] validExtension)
        {
            _validExtension = validExtension;
        }

        public override bool IsValid(object value)
        {
            if (value is not IFormFile file) return true;

            var fileExtension = Path.GetExtension(file.FileName);

            return _validExtension.Contains(fileExtension);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val-fileExtensionLimit", ErrorMessage);
        }
    }
}
