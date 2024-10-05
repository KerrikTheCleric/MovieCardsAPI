using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using MovieCardsAPI.Data;
using MovieCardsAPI.Models;
using MovieCardsAPI.Models.Entities;
using MovieCardsAPI.Models.Dtos;


namespace MovieCardsAPI.Validations{
    public class UniqueMovieTitleAttribute : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var context = (MovieContext)validationContext.GetService(typeof(MovieContext));
            if (!context.Movies.Any(m => m.Title == value.ToString())) {
                return ValidationResult.Success;
            }
            return new ValidationResult("Movie title must be unique");
        }
    }
}
