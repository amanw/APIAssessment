using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace ProgramAPIAssignment
{
    [DataContract]
    public class Country
    {
        [DataMember]
        public int attendeeCount { get; set; }
        [DataMember]
        public List<object> attendees { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string startDate { get; set; }
    }
    [DataContract]
    public class CountryList
    {
        [DataMember]
        public List<Country> countries { get; set; }
    }
}
