using System.Runtime.Serialization;

namespace MyTvSeries.Web.Models.Profile
{
    [DataContract]
    public class DataPointStackedBar
    {
        public DataPointStackedBar(double y, string label, string color)
        {
            Y = y;
            Label = label;
            Color = color;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public double? Y;

        [DataMember(Name = "label")]
        public string Label;

        [DataMember(Name = "color")]
        public string Color;
    }
}
