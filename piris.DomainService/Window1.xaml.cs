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
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
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


    public partial class Window1 : Window
    {
        DB dataBase = new DB();
        private CurrencyConverter converter;
        public Window1()
        {
            InitializeComponent();
            FillDataGrid();
            converter = new CurrencyConverter();

        }

        private void lbQuotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            // Получите все значения перечисления PositionType
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

            // Устанавливаем источник данных для DataGrid
            dgMain.ItemsSource = table.DefaultView;
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


            // Происходит запрос к серверу на сайт ЦБ, должна добавить в lbQuotes последнии котировки валют(USD, EUR,CNY)
            //lbQuotes.Items.Clear();
            //lbQuotes.Items.Add("75,47 USD");
            //lbQuotes.Items.Add("80,24 EUR");
            //lbQuotes.Items.Add("10,88 CNY");
            //lbQuotes.SelectedIndex = 0;
        }
        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового объекта PositionObject
            PositionObject item = new PositionObject();
            try
            {
                // Инициализация свойств объекта
                item.PositionName = tbPosName.Text;

                // Извлечение выбранного элемента ComboBox и его значения Tag
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)cbPosType.SelectedItem;
                if (selectedComboBoxItem != null)
                {
                    // Извлечение названия из Tag выбранного элемента ComboBoxItem
                    item.PositionType = (PositionType)Enum.Parse(typeof(PositionType), selectedComboBoxItem.Tag.ToString());
                }

                item.PositionValue = int.Parse(tbPosValue.Text);
                item.PositionPrice = double.Parse(tbPosPrice.Text);
                // Проверка наличия пустых данных
                if (string.IsNullOrEmpty(item.PositionName) || item.PositionValue == 0 || item.PositionPrice == 0)
                {
                    MessageBox.Show("Ошибка!" + '\n' + "Есть пустые данные!");
                    return;
                }
                // Если данные не пустые, продолжаем выполнение кода
                item.PositionID = dgMain.Items.Count + 1;
                // Дополнительная проверка, прежде чем использовать lbQuotes.SelectedItem;
                if (lbQuotes.SelectedItem != null)
                {
                    string[] strings = lbQuotes.SelectedItem.ToString().Split(' ');
                    item.PriceCurrency = Math.Round(double.Parse(tbPosPrice.Text) / double.Parse(strings[0]), 2);
                }
                // Добавление элемента в DataGrid
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                // Параметризованный запрос SQL для вставки данных в базу данных
                string insertQuery = "INSERT INTO ProductsDB (positionName, positionType, positionValue, positionPrice, priceCurrency) VALUES " +
                                     "(@PositionName, @PositionType, @PositionValue, @PositionPrice, 0)";


                try
                {
                    dataBase.openConnection();
                    using (SqlCommand command = new SqlCommand(insertQuery, dataBase.getConnection()))
                    {
                        // Добавление параметров
                        command.Parameters.AddWithValue("@PositionName", item.PositionName);
                        command.Parameters.AddWithValue("@PositionType", item.PositionType);
                        command.Parameters.AddWithValue("@PositionValue", item.PositionValue);
                        command.Parameters.AddWithValue("@PositionPrice", item.PositionPrice);

                        // Выполнение запроса
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при добавлении элемента: " + ex.Message);
                }
                finally
                {
                    dataBase.closeConnection();
                }


            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка!" + '\n' + "Некорректный формат числа или пустое поле ввода.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }

            FillDataGrid();
        }





        private void dgMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что выбран какой-то элемент
            if (dgMain.SelectedItem != null)
            {
                bDelete.IsEnabled = true;
            }
            else
            {
                bDelete.IsEnabled = false;
            }
        }

        private void cbPosType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            if (selectedItem != null)
            {
                // Извлекаем значение Tag, которое установлено для выбранного элемента ComboBoxItem
                string tagValue = selectedItem.Tag.ToString();
                // Преобразуем его в целое число и находим соответствующее перечисление PositionType
                PositionType selectedType = (PositionType)Enum.Parse(typeof(PositionType), tagValue);
                // Выполняем необходимые действия с выбранным PositionType
            }
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgMain.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgMain.SelectedItem;
                int positionID = (int)row["PositionID"];

                string deleteQuery = "DELETE FROM ProductsDB WHERE id = @PositionID";
                try
                {
                    dataBase.openConnection();
                    using (SqlCommand command = new SqlCommand(deleteQuery, dataBase.getConnection()))
                    {
                        command.Parameters.AddWithValue("@PositionID", positionID);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при удалении: " + ex.Message);
                }
                finally
                {
                    dataBase.closeConnection();
                }

                // Обновляем DataGrid после удаления
                FillDataGrid();
            }
        }


        private void bExit_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новое окно MainWindow
            MainWindow mainWindow = new MainWindow();

            // Открываем MainWindow
            mainWindow.Show();

            // Закрываем текущее окно Window1
            this.Close();
        }

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void bMarkup_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}