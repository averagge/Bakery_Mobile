using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySqlConnector;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void GetCodeButton_Clicked(object sender, EventArgs e)
        {
            int id=0;
            MySqlConnection connection = new MySqlConnection(@"Server=192.168.113.194; Port=3306; Database=bakery; Uid=admin; Pwd=qwerty;");
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = $"select id from users where login='{login.Text}' and password='{Hash.GetHashString(password.Text)}'";
            command.Connection = connection;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = reader.GetInt32("id");
                }
            }
            connection.Close();
            if (id == 0)
            {
                DisplayAlert("Ошибка", "Неверный логин или пароль", "ок");
                return;
            }
            User.userid = id;
            Page5 page5 = new Page5(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page5);
        }
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry != null)
            {
                entry.Text = new string(entry.Text.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());
            }
        }
        private void OnPhoneEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry != null && entry.Text.Length > 11)
            {
                entry.Text = entry.Text.Remove(11);
            }
        }
        private void OnEntryTextChanged1(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry != null)
            {
                entry.Text = entry.Text.Replace(" ", "");
            }
        }
        private void ShareDataButton_Clicked(object sender, EventArgs e)
        {
            // Добавьте ваш код обработки события здесь
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            // Добавьте ваш код обработки события здесь
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Page2 page2 = new Page2();
            Navigation.PushModalAsync(page2);
        }
    }
}