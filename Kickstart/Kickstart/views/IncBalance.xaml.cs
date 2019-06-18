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
	public partial class IncBalance : ContentPage
	{
        public User PersonalUser { get; private set; }
        private ApiCalls ApiCaller { get; } = new ApiCalls();

		public IncBalance (User user)
		{
			InitializeComponent();
            Init();
            PersonalUser = user;
            //Check if the user pressed the back button
            Btn_Return.Clicked += async (sender, args) =>
            {
                //If the user pressed the button return it to previous page on the stack
                await Navigation.PopToRootAsync();
            };
        }

        void Init()
        {

            //Header styling
            Header.BackgroundColor = Constant.BackGroundColor;

            Lbl_Header.TextColor = Constant.TextColor;
            Lbl_Header.Margin = new Thickness(5, 5, 0, 10);
            Lbl_Header.HeightRequest = Constant.HeightRequest;

            Btn_Return.HorizontalOptions = LayoutOptions.EndAndExpand;

            //Footer styling
            Footer.BackgroundColor = Constant.BackGroundColor;

            Lbl_Cr.HorizontalOptions = LayoutOptions.Center;
            Lbl_Cr.VerticalOptions = LayoutOptions.EndAndExpand;
            Lbl_Cr.VerticalTextAlignment = TextAlignment.Center;
            Lbl_Cr.HeightRequest = 80;
            Lbl_Cr.TextColor = Constant.TextColor;
        }

        private async void Btn_incbalance_ClickedAsync(object sender, EventArgs e)
        {
            int balance = ApiCaller.UpdateUserBalance(PersonalUser.Id, Convert.ToInt32(Ent_IncBalance.Text), PersonalUser.Username, '+');
            await DisplayAlert("Succes", $"Balance increased by: {balance}", "Okay");
        }
    }
}