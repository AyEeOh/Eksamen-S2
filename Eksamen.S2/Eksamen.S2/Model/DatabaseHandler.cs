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
using System.Threading;
using System.Data; // DataSet, DataTable, DataRow
using System.Data.SqlClient; // SqlDataAdapter
using Eksamen.S2.Controller;

namespace Eksamen.S2.Model
{
    class DatabaseHandler
    {

        private string connectionString = "Data Source=CV-BB-5910;Initial Catalog=JumpSquare;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private DataSet Execute(string query)
        {
            try
            {
                DataSet resultSet = new DataSet();
                using (SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, new SqlConnection(connectionString))))
                {
                    // Open conn, execute query, close conn, wrap result in DataSet:
                    adapter.Fill(resultSet);
                }
                return resultSet;
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't commit data to SQL Server.", "SQL Server Problem", MessageBoxButton.OK, MessageBoxImage.Warning);
                throw;
            }
        }

        public List<Player> GetAllPlayers()
        {
            try
            {
                List<Player> allPlayers = new List<Player>(0);
                string allPlayersQuery = "SELECT * FROM Players";

                // Perform query and save result in variable:
                DataSet resultSet = Execute(allPlayersQuery);

                // Get the first table of the data set, and save in variable:
                DataTable playersTable = resultSet.Tables[0];

                // Iterate through the rows of the table, and extract the data,
                // and create a new person object each time, and add that to the list of persons.
                foreach (DataRow skillRow in playersTable.Rows)
                {
                    string username = (string)skillRow["Username"];
                    long highScore = (long)skillRow["HighScore"];
                    uint timesPlayed = (uint)skillRow["TimesPlayed"];
                    string phoneNumber = (string)skillRow["PhoneNumber"];
                    string email = (string)skillRow["Email"];
                    Player player = new Player(username, highScore, timesPlayed, phoneNumber, email);
                    allPlayers.Add(player);
                }
                return allPlayers;
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't access SQL Server.", "SQL Server Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        public void AddNew(Player player)
        {
            try
            {
                string addNewPlayerQuery =
                    $"INSERT INTO Players (Username, HighScore, TimesPlayed, PhoneNumber, Email) " +
                    $"VALUES('{player.Username}','{player.HighScore}','{player.TimesPlayed}','{player.PhoneNumber}','{player.Email}')";
                Execute(addNewPlayerQuery);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to add and store item.", "SQL Server Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        public void Update(Player player)
        {
            string updateQuery = $"UPDATE Players " +
                $"SET HighScore='{player.HighScore}', TimesPlayed={player.TimesPlayed}, PhoneNumber={player.PhoneNumber}, Email={player.Email} " +
                $"WHERE Username='{player.Username}';";
            Execute(updateQuery);
        }

    }
}
