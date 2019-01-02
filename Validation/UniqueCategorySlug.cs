using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OilTeamProject.Models.Products;

namespace OilTeamProject.Validation
{
    public class UniqueCategorySlug : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var category = (Category)validationContext.ObjectInstance;
            var categorySlugExists = category.CheckIfSlugExists(category.Slug);

            return (categorySlugExists)
                ? new ValidationResult("The slug already exists.Try another one")
                : ValidationResult.Success;
        }
    }
}