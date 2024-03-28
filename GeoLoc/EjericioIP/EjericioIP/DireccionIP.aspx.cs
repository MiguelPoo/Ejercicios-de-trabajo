using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EjericioIP.Models;
using Newtonsoft.Json;

namespace EjericioIP
{
    public partial class DireccionIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Click(object sender, EventArgs e)
        {
            string json;
            string ip = txtIp.Text;
            string uri = "http://ip-api.com/json/" + ip;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            using(Stream stream = response.GetResponseStream())
            {
               StreamReader reader = new StreamReader(stream);
               json = reader.ReadToEnd();

            }

            RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
            Lbl1.Text = "query: " + root.query;
            Lbl2.Text = ",status: " + root.status;
            Lbl3.Text = ",continent: " + root.country;
            Lbl4.Text = ",continentCode: " + root.countryCode;
            Lbl5.Text = ",region: " + root.region;
            Lbl6.Text = ",regionName: " + root.regionName;
            Lbl7.Text = ",city: " + root.city;
            Lbl8.Text = ",zip: " + root.zip;
            Lbl9.Text = ",lat: " + root.lat.ToString();
            Lbl10.Text = ",lon: " + root.lon.ToString();
            Lbl11.Text = ",timezone: " + root.timezone;
            Lbl12.Text = ",isp: " + root.isp;
            Lbl13.Text = ",org: " + root.org;
            Lbl14.Text = ",@as: " + root.@as;
        }
    }
}