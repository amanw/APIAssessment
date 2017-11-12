using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProgramAPIAssignment
{
    [DataContract]
    public class ResponseAPI
    {
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string correlationId { get; set; }
        [DataMember]
        public string requestId { get; set; }
    }
}
