using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page13 : ContentPage
    {
        public Page13()
        {
            InitializeComponent();
        }
        private void OrderButton_Clicked(object sender, EventArgs e)
        {
            Page14 page14 = new Page14(); // Используйте Page14 вместо Page5
            Navigation.PushModalAsync(page14);
        }
        //private void ProfileButton_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new Page7());
        //}
        private void Frame_Tapped(object sender, EventArgs e)
        {
            Page6 page6 = new Page6(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page6);
        }
    }
}