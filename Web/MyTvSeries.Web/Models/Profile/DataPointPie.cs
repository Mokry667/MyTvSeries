using System.Runtime.Serialization;

namespace MyTvSeries.Web.Models.Profile
{
    [DataContract]
    public class DataPointPie
    {
        public DataPointPie(double y, string label)
        {
            Y = y;
            IndexLabel = label;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public double? Y;

        [DataMember(Name="indexLabel")]
        public string IndexLabel;
    }
}
