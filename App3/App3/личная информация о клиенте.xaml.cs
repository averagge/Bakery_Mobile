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
    public partial class Page8 : ContentPage
    {
        public Page8()
        {
            InitializeComponent();
        }
        private void ChangePhoneNumber_Clicked(object sender, EventArgs e)
        {
            // Реализация изменения номера телефона
        }

        // Обработчик нажатия кнопки изменения имени
        private void ChangeName_Clicked(object sender, EventArgs e)
        {
            // Переход на страницу для изменения имени (Page9)
            Navigation.PushAsync(new Page9());
        }

        // Обработчик нажатия кнопки выхода из аккаунта
        private void Logout_Clicked(object sender, EventArgs e)
        {
            // Реализация выхода из аккаунта
        }

        // Обработчик нажатия кнопки удаления аккаунта
        private void DeleteAccount_Clicked(object sender, EventArgs e)
        {
            // Реализация удаления аккаунта
        }
    }
}