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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent ();
            Init();
		}

        void Init()
        {
            //Header styling
            Header.BackgroundColor = Constant.BackGroundColor;

            Lbl_Header.TextColor = Constant.TextColor;
            Lbl_Header.Margin = new Thickness(5, 5, 0, 10);
            Lbl_Header.HeightRequest = Constant.HeightRequest;

            //Content styling
            EntryUsername.BackgroundColor = Color.FromRgb(255, 255, 255);
            EntryUsername.TextColor = Color.FromRgb(239, 118, 34);

            EntryPassword.BackgroundColor = Color.FromRgb(255, 255, 255);
            EntryPassword.TextColor = Color.FromRgb(239, 118, 34);
            EntryPassword.IsPassword = true;

            //Footer styling
            Footer.BackgroundColor = Constant.BackGroundColor;

            Lbl_Cr.HorizontalOptions = LayoutOptions.Center;
            Lbl_Cr.VerticalOptions = LayoutOptions.EndAndExpand;
            Lbl_Cr.VerticalTextAlignment = TextAlignment.Center;
            Lbl_Cr.HeightRequest = 80;
            Lbl_Cr.TextColor = Constant.TextColor;
        }

        private void BtnSignin_Clicked(object sender, EventArgs e)
        {

        }
    }
}