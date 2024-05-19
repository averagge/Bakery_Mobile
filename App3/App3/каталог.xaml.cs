using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page5 : ContentPage
    {
        private int id;
        
        public Page5()
        {
            InitializeComponent();
            var superstack = new StackLayout();
            var scrollViewmain = new ScrollView();
            var stackLayoutmain = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20)
            };
            var button1 = new Button { Text = "Корзина", BackgroundColor = Color.FromHex("#4EA429"), TextColor = Color.White, CornerRadius = 10, HorizontalOptions = LayoutOptions.End, Margin = new Thickness(0, 20, 0, 0) };
            button1.Clicked += CartButton_Clicked;
            var button2 = new Button { Text = "Заказы", BackgroundColor = Color.FromHex("#4EA429"), TextColor = Color.White, CornerRadius = 10, HorizontalOptions = LayoutOptions.End, Margin = new Thickness(0, 20, 0, 0) };
            button2.Clicked += Button_Clicked_1;

            var scrollView = new ScrollView { VerticalOptions = LayoutOptions.FillAndExpand };

            var stackLayout = new StackLayout { Spacing = 20, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

            MySqlConnection connection = new MySqlConnection(@"Server=192.168.113.194; Port=3306; Database=bakery; Uid=admin; Pwd=qwerty;");
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = $"select name, price, image, weight, id from goods";
            command.Connection = connection;    
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader.GetString("name");
                    int price = reader.GetInt32("price");
                    string img = reader.GetString("image");
                    int weight = reader.GetInt32("weight");
                    int goodid = reader.GetInt32("id");

                    var frame = new Frame { CornerRadius = 10, Padding = 10, BackgroundColor = Color.FromHex("#EEEEEE"), ClassId=goodid.ToString()};
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += Frame_Tapped;
                    frame.GestureRecognizers.Add(tapGestureRecognizer);
                    var innerStackLayout = new StackLayout();
                    var image = new Image { Source = img.Split('/')[1], Aspect = Aspect.AspectFill };
                    var idLabel = new Label { Text = goodid.ToString(), FontSize = 1, TextColor = Color.White };
                    var nameLabel = new Label { Text = name, FontSize = 18 };
                    var weightLabel = new Label { Text = $"Вес: {weight} г.", FontSize = 16 };
                    var button = new Button { Text = $"Цена: {price} руб.", BackgroundColor = Color.FromHex("#4EA429"), TextColor = Color.White, CornerRadius = 10, ClassId = goodid.ToString() };
                    button.Clicked += Button_Clicked;
                    innerStackLayout.Children.Add(image);
                    innerStackLayout.Children.Add(nameLabel);
                    innerStackLayout.Children.Add(weightLabel);
                    innerStackLayout.Children.Add(button);
                    frame.Content = innerStackLayout;
                    stackLayout.Children.Add(frame);
                }
            }
            connection.Close();

            scrollView.Content = stackLayout;
            StackLayout btnStack = new StackLayout { Orientation=StackOrientation.Horizontal };
            btnStack.Children.Add(button1);
            btnStack.Children.Add(button2);
            stackLayoutmain.Children.Add(scrollView);



            scrollViewmain.Content = stackLayoutmain;
            superstack.Children.Add(btnStack);
            superstack.Children.Add(scrollViewmain);
            Content = superstack;
        }
        
        //private void ProfileButton_Clicked(object sender, EventArgs e)
        //{
        //    Page7 page7 = new Page7(); // Создание экземпляра Page1
        //    Navigation.PushModalAsync(page7);
        //}
        private void CartButton_Clicked(object sender, EventArgs e)
        {
            Page12 page12 = new Page12(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page12);
        }

        private void Frame_Tapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            int goodid = Convert.ToInt32(frame.ClassId);
            frame.Content = null;
            Page6 page6 = new Page6(goodid); // Создание экземпляра Page1
            Navigation.PushModalAsync(page6);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            MySqlConnection connection = new MySqlConnection(@"Server=192.168.113.194; Port=3306; Database=bakery; Uid=admin; Pwd=qwerty;");
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = $"select id, name, price, image, weight from goods where id={Convert.ToInt32(btn.ClassId)}";
            command.Connection = connection;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Good good = new Good
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name"),
                        Price = reader.GetInt32("price"),
                        Image = reader.GetString("image"),
                        Weight = reader.GetInt32("weight")

                    };
                    if (Cart.Contains(good.Id))
                    {
                        DisplayAlert("Уведомление", "Товар уже в корзине", "ок");

                    }
                    else
                    {
                        Cart.goods.Add(good);
                        DisplayAlert("Уведомление", "Товар добавлен в корзину", "ок");

                    }

                }
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            succesfull page = new succesfull(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page);
        }
    }
}
