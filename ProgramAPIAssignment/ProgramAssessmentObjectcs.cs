using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramAPIAssignment
{
    /// <summary>
 
    /// used the concept of models which will be used for serialization and deserialization
    /// Used NewtonSoft.Json library 
    /// </summary>
    public class ProgramAPIAssignmentObjectcs
    {
        //private const string newAttendees = "";
        /// <summary>
        /// Runs to get the output as desired.
        /// </summary>
        /// <param name="Req"></param>
        /// <param name="Res"></param>
        public static void Run(string Req, string Res)
        {
            PartnerList PL = new PartnerList();
            string Result = Service.GetResponseFromApi(Req);
            if (!String.IsNullOrEmpty(Result))
            {
                PL = JsonConvert.DeserializeObject<PartnerList>(Result);
            }
            if (PL.partners.Count > 0)
            {
                var countries = GetCountries(PL.partners);
                string output = JsonConvert.SerializeObject(countries);
                if (!String.IsNullOrEmpty(output))
                {
                    Service.PostExecutedRespone(Res, output).Wait();
                }
            }
        }
        
        /// <summary>
        /// It takes the list of partners to check the minimum date between the partner of same countries
        /// Checks whether the date is consecutive for two day in a row
        /// Discard the partners if null arrived and only select attendees for two day in a row.
        /// Only Selects the Maximum number of attendees for the dates in a row.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static CountryList GetCountries(List<Partner> c)
        {
            CountryList cl = new CountryList();
            cl.countries = new List<Country>();
            string countryName="";
            var cCount = c.OrderBy(t => t.firstName).ToList();
            foreach (var item in cCount)
            {
             
                Country Cy = new Country();
                var startDate = GetDates(item.availableDates);
                if (startDate == null)
                {
                    c.Remove(item);
                    item.check = true;
                    c.Add(item);
                }
                countryName = item.country;
                Cy.attendeeCount = 0;
                //Cy.attendees = null;
                Cy.startDate = startDate;
                Cy.name = countryName;
                if (!String.IsNullOrEmpty(countryName))
                {
                   
                        if (cl.countries.Any(u => u.name == countryName))
                        {
                            Cy = cl.countries.First(u => u.name == countryName);
                            cl.countries.Remove(Cy);
                        }
                    
                    else
                    {
                        Cy.name = countryName;
                    }
                }
                if (startDate == null && Cy.startDate != null)
                {
                    cl.countries.Add(Cy);
                    continue;
                }
                else if (Cy.startDate != null && startDate != null)
                {
                        var oldStartDate = DateTime.Parse(Cy.startDate);
                        var newAttendees1 = ListAttendees(oldStartDate.ToString(), cCount, Cy);
                        var currentStartDate = DateTime.Parse(startDate);
                        var newAttendees = ListAttendees(currentStartDate.ToString(), cCount, Cy);
                        //if (DateTime.Compare(currentStartDate, oldStartDate) < 0)
                        //    Cy.startDate = startDate;

                        if(newAttendees.Count>newAttendees1.Count)
                        {
                            var AttendeesSort = newAttendees.OrderBy(o => o);
                            Cy.attendees = AttendeesSort.Cast<object>().ToList();
                            Cy.attendeeCount = AttendeesSort.Count();
                            Cy.startDate = currentStartDate.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            var AttendeesSort = newAttendees1.OrderBy(o => o);
                            Cy.attendees = AttendeesSort.Cast<object>().ToList();
                            Cy.attendeeCount = AttendeesSort.Count();
                            Cy.startDate = oldStartDate.ToString("yyyy-MM-dd");
                        }
                    }                
                else
                {
                    Cy.startDate = startDate;
                }                
                cl.countries.Add(Cy);
            }
            return cl;
        }

        /// <summary>
        /// Returns the maximum number of attendees.
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="partners"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public static List<string> ListAttendees(string Date, List<Partner> partners, Country C)
        {
            string newdate1 = null;
            string newdate2 = null;
            var K = AttendeesValueModifier(Date);
            if (K != null)
            {
                newdate1 = K.FirstOrDefault();
                newdate2 = K.LastOrDefault();
            }
            var newAttendees = partners.Where(x => x.country == C.name && C.startDate != null && x.availableDates != null && x.availableDates.Contains(newdate1) && x.availableDates.Contains(newdate2) && !x.check).Select(x => x.email).ToList();
            return newAttendees;
        }

        /// <summary>
        /// Check the dates and retun the earliest 
        /// </summary>
        /// <param name="AD"></param>
        /// <returns></returns>
        public static string GetDates(List<string> AD)
        {

            for (int i = 0; i < AD.Count; i++)
            {
                var date1 = AD[i].ToString();
                if ((i + 1) < AD.Count)
                {
                    var date2 = AD[i + 1].ToString();
                    if (CheckDate(date1, date2))
                        return date1;
                }
            }
            return null;
        }

        /// <summary>
        /// Check if the date is valid for two day in a row.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool CheckDate(string date1, string date2)
        {
            if (!string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(date2))
            {
                DateTime dtStart = DateTime.Parse(date1);
                DateTime dtEnd = DateTime.Parse(date2);

                TimeSpan ts = dtEnd - dtStart;

                if (ts.Days == 1)
                    return true;
                else
                    return false;
            }
            return false;
        }
        /// <summary>
        /// It modifies the value of Attendees and Adds the Date by 1 to get the Attendees for two day in a row.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<string> AttendeesValueModifier(string date)
        {
            List<string> Dates = new List<string>();
            if (date != null)
            {
                DateTime date1 = DateTime.Parse(date);
                DateTime date2 = date1.AddDays(1);
                Dates.Add(date1.ToString("yyyy-MM-dd"));
                Dates.Add(date2.ToString("yyyy-MM-dd"));
                return Dates;
            }
            return null;
        }
    }
}
