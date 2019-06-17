using Kickstart.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Kickstart.views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public string Username { get; private set; }
        public string Password { get; private set; }

		public LoginPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent ();
            Init();
		}

        void Init()
        {
            //Activty spinner set up
            this.IsBusy = false;
            Spinner.Color = Color.LimeGreen;

            //Header styling
            Header.BackgroundColor = Constant.BackGroundColor;

            Lbl_Header.TextColor = Constant.TextColor;
            Lbl_Header.Margin = new Thickness(5, 5, 0, 10);
            Lbl_Header.HeightRequest = Constant.HeightRequest;

            //Content styling
            Content.BackgroundColor = Constant.ContentLayout;
            EntryUsername.BackgroundColor = Color.FromRgb(255, 255, 255);
            EntryUsername.TextColor = Color.FromHex("#EFB02B");

            EntryPassword.BackgroundColor = Color.FromRgb(255, 255, 255);
            EntryPassword.TextColor = Color.FromHex("#EFB02B");
            EntryPassword.IsPassword = true;

            //Footer styling
            Footer.BackgroundColor = Constant.BackGroundColor;

            Lbl_Cr.HorizontalOptions = LayoutOptions.Center;
            Lbl_Cr.VerticalOptions = LayoutOptions.EndAndExpand;
            Lbl_Cr.VerticalTextAlignment = TextAlignment.Center;
            Lbl_Cr.HeightRequest = 80;
            Lbl_Cr.TextColor = Constant.TextColor;
        }

        private async void BtnSignin_Clicked(object sender, EventArgs e)
        {
            //Set up all the pre info
            User user = new User(EntryUsername.Text, EntryPassword.Text);

            //Check the username and password of one of them is empty
            //If one them is empty show them a error
            if(user.Username == "" || user.Password == "")
            {
                Login_Lbl.Text = "Please fill all the fields in";
                Login_Lbl.TextColor = Constant.ErrorColor;
            }
            else
            {
                this.IsBusy = true;
                //Else Start the login
                Login_Lbl.Text = "Logging in please wait";
                Login_Lbl.TextColor = Color.LimeGreen;

                string loginUrl = "http://i403879.hera.fhict.nl/api/login";

                //Encrypt data and encode it to send it towards the Login api
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", user.Username),
                    new KeyValuePair<string, string>("password", user.Password)
                };
                HttpContent loginContent = new FormUrlEncodedContent(postData);

                //Start the login check
                try
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    
                    using (var httpRepsone = await client.PostAsync(loginUrl, loginContent))
                    {
                        if (httpRepsone.StatusCode == HttpStatusCode.OK)
                        {
                            var content = await httpRepsone.Content.ReadAsStringAsync();
                            var results = JsonConvert.DeserializeObject(content);
                            var startPage = new MainPage(user);
                            NavigationPage.SetHasNavigationBar(startPage, false);
                            await Navigation.PushAsync(startPage);
                            Login_Lbl.Text = "";
                            EntryPassword.Text = "";
                        }
                        else
                        {
                            if(HttpStatusCode.InternalServerError == httpRepsone.StatusCode)
                            {
                                await DisplayAlert("Error occuerd", "The monkeys did something bad hang on", "Okay");
                            }
                            if(HttpStatusCode.MethodNotAllowed == httpRepsone.StatusCode)
                            {
                                await DisplayAlert("Error occuerd", "Monkeys told me you used the wrong request", "Okay");
                            }
                            if(HttpStatusCode.NotFound == httpRepsone.StatusCode)
                            {
                                await DisplayAlert("Error occuerd", "The monkeys are missing or your info hang on when i take a look", "Okay");
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    await DisplayAlert("Error occuerd", error.ToString(), "Okay");
                }
            }
            Login_Lbl.Text = "";
            this.IsBusy = false;
        }
    }
}