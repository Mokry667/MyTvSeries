using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTvSeries.Web.Models.People
{
    public class PersonDetailViewModel
    {
        public long PersonId { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Biography { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Birthday { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Deathday { get; set; }

        [Display(Name = "Place of Birth")]
        public string PlaceOfBirth { get; set; }

        public byte[] PosterContent { get; set; }

        public List<PersonDetailDepartmentCrewViewModel> Departments { get; set; }
        public List<PersonDetailCastViewModel> Cast { get; set; }

        public bool IsFavourite { get; set; }
    }
}
