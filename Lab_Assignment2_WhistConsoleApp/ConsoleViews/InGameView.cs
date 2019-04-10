using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    /// <summary>
    /// 1. AddRound
    /// 2. End Game 
    /// </summary>
    class InGameView
    {
        public GameInformation GameInformation { get; private set; }

        public InGameView(GameInformation gameInformation)
        {
            GameInformation = gameInformation;
            // gameInformation += gameInformationHandler()
        }

        private void HandleGameInformationEvent(object sender, EventArgs e)
        {
            Console.Clear();
            Console.WriteLine("--------------------- Choose action ---------------------");
            Console.WriteLine("1. Add round");
            Console.WriteLine("2. End game");
            while (true)
            {
                try
                {
                    string action = Console.ReadLine();
                    if (action.Equals("1"))
                    {
                        // invoke addgame event
                        return;
                    }
                    if (action.Equals("2"))
                    {
                        //invoke endgame view
                        return;
                    }
                    throw new Exception("Must choose options 1 or 2, try again");
                }
                catch (Exception Exception)
                {
                    Console.WriteLine(Exception);
                }
            }
        }

        public void InGame()
        {

        }


    }
}
