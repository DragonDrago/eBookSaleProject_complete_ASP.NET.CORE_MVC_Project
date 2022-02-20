using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
namespace eBookSaleProject.Attributes
{
    public class AllowedExtensionAttributes:ValidationAttribute
    {
        private readonly string[] extensions;

        public AllowedExtensionAttributes(string[] extensions)
        {
            this.extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"Only files with valid extensions ({string.Join(" , ", extensions)}) are allowed.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
