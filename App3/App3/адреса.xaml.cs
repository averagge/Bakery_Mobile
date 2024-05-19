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
    public partial class Page3 : ContentPage
    {
        public Page3()
        {
            InitializeComponent();
        }
        private void NextButton_Clicked(object sender, EventArgs e)
        {
            Page4 page4 = new Page4(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page4);
        }
    }
}