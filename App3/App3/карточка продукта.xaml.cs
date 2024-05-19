using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page6 : ContentPage
    {
        private int id;
        public Page6(int id)
        {
            this.id = id;
/*            InitializeComponent();
*/

            MySqlConnection connection = new MySqlConnection(@"Server=192.168.113.194; Port=3306; Database=bakery; Uid=admin; Pwd=qwerty;");
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = $"select name, price, image, weight from goods where id={id}";
            command.Connection = connection;
            
            string name="";
            int price=0;
            string img="";
            int weight=0;
            List<string> ing = new List<string>();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    name = reader.GetString("name");
                    price = reader.GetInt32("price");
                    img = reader.GetString("image");
                    weight = reader.GetInt32("weight");
                }
            }
            MySqlCommand command1 = new MySqlCommand();
            command1.CommandText = $"select c.name from components c join (select components_id from composition where goods_id={id}) as comp on c.id=comp.components_id;";
            command1.Connection = connection;
            using (MySqlDataReader reader = command1.ExecuteReader())
            {
                while (reader.Read())
                {
                    ing.Add(reader.GetString("name"));  
                }
            }
            connection.Close();
            var scrollView = new ScrollView();
            var stackLayout = new StackLayout { Padding = new Thickness(20) };


            var image = new Image { Source = img.Split('/')[1], Aspect = Aspect.AspectFill, HeightRequest = 200 };
            var nameLabel = new Label { Text = name, FontSize = 20, FontAttributes = FontAttributes.Bold, TextColor = Color.Black, HorizontalOptions = LayoutOptions.Center };
            var weightLabel = new Label { Text = $"{weight} г.", FontSize = 16, TextColor = Color.Black, HorizontalOptions = LayoutOptions.Center };


            var ingredientsLabel = new Label { Text = "Состав:", FontSize = 18, FontAttributes = FontAttributes.Bold, TextColor = Color.Black };
            var ingredientsDescriptionLabel = new Label { FontSize = 16, TextColor = Color.Black };
            foreach(var item in ing)
            {
                ingredientsDescriptionLabel.Text += item + ", ";
            }
            ingredientsDescriptionLabel.Text += " ";
            ingredientsDescriptionLabel.Text = ingredientsDescriptionLabel.Text.Trim().Trim(',');

            var addToCartButton = new Xamarin.Forms.Button { Text = "Добавить товар", BackgroundColor = Color.FromHex("#FFA500"), TextColor = Color.White, CornerRadius = 10, HorizontalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 20, 0, 0), ClassId=id.ToString() };
            addToCartButton.Clicked += AddToCartButton_Clicked;

            StackLayout btnStack = new StackLayout { Orientation = StackOrientation.Horizontal };
            var button1 = new Xamarin.Forms.Button { Text = "Корзина", BackgroundColor = Color.FromHex("#4EA429"), TextColor = Color.White, CornerRadius = 10, HorizontalOptions = LayoutOptions.End, Margin = new Thickness(0, 20, 0, 0) };
            button1.Clicked += CartButton_Clicked;
            var button2 = new Xamarin.Forms.Button { Text = "Заказы", BackgroundColor = Color.FromHex("#4EA429"), TextColor = Color.White, CornerRadius = 10, HorizontalOptions = LayoutOptions.End, Margin = new Thickness(0, 20, 0, 0) };
            button2.Clicked += Button_Clicked_1;
            btnStack.Children.Add(button1);
            btnStack.Children.Add(button2);
            stackLayout.Children.Add(btnStack);
            stackLayout.Children.Add(image);
            stackLayout.Children.Add(nameLabel);
            stackLayout.Children.Add(weightLabel);
            stackLayout.Children.Add(ingredientsLabel);
            stackLayout.Children.Add(ingredientsDescriptionLabel);
            stackLayout.Children.Add(addToCartButton);


            scrollView.Content = stackLayout;

            Content = scrollView;
        }
        public Page6()
        {
            InitializeComponent();
        }

        private void AddToCartButton_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Xamarin.Forms.Button;
            MySqlConnection connection = new MySqlConnection(@"Server=192.168.113.194; Port=3306; Database=bakery; Uid=admin; Pwd=qwerty;");
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = $"select name, price, image, weight from goods where id={id}";
            command.Connection = connection;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Good good = new Good
                    {
                        Id = id,
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

        public static implicit operator Page6(Page7 v)
        {
            throw new NotImplementedException();
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            succesfull page = new succesfull(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page);
        }
        private void CartButton_Clicked(object sender, EventArgs e)
        {
            Page12 page12 = new Page12(); // Создание экземпляра Page1
            Navigation.PushModalAsync(page12);
        }
    }
}