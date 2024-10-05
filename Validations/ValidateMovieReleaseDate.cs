using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using MovieCardsAPI.Data;
using MovieCardsAPI.Models;
using MovieCardsAPI.Models.Entities;
using MovieCardsAPI.Models.Dtos;
using System.IO;
using Bogus.DataSets;


namespace MovieCardsAPI.Validations {
    public class CorrectMovieReleasedateAttribute : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (DateOnly.FromDateTime(DateTime.Now).CompareTo((DateOnly)value) >= 0) {
                return ValidationResult.Success;
            }
            return new ValidationResult("Movie release date must not be in the future");
        }
    }
}
