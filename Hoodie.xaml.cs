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

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для Hoodie.xaml
    /// </summary>
    public partial class Hoodie : Window
    {
        public Hoodie()
        {
            InitializeComponent();

            //Image myImage = new Image();
            //BitmapImage bitmapImage = new BitmapImage(new Uri(@"D:\project VS\Shop\Shop\images\adici.jpg", UriKind.RelativeOrAbsolute));
            //myImage.Source = bitmapImage;
            //myImage.Width = 170;
            //myImage.Height = 170;
            //myImage.VerticalAlignment = VerticalAlignment.Top;
            //myImage.HorizontalAlignment = HorizontalAlignment.Center;
            //Grid.SetRow(myImage, 1);
            //Grid.SetColumn(myImage, 0);
            //adic.Children.Add(myImage);
            ////myImage.MouseLeftButtonUp += MyImage_MouseLeftButtonUp;

            //TextBlock newTextBlock = new TextBlock();
            //newTextBlock.Text = $"Описание товара";
            //Grid.SetRow(newTextBlock, 2);
            //Grid.SetColumn(newTextBlock, 0);
            //newTextBlock.VerticalAlignment = VerticalAlignment.Center;
            //newTextBlock.TextAlignment = TextAlignment.Center;
            //newTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            //newTextBlock.FontSize = 70;
            //newTextBlock.FontWeight = FontWeights.Medium;
            //adic.Children.Add(newTextBlock);

            //TextBlock newTextBlock2 = new TextBlock();
            //newTextBlock2.Text = $"Мода 90-х остается в тренде. Почувствуйте яркую энергетику того десятилетия в кроссовках adidas Ozelia. Верх из текстиля\n " +
            //    $"и кожи с литыми вставками сочетает ретро-дизайн и футуристические линии. Амортизация Adiprene обеспечивает\n" +
            //    $"комфорт в течение всего дня, чтобы вы смогли демонстрировать свой модный стиль с утра и до самого вечера. Модель\n" +
            //    $"частично выполнена из переработанного материала, который был создан из отходов производства, например, обрезков\n" +
            //    $"ткани, а также из вторичных бытовых отходов с целью сокращения производства первичных тканей и снижения\n" +
            //    $"негативного воздействия на экологию.";
            //Grid.SetRow(newTextBlock2, 3);
            //Grid.SetColumn(newTextBlock2, 0);
            //newTextBlock2.VerticalAlignment = VerticalAlignment.Top;
            //newTextBlock2.TextAlignment = TextAlignment.Left;
            //newTextBlock2.HorizontalAlignment = HorizontalAlignment.Center;
            //newTextBlock.FontSize = 15;
            //adic.Children.Add(newTextBlock2);
        }

        private void basket_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Catalog catalog = new Catalog();
            catalog.Show();
        }
    }
}
