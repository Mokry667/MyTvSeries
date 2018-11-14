using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyTvSeries.Web.Models.Profile
{
    public class CalendarViewModel
    {
        public string Username { get; set; }

        public int CurrentWeek { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime StartOfWeekDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EndOfWeekDate { get; set; }

        public List<SeriesOnCalendarViewModel> Monday { get; set; }
        public List<SeriesOnCalendarViewModel> Tuesday { get; set; }
        public List<SeriesOnCalendarViewModel> Wednesday { get; set; }
        public List<SeriesOnCalendarViewModel> Thursday { get; set; }
        public List<SeriesOnCalendarViewModel> Friday { get; set; }
        public List<SeriesOnCalendarViewModel> Saturday { get; set; }
        public List<SeriesOnCalendarViewModel> Sunday { get; set; }

        public DayOfWeek Today { get; set; }
    }
}
