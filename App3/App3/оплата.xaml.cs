using MySqlConnector;
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
    public partial class Page14 : ContentPage
    {
        Entry deliveryAddressEntry;
        Label totalPriceLabel;

        public Page14()
        {
            InitializeComponent();
            InitializeDeliveryMethodPicker();
            InitializeDeliveryAddressEntry();
            InitializeTotalPriceLabel();
            UpdateTotalPrice(); 
        }

        private void InitializeDeliveryMethodPicker()
        {
            var deliveryMethodPicker = new Picker
            {
                Title = "Способ доставки",
                FontSize = 16
            };
            deliveryMethodPicker.Items.Add("Самовывоз");
            deliveryMethodPicker.Items.Add("Курьер");
            deliveryMethodPicker.SelectedIndex = 0; // По умолчанию выбран самовывоз

            deliveryMethodPicker.SelectedIndexChanged += (sender, args) =>
            {
                var selectedItem = deliveryMethodPicker.SelectedItem.ToString();
                if (selectedItem == "Самовывоз")
                {
                    // Если выбран самовывоз, отображаем фиксированный адрес
                    deliveryAddressEntry.IsEnabled = false;
                    deliveryAddressEntry.Text = "Большая Красная 55";
                }
                else
                {
                    // Если выбран курьер, разрешаем пользователю вводить свой адрес
                    deliveryAddressEntry.IsEnabled = true;
                    deliveryAddressEntry.Placeholder = "Введите адрес доставки";
                    deliveryAddressEntry.Text = ""; // Очищаем поле адреса
                }
            };

            deliveryMethodPickerLayout.Children.Add(deliveryMethodPicker);
        }

        private void InitializeDeliveryAddressEntry()
        {
            deliveryAddressEntry = new Entry
            {
                Placeholder = "Введите адрес доставки",
                IsEnabled = false, // По умолчанию поле недоступно для ввода
                Text = "Большая Красная 55" // Устанавливаем изначальный текст
            };
            deliveryAddressEntryLayout.Children.Add(deliveryAddressEntry);
        }

        private void InitializeTotalPriceLabel()
        {
            totalPriceLabel = new Label
            {
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black
            };

            // Добавляем метку в StackLayout внутри Frame
            totalPriceLayout.Children.Add(totalPriceLabel);
        }

        private void UpdateTotalPrice()
        {
            decimal totalAmount = CalculateTotalAmount(); // Рассчитываем общую сумму
            totalPriceLabel.Text = $"Итого: {totalAmount} р"; // Обновляем метку с общей ценой
        }

        private decimal CalculateTotalAmount()
        {
            decimal total = 0;
            foreach (var item in Cart.goods)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }

        private async void ReturnToMainButton_Clicked(object sender, EventArgs e)
        {
        
            // Проверяем, выбран ли способ доставки "Курьер" и заполнено ли поле адреса доставки
            if (deliveryAddressEntry.IsEnabled && string.IsNullOrEmpty(deliveryAddressEntry.Text))
            {
                await DisplayAlert("Ошибка", "Введите адрес доставки", "OK");
                return;
            }

            // Проверяем, заполнено ли поле с номером карты
            if (string.IsNullOrEmpty(PaymentMethodEntry.Text) || PaymentMethodEntry.Text.Length!=16)
            {
                await DisplayAlert("Ошибка", "Введите номер карты", "OK");
                return;
            }

            MySqlConnection connection = new MySqlConnection(@"Server=192.168.113.194; Port=3306; Database=bakery; Uid=admin; Pwd=qwerty;");
            connection.Open();
            MySqlCommand command1 = new MySqlCommand();
            command1.CommandText = $"insert into orders (address, status, total, users_id) values ('{deliveryAddressEntry.Text}', 'В сборке', {CalculateTotalAmount()}," +
                $" {User.userid})";
            command1.Connection = connection;
            command1.ExecuteNonQuery();
            MySqlCommand command2 = new MySqlCommand();
            command2.CommandText = $"select max(id) from orders";
            command2.Connection = connection;
            Order.orderid = Convert.ToInt32(command2.ExecuteScalar());
            MySqlCommand command = new MySqlCommand();
            foreach(Good good in Cart.goods)
            {
                command.CommandText = $"insert into order_composition (quantity, goods_id, orders_id) values ({good.Quantity}, " +
                    $"{good.Id}, {Order.orderid})";
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            connection.Close();
            // Используйте Page14 вместо Page5
            await Navigation.PushModalAsync(new succesfull());
            
        }


    }
}