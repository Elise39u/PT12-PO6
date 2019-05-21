using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kickstart.models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User()
        {

        }

        public User GetUser(string username)
        {
            User askedUser = new User();

            //The url where we get the users
            string WebUrl = "http://i403879.hera.fhict.nl/api/users";

            try
            {
                //Create the request
                var webRequest = WebRequest.Create(WebUrl);
                //Check if the Request isn`t null
                if (webRequest != null)
                {
                    //Set the settinges of the webrequest
                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.ContentType = "application/json";
                    //Try to get a response from the website
                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        //Read the response 
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            
                            // set the response to a var
                            var jsonResponse = sr.ReadToEnd();
                            //Turn the json response intoo a user list
                            List<User> Data = JsonConvert.DeserializeObject<List<User>>(jsonResponse);
                            //Something goes wrong in user converter with results in 0 id
                            foreach(User user in Data)
                            {
                                //Check for the users username and fill the askedUser
                                if (user.Username == username)
                                {
                                    askedUser.Id = user.Id;
                                    askedUser.Username = user.Username;
                                    askedUser.Password = user.Password;
                                    askedUser.Latitude = user.Latitude;
                                    askedUser.Longitude = user.Longitude;
                                }
                            }
                        }
                    }
                }
            }
            //If a error occured throw a Exception
            catch (WebException ex)
            {
                //Think about a error that fits the return of a user object
            }
            return askedUser;
        }

        public void EditLocation(string username, int userid, double latitude, double longitude)
        {
            //Create a HttpWebrequest and Set the Method,Contenttype,Api key
            var webRequest = (HttpWebRequest)WebRequest.Create("http://i403879.hera.fhict.nl/api/users/" + userid);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.UseDefaultCredentials = true;
            webRequest.Timeout = 36000;

            using (var streamWritter = new StreamWriter(webRequest.GetRequestStream()))
            {
                //Replace the comma in the Longitude and Latitude to avoid Json errors
                string Latitude_Less = latitude.ToString().Replace(',', '.');
                string Longitude_Less = longitude.ToString().Replace(',', '.');

                //Make the Json body
                string json = "{\"username\":" + username + ","
                    + "\"latitude\":" + Latitude_Less + ","
                    + "\"longitude\":" + Longitude_Less + "}";

                //Send the request to the api
                streamWritter.Write(json);
                //Clean the Call
                streamWritter.Flush();
                //Remove the Api Call
                streamWritter.Close();

                //Create a Web Response
                var httpRepsone = (HttpWebResponse)webRequest.GetResponse();
                //Make a Stream Reader of the ResponseStream
                using (var streamReader = new StreamReader(httpRepsone.GetResponseStream()))
                {
                    // Try to Read the response
                    var result = streamReader.ReadToEnd();
                }
            }
        }
    }
}
