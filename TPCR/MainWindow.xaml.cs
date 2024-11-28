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
using System.Data.SqlClient;
using System.Data;

namespace TPCR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool Choised = false;
        Window1 workWindow = new Window1();
        DB dataBase = new DB();
       

        public MainWindow()
        {
            
            InitializeComponent();


        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            tbLogin.MaxLength = 50;
            tbPass1.MaxLength = 50;
            tbPass2.MaxLength = 50;
        }
        private void bLogin_Click(object sender, RoutedEventArgs e)
        {
           
            Choised = true;
            tbLogin.IsEnabled = true;
            tbPass1.IsEnabled = true;
            tbPass2.IsEnabled = false;
            bEnter.IsEnabled = true;
        }

        private void bRegistration_Click(object sender, RoutedEventArgs e)
        {
            Choised = false;
            tbLogin.IsEnabled = true;
            tbPass1.IsEnabled = true;
            tbPass2.IsEnabled = true;
            bEnter.IsEnabled = true;
        }
        private void Registration(string tbL, string tbP1, string tbP2)
        {
            if (tbL == "")
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (tbP1 == "")
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            if (tbP2 == "")
            {
                MessageBox.Show("Повторите пароль");
                return;
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string queryFind = $"select id, userName, userPassword from UsersDB where userName = '{tbL}'";

            SqlCommand commandFind = new SqlCommand(queryFind, dataBase.getConnection());
            adapter.SelectCommand = commandFind;
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                if (tbP1 == tbP2)
                {
                    string queryRegister = $"insert into UsersDB(userName, userPassword) values('{tbL}', '{tbP1}')";
                    SqlCommand commandRegister = new SqlCommand(queryRegister, dataBase.getConnection());
                    adapter.SelectCommand = commandRegister;
                    dataBase.openConnection();
                    if (commandRegister.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Аккаунт успешно создан", "Успех");
                    }

                    
                    workWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Пароли различны");
                    return;
                }

            }
            else
            {
                MessageBox.Show("Такой аккаунт уже существует", "Аккаунта существует", MessageBoxButton.OK);
            }
           
        }
        private void Logining(string tbL, string tbP1)
        {
            if (tbL == "")
            {
                MessageBox.Show("Введите логин");
                return;
            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string queryLogin = $"select id, userName, userPassword from UsersDB where userName = '{tbL}' and userPassword='{tbP1}'";

            SqlCommand commandLogin = new SqlCommand(queryLogin, dataBase.getConnection());
            adapter.SelectCommand = commandLogin;
            adapter.Fill(table);
            
            if(table.Rows.Count==1)
            {
                
                workWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует", "Аккаунта не существует", MessageBoxButton.OK);
            }

           
        }

        private void bEnter_Click(object sender, RoutedEventArgs e)
        {
            if (Choised == false)
            {
                Registration(tbLogin.Text, tbPass1.Text,
               tbPass2.Text);
            }
            else
            {
                Logining(tbLogin.Text, tbPass1.Text);
            }
        }

    }
}
