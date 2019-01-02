using OilTeamProject.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OilTeamProject.Validation
{
    public class UniqueProductSlug :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var product = (Product)validationContext.ObjectInstance;
            var productSlugExists = product.CheckIfSlugExists(product.Slug, product.ID);
            var productExists = product.CheckIfProductExists(product.ID);

            return (productSlugExists && !productExists)
                ? new ValidationResult("The slug already exists.Try another one")
                : ValidationResult.Success;
        }
    }
}