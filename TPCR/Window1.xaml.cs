using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Newtonsoft.Json.Linq;

namespace TPCR
{
    public class PositionObject
    {
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public PositionType PositionType { get; set; }
        public int PositionValue { get; set; }
        public double PositionPrice { get; set; }
        public double PriceCurrency { get; set; }
    }

    public enum PositionType
    {
        Swimsuits = 0,
        UpperClothing = 1, // Верхняя одежда
        Pants = 2, // Низ
        TShirts = 3, // Майки
        Hats = 4 // Шапки
    }

    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int PositionID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public partial class Window1 : Window
    {
        DB dataBase = new DB();
        private CurrencyConverter converter;
        public Window1()
        {
            InitializeComponent();
            FillDataGrid();
            FillCustomerGrid();
            converter = new CurrencyConverter();
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            cbPosType.ItemsSource = Enum.GetValues(typeof(PositionType));
            cbPosType.SelectedIndex = 0;
        }

        private void FillDataGrid()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string queryProducts = "SELECT id AS PositionID, positionName AS PositionName, positionType AS PositionType, positionValue AS PositionValue, positionPrice AS PositionPrice, priceCurrency AS PriceCurrency FROM ProductsDB";

            try
            {
                dataBase.openConnection();
                using (SqlCommand commandProducts = new SqlCommand(queryProducts, dataBase.getConnection()))
                {
                    adapter.SelectCommand = commandProducts;
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при заполнении таблицы: " + ex.Message);
            }
            finally
            {
                dataBase.closeConnection();
            }

            dgMain.ItemsSource = table.DefaultView;
        }

        private void FillCustomerGrid()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string queryCustomers = "SELECT CustomerID, CustomerName, Email, Phone, Address FROM CustomersDB";

            try
            {
                dataBase.openConnection();
                using (SqlCommand commandCustomers = new SqlCommand(queryCustomers, dataBase.getConnection()))
                {
                    adapter.SelectCommand = commandCustomers;
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при заполнении таблицы клиентов: " + ex.Message);
            }
            finally
            {
                dataBase.closeConnection();
            }

            dgCustomers.ItemsSource = table.DefaultView;
        }

        private void bAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer();
            try
            {
                customer.CustomerName = tbCustomerName.Text;
                customer.Email = tbCustomerEmail.Text;
                customer.Phone = tbCustomerPhone.Text;
                customer.Address = tbCustomerAddress.Text;

                if (string.IsNullOrEmpty(customer.CustomerName) || string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Phone) || string.IsNullOrEmpty(customer.Address))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля для клиента.");
                    return;
                }

                dataBase.openConnection();
                string queryAddCustomer = "INSERT INTO CustomersDB (CustomerName, Email, Phone, Address) VALUES (@CustomerName, @Email, @Phone, @Address)";
                using (SqlCommand command = new SqlCommand(queryAddCustomer, dataBase.getConnection()))
                {
                    command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);
                    command.Parameters.AddWithValue("@Address", customer.Address);

                    command.ExecuteNonQuery();
                }
                FillCustomerGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении клиента: " + ex.Message);
            }
            finally
            {
                dataBase.closeConnection();
            }
        }

        private void bAddOrder_Click(object sender, RoutedEventArgs e)
        {
            int customerId;
            if (!int.TryParse(tbOrderCustomerID.Text, out customerId))
            {
                MessageBox.Show("Введите корректный ID клиента.");
                return;
            }

            try
            {
                dataBase.openConnection();
                string queryAddOrder = "INSERT INTO OrdersDB (CustomerID, OrderDate) VALUES (@CustomerID, @OrderDate)";
                using (SqlCommand command = new SqlCommand(queryAddOrder, dataBase.getConnection()))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerId);
                    command.Parameters.AddWithValue("@OrderDate", DateTime.Now);

                    command.ExecuteNonQuery();
                }
                FillOrderGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении заказа: " + ex.Message);
            }
            finally
            {
                dataBase.closeConnection();
            }
        }

        private void FillOrderGrid()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string queryOrders = "SELECT OrderID, CustomerID, OrderDate FROM OrdersDB";

            try
            {
                dataBase.openConnection();
                using (SqlCommand commandOrders = new SqlCommand(queryOrders, dataBase.getConnection()))
                {
                    adapter.SelectCommand = commandOrders;
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при заполнении таблицы заказов: " + ex.Message);
            }
            finally
            {
                dataBase.closeConnection();
            }

            dgOrders.ItemsSource = table.DefaultView;
        }

        private void bAddOrderItem_Click(object sender, RoutedEventArgs e)
        {
            PositionType selectedPositionType = (PositionType)cbPosType.SelectedItem;
            // Ваш код для добавления товара в заказ на основе выбранного типа позиции
        }

        private void dgMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ваш код для обработки выбора элемента в главной таблице
        }

        private void bDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgCustomers.SelectedItem;
                int customerId = Convert.ToInt32(row["CustomerID"]); 

                string deleteQuery = $"DELETE FROM CustomersDB WHERE CustomerID = {customerId}";

                DB db = new DB();
                db.ExecuteNonQuery(deleteQuery);

                MessageBox.Show("Клиент успешно удален");
                // Перезагрузите DataGrid dgCustomers для отображения обновленных данных
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента для удаления.");
            }
        }

        private void bDeleteOrderItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrderItems.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgOrderItems.SelectedItem;
                int productId = Convert.ToInt32(row["ProductID"]); 

                string deleteQuery = $"DELETE FROM ProductsDB WHERE ProductID = {productId}";

                DB db = new DB();
                db.ExecuteNonQuery(deleteQuery);

                MessageBox.Show("Товар успешно удален");
                // Перезагрузите DataGrid dgOrderItems для отображения обновленных данных
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар для удаления.");
            }
        }

        // Обработчик события для кнопки "Выход"
        private void bExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Закрыть текущее окно
        }

        private void bEvents_Click(object sender, RoutedEventArgs e)
        {
            EventWindow eventWindow = new EventWindow();
            eventWindow.Show();
        }
        private void bQuotes_Click(object sender, RoutedEventArgs e)
        {
            string quotes = converter.GetConversionRates();
            JObject obj = JObject.Parse(quotes);
            JObject valute = (JObject)obj["Valute"];
            string usd = valute["USD"]["Value"].ToString();
            string eur = valute["EUR"]["Value"].ToString();
            string cny = valute["CNY"]["Value"].ToString();
            // Очищаем ListBox перед добавлением новых котировок
            lbQuotes.Items.Clear();
            lbQuotes.Items.Add($"{usd} USD");
            lbQuotes.Items.Add($"{eur} EUR");
            lbQuotes.Items.Add($"{cny} CNY");

            lbQuotes.SelectedIndex = 0;

        }

        private void lbQuotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
