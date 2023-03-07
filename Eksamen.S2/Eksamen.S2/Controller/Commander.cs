using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Eksamen.S2.View;
using Eksamen.S2.Model;

namespace Eksamen.S2.Controller
{
    public class Commander
    {

        DatabaseHandler db = new DatabaseHandler();
        ManualResetEvent pause = new ManualResetEvent(false);

        List<long> scores = new List<long>();
        Player currentPlayer;
        List<Player> allPlayers = new List<Player>();

        public Commander()
        {
            AllPlayers = db.GetAllPlayers();
        }

        public List<Player> AllPlayers { get => allPlayers; set => allPlayers = value; }

        public void UpdateCurrentPlayer(string username, string phoneNumber, string email)
        {
            currentPlayer = new Player(username, phoneNumber, email);
            if (!AllPlayers.Contains(currentPlayer))
            {
                db.AddNew(currentPlayer);
            }
        }

        public void AddPoints(int amount)
        {
            currentPlayer.Score += amount;
        }

        public void RemovePoints(int amount)
        {
            currentPlayer.Score -= amount;
        }

        public string GetScore()
        {
            if (currentPlayer.Score > 0)
            {
                return "Score: " + currentPlayer.Score;
            }
            else
            {
                currentPlayer.Score = 0;
                return "Score: 0";
            }
        }

    }
}
