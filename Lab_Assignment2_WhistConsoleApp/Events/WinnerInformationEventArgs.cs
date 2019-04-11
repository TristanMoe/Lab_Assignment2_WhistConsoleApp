using System;
using Lab_Assignment2_WhistConsoleApp.DATA.Team;

namespace Lab_Assignment2_WhistConsoleApp.Events
{
    public class WinnerInformationEventArgs : EventArgs
    {
        public Team WinnerTeam { get; set; }
    }
}