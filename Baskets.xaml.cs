using Microsoft.VisualBasic.ApplicationServices;
using Shop;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для Baskets.xaml
    /// </summary>
    public partial class Baskets : Window
    {
        int count = 0; 
        int columns = 0;
        int row = 0;
        public Baskets()
        {
            InitializeComponent();


            var contex = new AppDbContext();
            var q = contex.Basket.Count(); var l = contex.Basket.Where(x => x.Id > 0).ToList();
            int ss = l.Sum(x => Convert.ToInt32(x.kolvo));
            var w = contex.Basket.Where(x => x.Id > 0).ToList();

            var summa = contex.Basket.Select(x => x.prise).ToList();
            var sum = summa.Sum();
            tb.Text = "Сумма заказа: " + sum + " руб";

            while (count < q)
            {
                if (columns == 5)
                {
                    columns = 0; row += 1;
                    //if (row == 2)                    //{
                    //    break;                    //}
                }
                Image image = new Image();
                string a = w[count].image.ToString(); 
                image.Source = new BitmapImage(new Uri($"{a}", UriKind.RelativeOrAbsolute));
                image.Width = 100;
                image.Height = 100;

                TextBlock textBlock = new TextBlock(); textBlock.Text = w[count].naim;
                textBlock.FontSize = 25;
                textBlock.TextWrapping = TextWrapping.Wrap;

                Button button1 = new Button();
                button1.Content = w[count].prise.ToString() + " руб.";
                button1.Width = 150;
                button1.Height = 35;
                button1.Background = new SolidColorBrush(Colors.White);
                button1.BorderThickness = new Thickness(4);
                button1.BorderBrush = new SolidColorBrush(Colors.CornflowerBlue);
                button1.Template = (ControlTemplate)Resources["овальная кнопка"];
                button1.CommandParameter = a;
                button1.Click += Button_Click;

                Grid.SetColumn(image, columns + 0);
                Grid.SetRow(image, row + 1); 
                Grid.SetColumn(textBlock, columns + 1);
                Grid.SetRow(textBlock, row + 1); 
                Grid.SetColumn(button1, columns + 3);
                Grid.SetRow(button1, row + 7);

                bask.Children.Add(image);
                bask.Children.Add(textBlock); 
                bask.Children.Add(button1);
                row++;
                count++;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button; var context = new AppDbContext();
            string par = button.CommandParameter as string; var q = context.categs.Where(x => x.image == par).ToList();
            var r = context.Basket.Where(x => x.image == par).ToList(); if (r.Count > 0)
            {
                if (q[0].Id == r[0].Id)
                {
                    string price = (Convert.ToInt32(r[0].kolvo) + 1).ToString(); var h = context.Basket.Where(x => x.Id == r[0].Id).AsEnumerable().Select(x => { x.kolvo = price; return x; });
                    foreach (var x in h)
                    {
                        context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    context.SaveChanges();
                }
            }
            else
            {
                var tov = new Bask { Id = q[0].Id, image = q[0].image, naim = q[0].naim, kolvo = "1", prise = q[0].prise };
                context.Basket.Add(tov);
            }
            context.SaveChanges();
            var l = context.Basket.Where(x => x.Id > 0).ToList(); int ss = l.Sum(x => Convert.ToInt32(x.kolvo));
           
        }

        private void basket_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Catalog catalog = new Catalog();
            catalog.Show();
        }

        private void zakazat_Click(object sender, RoutedEventArgs e)
        {
            using (AppDbContext context = new AppDbContext())
            {
                context.Basket.RemoveRange(context.Basket);
                context.SaveChanges();
            }
            MessageBox.Show("Спасибо за покупку!");
        }
    }
}