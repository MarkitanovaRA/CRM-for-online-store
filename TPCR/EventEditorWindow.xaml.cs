using System;
using System.Data.SqlClient;
using System.Windows;

namespace TPCR
{
    public partial class EventEditorWindow : Window
    {
        private int? eventId;
        DB dataBase = new DB();

        public EventEditorWindow(int? eventId = null)
        {
            InitializeComponent();
            this.eventId = eventId;

            if (eventId.HasValue)
            {
                LoadEventDetails(eventId.Value);
            }
        }

        private void LoadEventDetails(int eventId)
        {
            string queryEvent = "SELECT EventName, EventDate, EventTime, CreatedBy FROM EventsDB WHERE EventID = @EventID";

            try
            {
                dataBase.openConnection();
                using (SqlCommand commandEvent = new SqlCommand(queryEvent, dataBase.getConnection()))
                {
                    commandEvent.Parameters.AddWithValue("@EventID", eventId);
                    using (SqlDataReader reader = commandEvent.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbEventName.Text = reader["EventName"].ToString();
                            dpEventDate.SelectedDate = Convert.ToDateTime(reader["EventDate"]);
                            tbEventTime.Text = reader["EventTime"].ToString();
                            tbCreatedBy.Text = reader["CreatedBy"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных события: " + ex.Message);
            }
            finally
            {
                dataBase.closeConnection();
            }
        }

        private void bSaveEvent_Click(object sender, RoutedEventArgs e)
        {
            string eventName = tbEventName.Text;
            DateTime? eventDate = dpEventDate.SelectedDate;
            string eventTime = tbEventTime.Text;
            string createdBy = tbCreatedBy.Text;

            if (string.IsNullOrEmpty(eventName) || !eventDate.HasValue || string.IsNullOrEmpty(eventTime) || string.IsNullOrEmpty(createdBy))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
                dataBase.openConnection();
                string query;

                if (eventId.HasValue)
                {
                    // Update existing event
                    query = "UPDATE EventsDB SET EventName = @EventName, EventDate = @EventDate, EventTime = @EventTime, CreatedBy = @CreatedBy WHERE EventID = @EventID";
                }
                else
                {
                    // Create new event
                    query = "INSERT INTO EventsDB (EventName, EventDate, EventTime, CreatedBy) VALUES (@EventName, @EventDate, @EventTime, @CreatedBy)";
                }

                using (SqlCommand command = new SqlCommand(query, dataBase.getConnection()))
                {
                    command.Parameters.AddWithValue("@EventName", eventName);
                    command.Parameters.AddWithValue("@EventDate", eventDate);
                    command.Parameters.AddWithValue("@EventTime", eventTime);
                    command.Parameters.AddWithValue("@CreatedBy", createdBy);

                    if (eventId.HasValue)
                    {
                        command.Parameters.AddWithValue("@EventID", eventId.Value);
                    }

                    command.ExecuteNonQuery();
                }

                MessageBox.Show(eventId.HasValue ? "Событие успешно обновлено" : "Событие успешно создано");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении события: " + ex.Message);
            }
            finally
            {
                dataBase.closeConnection();
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
