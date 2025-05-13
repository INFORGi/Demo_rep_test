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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace poject_md
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static demoEntities db;
        public MainWindow()
        {
            InitializeComponent();
            db = new demoEntities();
            SelectListView();
        }

        public void SelectListView()
        {
            try
            {
                var data = db.Products.ToList();

                listView.ItemsSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new Window1(null, this).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var product = listView.SelectedItem as Products;
                if (product == null) throw new Exception("Не выбран продукт");
                
                new Window1(product, this).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = listView.SelectedItem as Products;
                if (product == null) throw new Exception("Выбирите продукт");

                new Card(product, db).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Price_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = listView.SelectedItem as Products;
                if (product == null) throw new Exception("Выбирите продукт");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
