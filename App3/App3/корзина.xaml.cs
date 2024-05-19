using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page12 : ContentPage
    {
        public void RefreshCartUI()
        {
            decimal CalculateTotalAmount()
            {
                decimal total = 0;
                foreach (var item in Cart.goods)
                {
                    total += item.Price * item.Quantity;
                }
                return total;
            }
            var scrollView = new ScrollView();
            var stackLayout = new StackLayout { Spacing = 20, Padding = new Thickness(20) };

            decimal totalAmount = CalculateTotalAmount(); // Переменная для хранения общей суммы

            var totalPriceLabel = new Label { Text = $"Итого: {totalAmount} р", FontSize = 20, TextColor = Color.Black, FontAttributes = FontAttributes.Bold };

            foreach (var item in Cart.goods)
            {
                var frame = new Frame { Padding = new Thickness(10), BackgroundColor = Color.FromHex("#f0f0f0"), CornerRadius = 10 };
                var stack = new StackLayout { Spacing = 5, Orientation = StackOrientation.Horizontal };

                var textstack = new StackLayout { Margin = new Thickness(5) };
                var nameLabel = new Label { Text = item.Name, FontSize = 16, FontAttributes = FontAttributes.Bold };
                var weightLabel = new Label { Text = $"Вес: {item.Weight} г.", FontSize = 14, TextColor = Color.Gray };
                var pricelabel = new Label { Text = $"{item.Price} руб.", FontSize = 14, TextColor = Color.FromHex("#4EA429") };

                textstack.Children.Add(nameLabel);
                textstack.Children.Add(weightLabel);
                textstack.Children.Add(pricelabel);

                var quantityEntry = new Xamarin.Forms.Entry
                {
                    Text = item.Quantity.ToString(),
                    Placeholder = "Количество",
                    Keyboard = Keyboard.Numeric,
                    WidthRequest = 80
                };
                totalAmount += item.Price * item.Quantity;

                // Добавляем обработчик события изменения текста
                quantityEntry.TextChanged += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.NewTextValue))
                    {
                        // Проверяем, что новое значение в пределах от 1 до 20
                        int newQuantity = Math.Max(1, Math.Min(20, int.Parse(e.NewTextValue)));
                        item.Quantity = newQuantity; // Обновляем количество товара
                        quantityEntry.Text = newQuantity.ToString(); // Обновляем значение текстового поля
                        totalAmount = CalculateTotalAmount(); // Пересчитываем общую сумму
                        totalPriceLabel.Text = $"Итого: {totalAmount} р"; // Обновляем метку с общей ценой
                    }
                };

                var deleteButton = new Xamarin.Forms.Button { Text = "Удалить", TextColor = Color.Red, FontSize = 14, WidthRequest = 80, BackgroundColor = Color.Transparent };
                deleteButton.Clicked += (sender, e) =>
                {
                    // Удаляем товар из корзины по индексу
                    Cart.goods.Remove(item);
                    // Перерисовываем интерфейс корзины
                    RefreshCartUI();
                };

                stack.Children.Add(textstack);
                stack.Children.Add(quantityEntry);
                stack.Children.Add(deleteButton);

                frame.Content = stack;
                stackLayout.Children.Add(frame);

                // Добавляем стоимость каждого товара к общей сумме
            }

            /*var paymentMethodPicker = new Picker { Title = "Выберите метод оплаты", FontSize = 16 };
            paymentMethodPicker.Items.Add("Карта ****0006");
            paymentMethodPicker.Items.Add("Наличные");
            paymentMethodPicker.SelectedIndex = 0;*/


            var checkoutButton = new Xamarin.Forms.Button { Text = "Оформить заказ", BackgroundColor = Color.FromHex("#4EA429"), TextColor = Color.White, FontSize = 18, CornerRadius = 10, HeightRequest = 50 };
            checkoutButton.Clicked += ContinueButton_Clicked;


/*            stackLayout.Children.Add(paymentMethodPicker);
*/            stackLayout.Children.Add(totalPriceLabel);
            stackLayout.Children.Add(checkoutButton);

            scrollView.Content = stackLayout;
            Content = scrollView;

            // Функция для пересчета общей суммы

        }


        public Page12()
        {
            InitializeComponent();
            RefreshCartUI();
            
        }

        private void ContinueButton_Clicked(object sender, EventArgs e)
        {

            foreach (var item in Cart.goods.ToList())
            {
                // Проверяем, если количество товара стало равным 0, удаляем его из корзины
                if (item.Quantity == 0)
                {
                    Cart.goods.Remove(item);
                }
            }

            // Далее можно выполнить другие действия, например, переход на страницу оформления заказа или обновление информации о корзине


            Page14 page14 = new Page14(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page14);
        }
    }
}