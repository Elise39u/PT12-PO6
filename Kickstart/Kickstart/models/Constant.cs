using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Kickstart.models
{
    public class Constant
    {
        public static Color ContentLayout { get; } = Color.FromHex("#102E40");
        public static Color BackGroundColor { get; } = Color.FromHex("#068587");
        public static Color TextColor { get; } = Color.White;
        public static Color ErrorColor { get; } = Color.FromHex("#b20000");

        public static Color ButtonBackGroundColor { get; } = Color.FromHex("#EC553B");

        public static int WidthRequest { get; } = 40;
        public static int HeightRequest { get; } = 40;
    }
}
