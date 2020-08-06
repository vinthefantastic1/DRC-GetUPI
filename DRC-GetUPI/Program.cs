using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace DRC_GetUPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://wbgservice.worldbank.org/json/ENTERPRISE/MASTER_DATA/HR/PERSON/WB_STAFF?IN_UPI=";

            Console.Write("Enter UPI: ");
            var UPI = Console.ReadLine();

            var newurl = $"{url}{UPI}";

            var res = Getdata(newurl);

            var json = JObject.Parse(res);

            var data = json["response"]["data"][0];

            var staff = new StaffInformation();
            
            staff.FullName = data["FULL_NAME"].ToString();
            staff.WorkPhone = data["WORK_PHONE"].ToString();
            staff.RoomNumber = data["ROOM_NUM"].ToString();
            staff.WorkOUI = data["WORK_OUI"].ToString();
            staff.WorkAlpha = data["WORK_ALPHA"].ToString();
            staff.MSN = data["MAIL_STOP_NBR"].ToString();


        }


        static string Getdata(string url)
        {

            var client = new RestClient(url);
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            var DRC_Auth = Environment.GetEnvironmentVariable("COMPOSITEBASICAUTH2");

            request.AddHeader("Authorization", $"Basic {DRC_Auth}");
            request.AddHeader("Cookie", "wbgservice.cookie=277788938.47652.0000");
            IRestResponse response = client.Execute(request);
            

            return response.Content;
        }
    }
}
