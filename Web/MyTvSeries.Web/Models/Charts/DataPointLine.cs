using System.Runtime.Serialization;


namespace MyTvSeries.Web.Models.Charts
{
    [DataContract]
    public class DataPointLine
    {
        public DataPointLine(double y, string x)
        {
            Y = y;
            X = x;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public double? Y;

        [DataMember(Name = "label")]
        public string X;
    }
}
