using Kickstart.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kickstart.views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProfilePage : ContentPage
    {
        public User PersonalUser { get; private set; }
        public ApiCalls ApiCaller { get; } = new ApiCalls();

		public EditProfilePage (User user)
		{
			InitializeComponent ();
            PersonalUser = user;
            Init();

            //Check if the user pressed the back button
            Btn_Return.Clicked += async (sender, args) =>
            {
                //If the user pressed the button return it to previous page on the stack
                await Navigation.PopToRootAsync();
            };
        }

        //Code styling and nesscary things that need to run before launch
        void Init()
        {
            //Header styling
            Header.BackgroundColor = Constant.BackGroundColor;

            Lbl_Header.TextColor = Constant.TextColor;
            Lbl_Header.Margin = new Thickness(5, 5, 0, 10);
            Lbl_Header.HeightRequest = Constant.HeightRequest;

            Btn_Return.HorizontalOptions = LayoutOptions.EndAndExpand;

            //Middle part styling
            EntryUsername.Placeholder = PersonalUser.Username;
            EntryUsername.PlaceholderColor = Color.DarkGray;
            EntryPassword.Placeholder = PersonalUser.Password;
            EntryPassword.PlaceholderColor = Color.DarkGray;
            EntryEmail.Placeholder = PersonalUser.Email;
            EntryEmail.PlaceholderColor = Color.DarkGray;

            Btn_Submit.BackgroundColor = Constant.ButtonBackGroundColor;
            Btn_Submit.TextColor = Constant.TextColor;
            Btn_Submit.WidthRequest = Constant.WidthRequest;
            Btn_Submit.HeightRequest = Constant.HeightRequest;


            //Footer styling
            Footer.BackgroundColor = Constant.BackGroundColor;

            Lbl_Cr.HorizontalOptions = LayoutOptions.Center;
            Lbl_Cr.VerticalOptions = LayoutOptions.EndAndExpand;
            Lbl_Cr.VerticalTextAlignment = TextAlignment.Center;
            Lbl_Cr.HeightRequest = 80;
            Lbl_Cr.TextColor = Constant.TextColor;
        }

        private async void Btn_Submit_ClickedAsync(object sender, EventArgs e)
        {
            if(EntryUsername.Text == "" || EntryEmail.Text == "")
            {
                await DisplayAlert("Error", "some * fields are left empty", "abort");
                return;
            }
            ApiCaller.UpdateUser(PersonalUser.Id, EntryUsername.Text, EntryPassword.Text, EntryEmail.Text);
        }
    }
}