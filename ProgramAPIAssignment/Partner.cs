using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace ProgramAPIAssignment
{
    [DataContract]
    public class Partner
    {
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public List<string> availableDates { get; set; }
        [DataMember]
        public bool check { get; set; }
    }
    [DataContract]
    public class PartnerList
    {
        [DataMember]
        public List<Partner> partners { get; set; }
    }
}
