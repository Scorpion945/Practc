using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {

        int count = 0;
        int columns = 0;
        int row = 0;
        int mm = 0;

        public Catalog()
        {
            InitializeComponent();

            var context = new AppDbContext();
            var q = context.categs.Count();
            var l = context.Basket.Where(x => x.Id > 0).ToList();
            int ss = l.Sum(x => Convert.ToInt32(x.kolvo));
            var W = context.categs.Where(x => x.Id > 0).ToList();
            while (count < q)
            {
                if (columns == 4)
                {
                    columns = 0;
                    row += 1;
                    //if (row == 2)
                    //{
                    //    break;
                    //}
                }

                // Создание стиля
                Style transparentButtonStyle = new Style(typeof(Button));
                transparentButtonStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.Transparent));
                transparentButtonStyle.Setters.Add(new Setter(BorderBrushProperty, Brushes.Transparent));
                transparentButtonStyle.Setters.Add(new Setter(ForegroundProperty, Brushes.White));
                // Создание изображения
                Image image = new Image();
                string imageUrl = W[count].image.ToString(); // Путь к изображению
                BitmapImage bitmap = new BitmapImage(new Uri(imageUrl, UriKind.RelativeOrAbsolute));
                image.Source = bitmap;
                image.Width = 100;
                image.Height = 100;
                //image.MouseLeftButtonUp += MyImage_MouseLeftButtonUp;

                // Создание кнопки

                TextBlock textBlock = new TextBlock();
                textBlock.Text = W[count].naim;
                textBlock.FontSize = 20;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.TextWrapping = TextWrapping.Wrap;

                Button button = new Button();
                button.Content = W[count].prise.ToString() + " руб.";
                button.Width = 150;
                button.Height = 35;
                button.FontSize = 20;
                button.Background = new SolidColorBrush(Colors.White);
                button.BorderThickness = new Thickness(4);
                button.BorderBrush = new SolidColorBrush(Colors.CornflowerBlue);
                button.VerticalAlignment = VerticalAlignment.Bottom;
                button.CommandParameter = imageUrl;
              //  button.Style = transparentButtonStyle;
                button.Click += new RoutedEventHandler(Button2_Click);

                Button opis = new Button();
              //opis.Content = "Описание";
                opis.Width = 100;
                opis.Height = 100;
                opis.FontSize = 20;
                opis.CommandParameter = imageUrl;
                opis.Opacity = 0.0;
                opis.Style = transparentButtonStyle;
                opis.Click += new RoutedEventHandler(Button3_Click);

                Button Info = new Button();
                Info.Template = (ControlTemplate)Resources["кнопка"];
                Info.CommandParameter = opis;
                Info.Click += Info_Click;
                korziina.Text = ss.ToString();
                




                // Отображение всплывающего окна с описанием товара

                // Добавление элементов на Grid
                Grid.SetRow(image, row);
                Grid.SetRow(textBlock, row);
                Grid.SetRow(button, row);
                Grid.SetRow(Info, row);
                Grid.SetRow(opis, row);


                Grid.SetColumn(image, columns);
                Grid.SetColumn(textBlock, columns);
                Grid.SetColumn(button, columns);
                Grid.SetColumn(opis, columns);

                Grid.SetRowSpan(Info, 2);

                // Добавление элементов на Grid
                myGrid.Children.Add(image);
                myGrid.Children.Add(textBlock);
                myGrid.Children.Add(button);
                myGrid.Children.Add(Info);
                myGrid.Children.Add(opis);


                columns++;
                count++;

            }
        }


        //описание товара через MessageBox 
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string imageUrl = btn.CommandParameter as string;

                var context = new AppDbContext();
                var product = context.categs.FirstOrDefault(x => x.image == imageUrl);

                if (product != null)
                {
                    
                    MessageBox.Show($"Описание товара:\n{product.opisanie}");
                }

            }
        }


        

        // кнопка добавления товара и прибавление числа в счётчике
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(korziina.Text, out int currentValue))
            {
                currentValue++;
                korziina.Text = currentValue.ToString();

            }

            Button button = sender as Button; 
            var context = new AppDbContext();
            string par = button.CommandParameter as string; 
            var q = context.categs.Where(x => x.image == par).ToList();
            var r = context.Basket.Where(x => x.image == par).ToList();
            if (r.Count > 0)
            {
                if (q[0].Id == r[0].Id)
                {
                    string cost = (Convert.ToInt32(r[0].kolvo) + 1).ToString();
                    var h = context.Basket.Where(x => x.Id == r[0].Id).AsEnumerable().Select(x => { x.kolvo = cost; return x; }); foreach (var x in h)
                    {
                        context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    context.SaveChanges();
                }
            }
            else
            {
                var tov = new Bask { image = q[0].image, naim = q[0].naim, kolvo = "1", prise = q[0].prise };
                context.Basket.Add(tov);
            }
            context.SaveChanges();
            var l = context.Basket.Where(x => x.Id > 0).ToList(); int ss = l.Sum(x => Convert.ToInt32(x.kolvo));
           //korziina.Text = ss.ToString();

        }

        public void Info_Click(object sender, RoutedEventArgs e)
        {

        }
       

        private void basket_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Baskets baskets = new Baskets();
            baskets.Show();
        }
    }
}

