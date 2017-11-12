using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace ProgramAPIAssignment
{
    /// <summary>
    /// The project is sovled using JObject and Normal Models using Serialization and Deserialization
    /// </summary>
    public class APIConsumption
    {
        private const string RequestURL = "https://localhost:2401/partners?userKey=4c3a4873b63fbaae3143de8876d7";
        private const string ResponseURL = "https://localhost:3402/results?userKey=4c3a4873b63fbaae3143de8876d7";
        
        static void Main()
        {
            //This takes the classes which uses Serialization and Deserialization Concepts.
            ProgramAPIAssignmentObjectcs.Run(RequestURL, ResponseURL);
        }
    }
}
