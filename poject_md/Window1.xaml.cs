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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace poject_md
{
    /// <summary>
    /// Логика взаимодействия для Window.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        demoEntities db;
        MainWindow mainWindow;
        public Window1(Products product, MainWindow main)
        {
            InitializeComponent();
            db = MainWindow.db;
            mainWindow = main;

            SelectData(product);
        }

        /// <summary>
        /// Заполняем поля
        /// </summary>
        /// <param name="products"></param>
        private void SelectData(Products products)
        {
            try
            {
                var materialTypes = db.Material_type.ToList();
                var productTypes = db.Product_type.ToList();

                typeMaterial.ItemsSource = materialTypes;
                typeProduct.ItemsSource = productTypes;

                if (products == null)
                {
                    this.Title = "Добавление продукта";
                    var newProduct = new Products();
                    newProduct.Material_type = materialTypes.FirstOrDefault();
                    newProduct.Product_type = productTypes.FirstOrDefault();
                    this.DataContext = newProduct;
                }
                else
                {
                    this.Title = "Редактирование продукта";
                    this.DataContext = products;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Сохраняем запись или создаем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (articul.Text == "" || name.Text == "" || min_cost.Text == ""|| int.Parse(min_cost.Text) < 0 || typeProduct.Items == null || typeMaterial.Items == null)
                    throw new Exception("Заполните все поля, поле минемальная стоимость не должна быть отрицательной, 2 занака после запятой.");

                var item = this.DataContext as Products;
                item.min_cost_seller.ToString("F2");
                if (item != null)
                {
                    var existing = db.Products.FirstOrDefault(x => x.id == item.id);
                    if (existing != null)
                    {
                        db.Entry(existing).CurrentValues.SetValues(item);
                        db.SaveChanges();
                        mainWindow.SelectListView();
                        MessageBox.Show("Изменения успешно сохранены", "Информация",MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        db.Products.Add(item);
                        db.SaveChanges();
                        mainWindow.SelectListView();
                        MessageBox.Show("Запись создана", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
