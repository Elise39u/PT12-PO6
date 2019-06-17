using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kickstart.models
{
    public class User
    {
        [JsonProperty]
        public int Id { get; private set; }
        [JsonProperty]
        public string Username { get;  private set; }
        [JsonProperty]
        public string Password { get; private set; }
        [JsonProperty]
        public string Email { get; private set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string BindData => $"Username: {Username} \n" + $"Email: {Email} \n" + $"Latitude: {Latitude} \n" + $"Longitude: {Longitude}";

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User()
        {

        }

        public User(int id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }

        public void EditLocation(string username, int userid, double latitude, double longitude)
        {

            WebClient webClient = new WebClient();
            //Replace the comma in the Longitude and Latitude to avoid Json errors
            string Latitude_Less = latitude.ToString().Replace(',', '.');
            string Longitude_Less = longitude.ToString().Replace(',', '.');

            string jsonUsername = JsonConvert.SerializeObject(username);
            string json = "{\"username\":" + jsonUsername + ","
                + "\"latitude\":" + Latitude_Less + ","
                + "\"longitude\":" + Longitude_Less + "}";
            

            //Make the call
            string webUri = "http://i403879.hera.fhict.nl/api/users/" + userid;
            webClient.Headers.Add("Content-Type", "application/json");
            string reply = webClient.UploadString(webUri, "PUT", json);
        }
    }
}
