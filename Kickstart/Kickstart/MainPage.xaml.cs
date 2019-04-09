using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Vibrate;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ZXing.Net.Mobile.Forms;
using Kickstart.models;

namespace Kickstart
{
    public partial class MainPage : ContentPage
    {
        //Global vars
        public Constant Constant { get; private set; } = new Constant();

        public MainPage()
        {
            //Remove the navigation bar for better view and replace it with the header
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            Init();

            //Check if the user pressed the back button
            Btn_Return.Clicked += async (sender, args) =>
            {
                //If the user pressed the button return it to previous page on the stack
                await Navigation.PopAsync(true);
            };
        }

        //Code styling and nesscary things that need to run before launch
        void Init()
        {
            //Header styling
            Header.BackgroundColor = Constant.BackGroundColor;

            Lbl_Header.TextColor = Constant.TextColor;
            Lbl_Header.Margin = new Thickness(5,5,0,10);
            Lbl_Header.HeightRequest = Constant.HeightRequest;

            Btn_Return.HorizontalOptions = LayoutOptions.EndAndExpand;

            //Content Screen Styling
            foreach (StackLayout item in Content.Children)
            {
                foreach (Button button in item.Children)
                {
                    button.BackgroundColor = Constant.ButtonBackGroundColor;
                    button.TextColor = Constant.TextColor;
                    button.WidthRequest = Constant.WidthRequest;
                    button.HeightRequest = Constant.HeightRequest;
                }
            }

            //Footer styling
            Footer.BackgroundColor = Constant.BackGroundColor;

            Lbl_Cr.HorizontalOptions = LayoutOptions.Center;
            Lbl_Cr.VerticalOptions = LayoutOptions.EndAndExpand;
            Lbl_Cr.VerticalTextAlignment = TextAlignment.Center;
            Lbl_Cr.HeightRequest = 80;
            Lbl_Cr.TextColor = Constant.TextColor;
        }

        private async void QrCode_Clicked(object sender, EventArgs e)
        {
            //Let the user know if they want to give access to the camera
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (status != PermissionStatus.Unknown)
            {
                //Await the request 
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Permission.Camera))
                {
                    status = results[Permission.Camera];
                }

                //Check if Permisson has been granted
                if (status == PermissionStatus.Granted)
                {
                }
                // If no permisson is deteced or denied Let the user know he cannot continue 
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Camera Denied", "Can not continue, try again.", "OK");
                    return;
                }
            }
            // Create our custom overlay
            var customLayout = new StackLayout { };
            var header_qr = new StackLayout
            {
                BackgroundColor = Constant.BackGroundColor,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                
            };
            var middleLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var footer_qr = new StackLayout
            {
                BackgroundColor = Constant.BackGroundColor,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End
            };
            var CopyRightLabel = new Label
            {
                Text = "Copyright © 2019, Kickstart",
                Margin = new Thickness(95, 0, 0, 0),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.EndAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HeightRequest = 75,
            };


            var CopyRightHeaderLabel = new Label
            {
                Text = "Logo Kickstart",
                Margin = new Thickness(95, 0, 0, 0),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.EndAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HeightRequest = 75,
            };

            // Give the header a text label for now
            header_qr.Children.Add(CopyRightHeaderLabel);
            // Give the footer the Copy Right Label
            footer_qr.Children.Add(CopyRightLabel);
            //Create the layout for the Scan page
            customLayout.Children.Add(header_qr);
            customLayout.Children.Add(middleLayout);
            customLayout.Children.Add(footer_qr);
            /// Make a new scanner page and add the layout
            var scan = new ZXingScannerPage(customOverlay: customLayout);
            NavigationPage.SetHasNavigationBar(scan, false);
            //Push the Scan page as first in the Navigation row
            await Navigation.PushAsync(scan);

            //Handel te qr code result
            scan.OnScanResult += (result) =>
            {
                // Start to sync the result of the qr scanner
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var vibrate = CrossVibrate.Current;
                    vibrate.Vibration(TimeSpan.FromSeconds(0.25));
                    await Navigation.PopAsync();
                    //Check iff the qrcode contains Kleyn
                    if (result.Text != "")
                    {
                        await DisplayAlert("Found", result.Text, "Well okay");
                    }
                    //If kleyn is not detected send a error back
                    else
                    {
                        await DisplayAlert("ERROR", "Not a vail qr code ", "OK");
                    }

                });
            };
        }
    }
}
