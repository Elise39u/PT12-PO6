using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;

namespace Kickstart.models
{
    public class ApiCalls
    {
        private string API_Address { get; } = "http://i403879.hera.fhict.nl/api/";

        public User GetUser(string username)
        {

            User askedUser = new User();

            //The url where we get the users

            try
            {
                //Create the request
                var webRequest = WebRequest.Create(API_Address);
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
                            askedUser = Data
                                .Where(u => u.Username == username)
                                .FirstOrDefault();
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

        
        public void UpdateUser(int id, string username, string password, string email)
        {

            WebClient webClient = new WebClient();

            string jsonUsername = JsonConvert.SerializeObject(username);
            string json;

            if(password == "")
            {
                json = "{\"username\":" + jsonUsername + ","
                    + "\"Email\":" + email + "}";
            }
            else
            {
                 json = "{\"username\":" + jsonUsername + ","
                    + "\"Email\":" + email + ","
                    + "\"Password\":" + password + "}";
            }

            //Make the call
            webClient.Headers.Add("Content-Type", "application/json");
            string reply = webClient.UploadString(API_Address + $"users/{id}", "PUT", json);
        }
    }
}
