using System.Collections.Generic;

namespace MyTvSeries.Web.Models.People
{
    public class PersonDetailDepartmentCrewViewModel
    {
        public string DepartmentName { get; set; }
        public List<PersonDetailCrewViewModel> Crew { get; set; }
    }
}
