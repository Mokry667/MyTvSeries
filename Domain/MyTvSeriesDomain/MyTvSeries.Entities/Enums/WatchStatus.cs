using System.ComponentModel.DataAnnotations;

namespace MyTvSeries.Domain.Enums
{
    public enum WatchStatus
    {
        [Display(Name = "Add to list")]
        NotSpecified = 0,

        [Display(Name = "Dropped")]
        Dropped = 1,

        [Display(Name = "Plan to watch")]
        PlanToWatch = 2,

        [Display(Name = "Watching")]
        Watching = 3,

        [Display(Name = "Completed")]
        Completed = 4,

        [Display(Name = "On hold")]
        OnHold = 5
    }
}
