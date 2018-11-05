using System.ComponentModel.DataAnnotations;

namespace MyTvSeries.Web.Models.People
{
    public class PersonIndexViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public byte[] PosterContent { get; set; }

        public string SearchKeyword { get; set; }
    }
}
