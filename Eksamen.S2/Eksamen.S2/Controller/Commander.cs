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

        public void UpdateCurrentPlayer(string username, string phoneNumber, string email)
        {
            currentPlayer = new Player(username, phoneNumber, email);
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
