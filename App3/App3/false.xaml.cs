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
    public partial class _false : ContentPage
    {
        public _false()
        {
            InitializeComponent();
        }
        private void ReturnToMainButton_Clicked(object sender, EventArgs e)
        {
            Page5 page5 = new Page5(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page5);
        }
    }
}