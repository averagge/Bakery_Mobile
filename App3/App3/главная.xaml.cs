using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void AddressButton_Clicked(object sender, EventArgs e)
        {
            Page3 page3 = new Page3(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page3);
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            Page1 page1 = new Page1(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page1);
        }

    }
}
