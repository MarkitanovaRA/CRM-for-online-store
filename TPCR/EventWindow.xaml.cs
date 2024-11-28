using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace TPCR
{
    public partial class EventWindow : Window
    {
        DB dataBase = new DB();

        public EventWindow()
        {
            InitializeComponent();
            FillEventGrid();
        }

        private void FillEventGrid()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string queryEvents = "SELECT EventID, EventName, EventDate, EventTime, CreatedBy FROM EventsDB";

            try
            {
                dataBase.openConnection();
                using (SqlCommand commandEvents = new SqlCommand(queryEvents, dataBase.getConnection()))
                {
                    adapter.SelectCommand = commandEvents;
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при заполнении таблицы событий: " + ex.Message);
            }
            finally
            {
                dataBase.closeConnection();
            }

            dgEvents.ItemsSource = table.DefaultView;
        }

        private void bCreateEvent_Click(object sender, RoutedEventArgs e)
        {
            EventEditorWindow eventEditor = new EventEditorWindow();
            eventEditor.ShowDialog();
            FillEventGrid();
        }

        private void bEditEvent_Click(object sender, RoutedEventArgs e)
        {
            if (dgEvents.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgEvents.SelectedItem;
                int eventId = Convert.ToInt32(row["EventID"]);

                EventEditorWindow eventEditor = new EventEditorWindow(eventId);
                eventEditor.ShowDialog();
                FillEventGrid();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите событие для редактирования.");
            }
        }

        private void bDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            if (dgEvents.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgEvents.SelectedItem;
                int eventId = Convert.ToInt32(row["EventID"]);

                string deleteQuery = $"DELETE FROM EventsDB WHERE EventID = {eventId}";

                try
                {
                    dataBase.ExecuteNonQuery(deleteQuery);
                    MessageBox.Show("Событие успешно удалено");
                    FillEventGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при удалении события: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите событие для удаления.");
            }
        }

        private void eventCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обновить таблицу событий на основе выбранной даты в календаре
            DateTime selectedDate = eventCalendar.SelectedDate ?? DateTime.Now;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string queryEvents = "SELECT EventID, EventName, EventDate, EventTime, CreatedBy FROM EventsDB WHERE EventDate = @EventDate";

            try
            {
                dataBase.openConnection();
                using (SqlCommand commandEvents = new SqlCommand(queryEvents, dataBase.getConnection()))
                {
                    commandEvents.Parameters.AddWithValue("@EventDate", selectedDate);
                    adapter.SelectCommand = commandEvents;
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при заполнении таблицы событий: " + ex.Message);
            }
            finally
            {
                dataBase.closeConnection();
            }

            dgEvents.ItemsSource = table.DefaultView;
        }
    }
}
