using System.ComponentModel.DataAnnotations;
using MyTvSeries.Web.Models.Series;

namespace MyTvSeries.Web.Models.Validators
{
    public class SeasonNumberValidator : ValidationAttribute
    {
        private readonly string _maxPropertyName;

        public SeasonNumberValidator(string maxPropertyName)
        {
            _maxPropertyName = maxPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            UserSeriesDetailViewModel viewModel = (UserSeriesDetailViewModel)validationContext.ObjectInstance;

            var maxProperty = validationContext.ObjectType.GetProperty(_maxPropertyName);

            if (maxProperty == null)
                return new ValidationResult(string.Format("Unknown property {0}", _maxPropertyName));

            var maxValue = (int)maxProperty.GetValue(validationContext.ObjectInstance, null);

            if (viewModel.SeasonsWatched < 0 || viewModel.SeasonsWatched > maxValue)
            {
                return new ValidationResult("Incorrect number of seasons");
            }

            return ValidationResult.Success;
        }

        // TODO add cleint side validation
    }
}
