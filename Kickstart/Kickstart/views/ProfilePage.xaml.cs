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
	public partial class ProfilePage : ContentPage
	{
        public User PersonalUser { get; private set; }
        public User MethodUser { get; set; } = new User();

		public ProfilePage (User user)
		{
			InitializeComponent ();
            PersonalUser = user;
            Init();
            // Solved with https://stackoverflow.com/questions/37660525/how-to-have-2-data-binding-fields-in-one-xamarin-forms-label
            Lbl_InfoLabel.BindingContext = PersonalUser;
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
            Lbl_Welcome.Text = $"{PersonalUser.Username} your profile page";

            //Footer styling
            Footer.BackgroundColor = Constant.BackGroundColor;

            Lbl_Cr.HorizontalOptions = LayoutOptions.Center;
            Lbl_Cr.VerticalOptions = LayoutOptions.EndAndExpand;
            Lbl_Cr.VerticalTextAlignment = TextAlignment.Center;
            Lbl_Cr.HeightRequest = 80;
            Lbl_Cr.TextColor = Constant.TextColor;
        }

        public User getUser()
        {
            User TryedUser = MethodUser.GetUser(PersonalUser.Username);
            return TryedUser;
        }
    }
}