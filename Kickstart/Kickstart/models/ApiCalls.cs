using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using Kickstart.views;
using Xamarin.Forms;

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
                var webRequest = WebRequest.Create(API_Address + "users");
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
                if(ex.InnerException is TimeoutException)
                {
                    throw new Exception(ex.Message);
                }
            }
            return askedUser;
        }

        
        public string UpdateUserAsync(int id, string username, string password, string email)
        {
            WebClient webClient = new WebClient();

            string jsonUsername = JsonConvert.SerializeObject(username);
            string jsonEmail = JsonConvert.SerializeObject(email);
            string json;
            try
            {
                if (password == "")
                {
                    json = "{\"username\":" + jsonUsername + ","
                        + "\"Email\":" + jsonEmail + "}";
                }
                else
                {
                    json = "{\"username\":" + jsonUsername + ","
                       + "\"Email\":" + jsonEmail + ","
                       + "\"Password\":" + password + "}";
                }

                //Make the call
                webClient.Headers.Add("Content-Type", "application/json");
                string reply = webClient.UploadString(API_Address + $"users/{id}", "PUT", json);
                return "Succes";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
