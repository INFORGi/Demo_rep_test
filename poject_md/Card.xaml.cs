using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace poject_md
{
    /// <summary>
    /// Логика взаимодействия для Card.xaml
    /// </summary>
    public partial class Card : Window
    {
        Products product;
        demoEntities db;
        public Card(Products product, demoEntities db)
        {
            InitializeComponent();
            this.db = db;
            LoadRouteMap(product);
        }

        private void LoadRouteMap(Products prod)
        {
            try
            {
                var routeMap = prod.Product_workshops.Where(pw => pw.id_product == prod.id).ToList();
                dataGrid.ItemsSource = routeMap;

                int total = routeMap.Sum(x => x.Workshops.count_people);
                totalPeople.Content = $"Всего человек: {total}";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
