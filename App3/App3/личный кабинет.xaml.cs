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
    public partial class Page7 : ContentPage
    {
        public Page7()
        {
            InitializeComponent();
        }
        private void ContactUsButton_Clicked(object sender, EventArgs e)
        {

        }
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            string selectedItem = e.Item.ToString();

            switch (selectedItem)
            {
                case "ЗАКАЗЫ":
                    Page12 page12 = new Page12(); // Используйте Page14 вместо Page5
                    Navigation.PushModalAsync(page12);

                    break;
                case "АДРЕСА":
                    Page10 page10 = new Page10(); // Используйте Page14 вместо Page5
                    Navigation.PushModalAsync(page10);

                    break;
                case "НАСТРОЙКИ":
                    Page8 page8 = new Page8(); // Используйте Page14 вместо Page5
                    Navigation.PushModalAsync(page8);

                    break;
                case "СПОСОБЫ ОПЛАТЫ":
                    Page11 page11 = new Page11(); // Используйте Page14 вместо Page5
                    Navigation.PushModalAsync(page11);
                    break;
                default:
                    break;
            }

           //((ListView)sender).SelectedItem = null; // Сброс выбранного элемента списка
        }


    }
}
