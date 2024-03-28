using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using PublicHoliday.Models;

namespace PublicHoliday.Controllers
{
    public class HomeController : Controller
    {
      

       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPublicHolidays()
        {

            string countryName = Request["country"];
            string year = Request["year"];

            WebRequest request = WebRequest.Create("https://date.nager.at/api/v3/publicholidays/" + year + "/" + countryName);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);
            string responseString = reader.ReadToEnd();
            dynamic json = JsonConvert.DeserializeObject(responseString);

            var results = new List<HolidayDetails>();

            foreach (var item in json)
            {
                results.Add(new HolidayDetails
                {
                    date = item.date,
                    country = item.countryCode,
                    localName = item.localName,
                    name = item.name,

                });
            }

            return View(results.ToList());
        }
    }
}
