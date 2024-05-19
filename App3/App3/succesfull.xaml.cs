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
    public partial class succesfull : ContentPage
    {
        public succesfull()
        {
            InitializeComponent();
            var scrollView = new ScrollView();
            var stackLayout = new StackLayout { Spacing = 20, Padding = new Thickness(20) };
            Label orderLabel = new Label
            {
                Text = "Ваши заказы",
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center
            };
            stackLayout.Children.Add(orderLabel);
            Button returnToMainButton = new Button
            {
                Text = "Вернуться на главную",
                BackgroundColor = Color.FromHex("#4EA429"),
                TextColor = Color.White,
                CornerRadius = 10,
                Margin = new Thickness(0, 20, 0, 0)
            };
            returnToMainButton.Clicked += ReturnToMainButton_Clicked;
            stackLayout.Children.Add(returnToMainButton);

            MySqlConnection connection = new MySqlConnection(@"Server=192.168.113.194; Port=3306; Database=bakery; Uid=admin; Pwd=qwerty;");
            connection.Open();
            MySqlCommand command1 = new MySqlCommand();
            command1.CommandText = $"select id, total from orders where users_id={User.userid}";
            command1.Connection = connection;
            List<UserOrder> orders = new List<UserOrder>();
            using (MySqlDataReader reader = command1.ExecuteReader())
            {
                while (reader.Read())
                {
                    int orderid = reader.GetInt32("id");
                    int total = reader.GetInt32("total");
                    UserOrder uo = new UserOrder(orderid, total);
                    orders.Add(uo);
                }
            }
            foreach (UserOrder item in orders)
            {
                var orderNumLabel = new Label { Text = $"Номер заказа: {item.Id}", FontSize = 20, TextColor = Color.Black };

                var stack = new StackLayout { Spacing = 5, Orientation = StackOrientation.Vertical };
                var orderframe = new Frame { Padding = new Thickness(10), BackgroundColor = Color.FromHex("#f0f0f0"), CornerRadius = 10 };
                stack.Children.Add(orderNumLabel);
                MySqlCommand command = new MySqlCommand();
                command.CommandText = $"select goods.name, order_composition.quantity from goods join order_composition on goods.id=order_composition.goods_id where order_composition.orders_id={item.Id}";
                command.Connection = connection;
                using (MySqlDataReader reader1 = command.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        var frame = new Frame { Padding = new Thickness(10), BackgroundColor = Color.FromHex("#f0f0f0"), CornerRadius = 10 };
                        string good = reader1.GetString(0);
                        int count = reader1.GetInt32(1);
                        var textstack = new StackLayout { Margin = new Thickness(5), Orientation=StackOrientation.Horizontal };
                        var nameLabel = new Label { Text = good, FontSize = 16, FontAttributes = FontAttributes.Bold };
                        var countLabel = new Label { Text = $"Количество: {count} шт.", FontSize = 14, TextColor = Color.Gray };
                        textstack.Children.Add(nameLabel);
                        textstack.Children.Add(countLabel);
                        frame.Content = textstack;
                        stack.Children.Add(frame);

                    }
                }


                var totalPriceLabel = new Label { Text = $"Итого: {item.Total} р", FontSize = 16, TextColor = Color.Black, FontAttributes = FontAttributes.Bold };
                stack.Children.Add(totalPriceLabel);
                orderframe.Content = stack;
                stackLayout.Children.Add(orderframe);



            }
            connection.Close();
            scrollView.Content = stackLayout;
            Content = scrollView;


            // Надпись "ВАШ ЗАКАЗ ПРИНЯТ"
            

            // Кнопка "Вернуться на главную"
            

        }
        private void ReturnToMainButton_Clicked(object sender, EventArgs e)
        {
            Page5 page5 = new Page5(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page5);
        }
    }
}