using System;
using UnityEngine;
using UnityEngine.UI;
namespace Service
{


    public class TimeService : MonoBehaviour
    {
        private int gameHours { get; set; }
        private int gameMinutes { get; set; }
        private int gameSeconds { get; set; }

        private int zero { get; set; }
        private string zeroText;

        string stringMinutes;
        string stringHours;

        public Text TextTime;

        void Update()
        {
            gameSeconds += 1;
            if (gameSeconds == 3)
            {
                gameSeconds -= 3;
                gameMinutes++;

                if (gameMinutes == 60)
                {
                    gameMinutes -= 60;
                    gameHours++;

                    if (gameHours == 24)
                    {
                        gameHours -= 24;
                        Debug.Log("День прошёл");
                    }
                }
            }

            if (gameHours <= 23 && gameHours >= 10) zero = 1;
            else zero = 2;

            if (zero == 1) zeroText = "";
            else if (zero == 2) zeroText = "0";

            stringMinutes = gameMinutes.ToString();
            stringHours = gameHours.ToString();

            TextTime.text = "Время " + zeroText + stringHours + ":00 ";
        }
    }

}

