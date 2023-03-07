using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eksamen.S2.Controller;

namespace Eksamen.S2.Model
{
    public class Player
    {

        string username;
        long score;
        long highScore;
        uint timesPlayed;
        string phoneNumber;
        string email;

        /// <summary>
        /// Create an instance of the Player class by assigning username, phoneNumber, and email.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        public Player(string username, string phoneNumber, string email)
        {
            Username = username;
            Score = 0;
            HighScore = 0;
            TimesPlayed = 0;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        /// <summary>
        /// Create an instance of the Player class by assigning username, score, timesPlayed, phoneNumber, and email values.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="score"></param>
        /// <param name="timesPlayed"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        public Player(string username, long highScore, uint timesPlayed, string phoneNumber, string email)
        {
            Username = username;
            Score = 0;
            HighScore = highScore;
            TimesPlayed = timesPlayed;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        /// <summary>
        /// The unique username of the player. Cannot be the same as someone else.
        /// </summary>
        public string Username { get => username; set => username = value; }
        /// <summary>
        /// The player's current score.
        /// </summary>
        public long Score { get => score; set => score = value; }
        /// <summary>
        /// The player's highest score ever achieved.
        /// </summary>
        public long HighScore { get => highScore; set => highScore = value; }
        /// <summary>
        /// The amount of times the player has played the game.
        /// </summary>
        public uint TimesPlayed { get => timesPlayed; set => timesPlayed = value; }
        /// <summary>
        /// The player's phone number.
        /// </summary>
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        /// <summary>
        /// The player's email address.
        /// </summary>
        public string Email { get => email; set => email = value; }
    }
}
