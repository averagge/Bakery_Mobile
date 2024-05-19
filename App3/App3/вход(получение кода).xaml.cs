using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        private string pattern1 = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public Page2()
        {
            InitializeComponent();
        }
        private void OnPhoneEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                // Очищаем введённые данные от всего, кроме цифр
                var newText = new string(e.NewTextValue.Where(char.IsDigit).ToArray());

                // Обновляем текст ввода
                ((Entry)sender).Text = newText;
            }
        }
        private void ContinueButton_Clicked(object sender, EventArgs e)
        {
            if (IsFormValid()== "Вы успешно зарегистрировались")
            {
                MySqlConnection connection = new MySqlConnection(@"Server=192.168.113.194; Port=3306; Database=bakery; Uid=admin; Pwd=qwerty;");
                connection.Open();
                MySqlCommand command1 = new MySqlCommand();
                command1.CommandText = $"select id from users where login='{UsernameEntry.Text}'";
                command1.Connection = connection;
                using (MySqlDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    { 
                        UsernameEntry.Text = "";
                        DisplayAlert("Ошибка", "Пользователь с таким логином уже существует", "ок");
                        connection.Close();
                        return;
                    }
                }
                MySqlCommand command = new MySqlCommand();
                command.CommandText = $"insert into users (login, password, name, surname, role, phone, email, address) values" +
                    $"('{UsernameEntry.Text}', '{Hash.GetHashString(PasswordEntry.Text)}', '{FirstNameEntry.Text}', '{LastNameEntry.Text}', " +
                    $"'Клиент', '{PhoneEntry.Text}', '{EmailEntry.Text}', '')";
                command.Connection = connection;
                command.ExecuteNonQuery();
                MySqlCommand command2 = new MySqlCommand();
                command2.CommandText = $"select max(id) from users;";
                command2.Connection = connection;   
                User.userid = Convert.ToInt32(command2.ExecuteScalar());
                connection.Close();
                DisplayAlert("Успешно", "Вы успешно зарегистрировались", "ок");
                Page5 page5 = new Page5(); // Создание экземпляра Page1
                Navigation.PushModalAsync(page5);
            }
            else
            {
                DisplayAlert("Ошибка", IsFormValid(), "ОК");
            }
        }
        private string IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text))
                return "Заполните поле 'Логин'";
            if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
                return "Заполните поле 'Пароль'";
            if (string.IsNullOrWhiteSpace(LastNameEntry.Text))
                return "Заполниите поле 'Фамлия'";
            if (string.IsNullOrWhiteSpace(FirstNameEntry.Text))
                return "Заполните поле 'Имя'";
            if (string.IsNullOrWhiteSpace(EmailEntry.Text) || !Regex.IsMatch(EmailEntry.Text, pattern1) || !EmailEntry.Text.Contains("@"))
                return "Недопустимый формат почты";
            if (string.IsNullOrWhiteSpace(PhoneEntry.Text))
                return "Недопустимый формат номера телефона";
            return "Вы успешно зарегистрировались";
        }
        
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry != null)
            {
                entry.Text = entry.Text.Replace(" ", "");
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Page1 page1 = new Page1(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page1);
        }
    }
}