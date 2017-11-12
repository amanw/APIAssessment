using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramAPIAssignment
{

    /// <summary>
    /// This class is used for Request and Respone API
    /// </summary>
    public class Service
    {

        public static string GetResponseFromApi(string Request)
        {
            HttpClient client = new HttpClient();
            string requestResult="";
            client.BaseAddress = new Uri(Request);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage response = client.GetAsync(Request).Result;
            if (response.IsSuccessStatusCode)
            {
                 requestResult = response.Content.ReadAsStringAsync().Result;
                 response.Dispose();
                 client.Dispose();
               
            }
            return requestResult;
        }
        

        public async static Task PostExecutedRespone(string Response, string output)
        {
            HttpClient client1 = new HttpClient();
            string msg = string.Empty;
            client1 = new HttpClient();
            client1.BaseAddress = new Uri(Response);
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //HttpResponseMessage newRes = client1.PostAsJsonAsync(Response, output).Result;
            try
            {
                HttpResponseMessage newRes = await client1.PostAsync(Response, new StringContent(output, Encoding.UTF8, "application/json"));
                if (newRes.IsSuccessStatusCode)
                {
                    string content = await newRes.Content.ReadAsStringAsync();
                    var message = JObject.Parse(content)["message"];
                    Console.WriteLine("Success");
                    Console.WriteLine(message);
                    Console.ReadLine();
                    
                }
                else
                {
                    string content = await newRes.Content.ReadAsStringAsync();
                    Console.WriteLine("Failed");
                    var Msg = JsonConvert.DeserializeObject<ResponseAPI>(content);
                    Console.WriteLine(newRes.ReasonPhrase);
                    Console.WriteLine("Status" + ":" + Msg.status + "\n" + "Message" + ":" + Msg.message + "\n"+ "CorrelationId" + ":" + Msg.correlationId + "\n"+ "RequestId" + ":" + Msg.requestId);
                    Console.ReadLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                Console.ReadLine();
            }
        }
    }
}
